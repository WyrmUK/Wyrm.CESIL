using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Wyrm.CESIL.Lexical;
using Wyrm.CESIL.Parsing;

namespace Wyrm.CESIL
{
    /// <summary>
    /// An Intepreter class to load and run a program.
    /// </summary>
    public class Interpreter : IInterpreter
    {
        private readonly ILexer _lexicalAnalyser;
        private readonly IParser _parser;
        private readonly IExecutor _executor;
        private readonly List<Instruction> _instructionSet = new List<Instruction>();
        private readonly List<long> _dataSet = new List<long>();
        private readonly List<SyntaxError> _errors = new List<SyntaxError>();

        /// <summary>
        /// Creates a new <see cref="Interpreter"/> instance.
        /// </summary>
        /// <param name="lexer">A Lexical Analyser implementing <see cref="ILexer"/>.</param>
        /// <param name="parser">A Parser implementing <see cref="IParser"/>.</param>
        /// <param name="executor">An Executor implementing <see cref="IExecutor"/>.</param>
        public Interpreter(ILexer lexer, IParser parser, IExecutor executor)
        {
            _lexicalAnalyser = lexer;
            _parser = parser;
            _executor = executor;
        }

        /// <inheritdoc/>
        public void Load(TextReader reader)
        {
            var tokens = _lexicalAnalyser.Analyse(reader, _errors);
            _instructionSet.AddRange(_parser.Parse(tokens, _dataSet, _errors));
        }

        /// <inheritdoc/>
        public async Task LoadAsync(TextReader reader, CancellationToken cancellationToken)
        {
            var tokens = await _lexicalAnalyser.AnalyseAsync(reader, _errors, cancellationToken);
            _instructionSet.AddRange(_parser.Parse(tokens, _dataSet, _errors));
        }

        /// <inheritdoc/>
        public void Run(TextWriter writer, Func<bool> terminate)
        {
            _executor.Prepare(_instructionSet, _errors);
            if (_errors.Count > 0)
            {
                foreach (var error in _errors)
                {
                    if (error.LineNo < 0) writer.WriteLine($"Error: {error.Message}");
                    else if (error.CharNo < 0) writer.WriteLine($"Error at line {error.LineNo}: {error.Message}");
                    else writer.WriteLine($"Error at line {error.LineNo} character {error.CharNo}: {error.Message}");
                }
                return;
            }
            _executor.Run(_dataSet, writer, terminate);
            writer.Flush();
        }

        /// <inheritdoc/>
        public async Task RunAsync(TextWriter writer, Func<bool> terminate, CancellationToken cancellationToken)
        {
            _executor.Prepare(_instructionSet, _errors);
            if (_errors.Count > 0)
            {
                foreach (var error in _errors)
                {
                    if (error.LineNo < 0) writer.WriteLine($"Error: {error.Message}");
                    else if (error.CharNo < 0) writer.WriteLine($"Error at line {error.LineNo}: {error.Message}");
                    else writer.WriteLine($"Error at line {error.LineNo} character {error.CharNo}: {error.Message}");
                }
                return;
            }
            await _executor.RunAsync(_dataSet, writer, terminate, cancellationToken);
            writer.Flush();
        }

        /// <inheritdoc/>
        public void Clear()
        {
            _instructionSet.Clear();
            _dataSet.Clear();
            _errors.Clear();
            _executor.Clear();
        }
    }
}
