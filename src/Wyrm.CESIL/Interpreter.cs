using System.Collections.Generic;
using System.IO;
using System.Threading;
using Wyrm.CESIL.Lexical;
using Wyrm.CESIL.Parsing;

namespace Wyrm.CESIL
{
    public class Interpreter : IInterpreter
    {
        private readonly ILexer lexicalAnalyser;
        private readonly IParser parser;
        private readonly IExecutor executor;
        private readonly List<Instruction> instructionSet = new List<Instruction>();
        private readonly List<long> dataSet = new List<long>();
        private readonly List<SyntaxError> errors = new List<SyntaxError>();

        public Interpreter(ILexer lexer, IParser parser, IExecutor executor)
        {
            lexicalAnalyser = lexer;
            this.parser = parser;
            this.executor = executor;
        }

        public void Load(TextReader reader, CancellationToken cancellationToken = default(CancellationToken))
        {
            var tokens = lexicalAnalyser.Analyse(reader, errors, cancellationToken);
            if (!cancellationToken.IsCancellationRequested)
                instructionSet.AddRange(parser.Parse(tokens, dataSet, errors, cancellationToken));
        }

        public void Run(TextWriter writer, CancellationToken cancellationToken = default(CancellationToken))
        {
            executor.Prepare(instructionSet, errors, cancellationToken);
            if (errors.Count > 0 && !cancellationToken.IsCancellationRequested)
            {
                foreach (var error in errors)
                {
                    if (error.LineNo < 0) writer.WriteLine($"Error: {error.Message}");
                    else if (error.CharNo < 0) writer.WriteLine($"Error at line {error.LineNo}: {error.Message}");
                    else writer.WriteLine($"Error at line {error.LineNo} character {error.CharNo}: {error.Message}");
                    if (cancellationToken.IsCancellationRequested) break;
                }
                return;
            }
            if (!cancellationToken.IsCancellationRequested)
                executor.Run(dataSet, writer, cancellationToken);
            writer.Flush();
        }
    }
}
