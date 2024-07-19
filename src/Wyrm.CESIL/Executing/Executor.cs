using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Wyrm.CESIL.Exceptions;
#if NET6_0_OR_GREATER
using Wyrm.CESIL.Extensions;
#endif
using Wyrm.CESIL.Lexical;
using Wyrm.CESIL.Parsing;

namespace Wyrm.CESIL.Executing
{
    /// <summary>
    /// An Executor class for executing instructions.
    /// </summary>
    public class Executor : IExecutor
    {
        private readonly IOperator _operations;
        private readonly IOperationStateFactory _operationStateFactory;
        private readonly Dictionary<string, int> _labels = new Dictionary<string, int>();
        private IList<Instruction> _instructionSet;

        /// <summary>
        /// Creates a new <see cref="Executor"/> instance.
        /// </summary>
        /// <param name="operations">An operator executor implementing <see cref="IOperator"/>.</param>
        /// <param name="operationStateFactory">A factory for creating <see cref="IOperationState"/> instances.</param>
        public Executor(IOperator operations, IOperationStateFactory operationStateFactory)
        {
            _operations = operations;
            _operationStateFactory = operationStateFactory;
        }

        /// <inheritdoc/>
        public void Prepare(IList<Instruction> instructionSet, IList<SyntaxError> errors)
        {
            _instructionSet = instructionSet;
            for (var ind = 0; ind < instructionSet.Count; ++ind)
            {
                if (!string.IsNullOrEmpty(instructionSet[ind].Label))
                {
                    if (_labels.ContainsKey(instructionSet[ind].Label)) errors.Add(new SyntaxError(instructionSet[ind].LineNo, -1, "Duplicate label"));
                    else _labels.Add(instructionSet[ind].Label, ind);
                }
            }
        }

        /// <inheritdoc/>
        public void Run(IList<long> dataSet, TextWriter writer, TimeSpan? maxRunTime)
        {
            var state = _operationStateFactory.CreateOperationState(dataSet, _labels);
            var started = DateTime.UtcNow;
            try
            {
                try
                {
                    while (!state.Halted)
                    {
                        _operations.Operate(_instructionSet[state.Instruction], state, writer);
                        if (maxRunTime.HasValue && started + maxRunTime.Value < DateTime.UtcNow) throw new TimeoutException();
                    }
                }
                catch (IllegalOperationException ex)
                {
                    writer.WriteLine($"Illegal operation at line {_instructionSet[state.Instruction].LineNo}: {ex.Message}");
                }
                catch (IllegalLabelException)
                {
                    writer.WriteLine($"Unknown label at line {_instructionSet[state.Instruction].LineNo}.");
                }
                catch (NotInitialisedException)
                {
                    writer.WriteLine($"Uninitialised store at line {_instructionSet[state.Instruction].LineNo}.");
                }
                catch (TimeoutException)
                {
                    writer.WriteLine($"Timed out at line {_instructionSet[state.Instruction].LineNo}.");
                }
            }
            catch (Exception ex)
            {
                writer.WriteLine($"Illegal operation: {ex.Message}");
            }
        }

        /// <inheritdoc/>
        public async Task RunAsync(IList<long> dataSet, TextWriter writer, TimeSpan? maxRunTime, CancellationToken cancellationToken)
        {
            var state = new OperationState(dataSet, _labels);
            var started = DateTime.UtcNow;
            try
            {
                try
                {
                    while (!state.Halted && !cancellationToken.IsCancellationRequested)
                    {
                        await _operations.OperateAsync(_instructionSet[state.Instruction], state, writer, cancellationToken);
                        if (maxRunTime.HasValue && started + maxRunTime.Value < DateTime.UtcNow) throw new TimeoutException();
                    }
                }
                catch (IllegalOperationException ex)
                {
                    var error = $"Illegal operation at line {_instructionSet[state.Instruction].LineNo}: {ex.Message}";
#if NET6_0_OR_GREATER
                    await writer.WriteLineAsync(error.ToStringBuilder(), cancellationToken);
#else
                    writer.WriteLine(error);
#endif
                }
                catch (IllegalLabelException)
                {
                    var error = $"Unknown label at line {_instructionSet[state.Instruction].LineNo}.";
#if NET6_0_OR_GREATER
                    await writer.WriteLineAsync(error.ToStringBuilder(), cancellationToken);
#else
                    writer.WriteLine(error);
#endif
                }
                catch (NotInitialisedException)
                {
                    var error = $"Uninitialised store at line {_instructionSet[state.Instruction].LineNo}.";
#if NET6_0_OR_GREATER
                    await writer.WriteLineAsync(error.ToStringBuilder(), cancellationToken);
#else
                    writer.WriteLine(error);
#endif
                }
                catch (TimeoutException)
                {
                    var error = $"Timed out at line {_instructionSet[state.Instruction].LineNo}.";
#if NET6_0_OR_GREATER
                    await writer.WriteLineAsync(error.ToStringBuilder(), cancellationToken);
#else
                    writer.WriteLine(error);
#endif
                }
            }
            catch (Exception ex)
            {
                var error = $"Illegal operation: {ex.Message}";
#if NET6_0_OR_GREATER
                await writer.WriteLineAsync(error.ToStringBuilder(), cancellationToken);
#else
                writer.WriteLine(error);
#endif
            }
        }
    }
}
