using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Wyrm.CESIL.Exceptions;
using Wyrm.CESIL.Lexical;
using Wyrm.CESIL.Parsing;

namespace Wyrm.CESIL.Executing
{
    public class Executor : IExecutor
    {
        private readonly IOperator operations;
        private readonly Dictionary<string, int> labels = new Dictionary<string, int>();
        private IList<Instruction> instructionSet;

        public Executor(IOperator operations)
        {
            this.operations = operations;
        }

        public void Prepare(IList<Instruction> instructionSet, IList<SyntaxError> errors, CancellationToken cancellationToken)
        {
            this.instructionSet = instructionSet;
            for (var ind = 0; ind < instructionSet.Count; ++ind)
            {
                if (!string.IsNullOrEmpty(instructionSet[ind].Label))
                {
                    if (labels.ContainsKey(instructionSet[ind].Label)) errors.Add(new SyntaxError(instructionSet[ind].LineNo, -1, "Duplicate label"));
                    else labels.Add(instructionSet[ind].Label, ind);
                }
                if (cancellationToken.IsCancellationRequested) return;
            }
        }

        public void Run(IList<long> dataSet, TextWriter writer, CancellationToken cancellationToken)
        {
            var state = new OperationState(dataSet, labels);
            try
            {
                try
                {
                    while (!state.Halted && !cancellationToken.IsCancellationRequested)
                    {
                        operations.Operate(instructionSet[state.Instruction], state, writer);
                    }
                }
                catch (IllegalOperationException ex)
                {
                    writer.WriteLine($"Illegal operation at line {instructionSet[state.Instruction].LineNo}: {ex.Message}");
                }
                catch (IllegalLabelException)
                {
                    writer.WriteLine($"Unknown label at line {instructionSet[state.Instruction].LineNo}.");
                }
                catch (NotInitialisedException)
                {
                    writer.WriteLine($"Uninitialised store at line {instructionSet[state.Instruction].LineNo}.");
                }
            }
            catch (Exception ex)
            {
                writer.WriteLine($"Illegal operation: {ex.Message}");
            }
        }
    }
}
