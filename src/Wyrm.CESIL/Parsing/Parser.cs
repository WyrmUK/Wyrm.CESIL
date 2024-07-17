using System;
using System.Collections.Generic;
using System.Threading;
using Wyrm.CESIL.Exceptions;
using Wyrm.CESIL.Lexical;

namespace Wyrm.CESIL.Parsing
{
    public class Parser : IParser
    {
        private IInstructionBuilder builder;

        public Parser(IInstructionBuilder builder)
        {
            this.builder = builder;
        }

        public IEnumerable<Instruction> Parse(IEnumerable<Token> tokens, IList<long> data, IList<SyntaxError> errors, CancellationToken cancellationToken)
        {
            List<Instruction> instructionSet = new List<Instruction>();
            Instruction instruction = null;
            bool isData = false;
            foreach (var token in tokens)
            {
                try
                {
                    if (!builder.BuildInstruction(token, ref instruction, isData) || instruction == null) continue;
                    if (isData) data.Add((long)instruction.Value);
                    else
                    {
                        if (instruction.InstructionType == null && instruction.Value == null)
                        {
                            isData = true;
                            instruction.InstructionType = InstructionType.HALT;
                        }
                        instructionSet.Add(instruction);
                    }
                    instruction = null;
                }
                catch (SyntaxException)
                {
                    errors.Add(new SyntaxError(token.LineNo, token.CharNo, "Syntax error"));
                }
                catch (NotSupportedException)
                {
                    errors.Add(new SyntaxError(token.LineNo, token.CharNo, "Unsupported token"));
                }
                catch (IncompleteInstructionException)
                {
                    errors.Add(new SyntaxError(token.LineNo, token.CharNo, "Incomplete instruction"));
                }
                catch (IllegalIntegerException)
                {
                    errors.Add(new SyntaxError(token.LineNo, token.CharNo, "Illegal integer"));
                }
                catch (IllegalDataException)
                {
                    errors.Add(new SyntaxError(token.LineNo, token.CharNo, "Illegal data"));
                }
                catch (IllegalInstructionException)
                {
                    errors.Add(new SyntaxError(token.LineNo, token.CharNo, "Illegal instruction"));
                }
                catch (IllegalLabelException)
                {
                    errors.Add(new SyntaxError(token.LineNo, token.CharNo, "Illegal label"));
                }
                catch (IllegalLocationException)
                {
                    errors.Add(new SyntaxError(token.LineNo, token.CharNo, "Illegal variable or label"));
                }
                if (cancellationToken.IsCancellationRequested) break;
            }
            if (instruction != null) errors.Add(new SyntaxError(-1, -1, "Unterminated program"));
            return instructionSet;
        }
    }
}
