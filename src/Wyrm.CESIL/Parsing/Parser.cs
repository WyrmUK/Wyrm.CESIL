using System;
using System.Collections.Generic;
using Wyrm.CESIL.Exceptions;
using Wyrm.CESIL.Lexical;

namespace Wyrm.CESIL.Parsing
{
    /// <summary>
    /// A Parser class to parse language tokens.
    /// </summary>
    public class Parser : IParser
    {
        private IInstructionBuilder _builder;

        /// <summary>
        /// Creates a new <see cref="Parser"/> instance.
        /// </summary>
        /// <param name="builder">An Instruction Builder that implements <see cref="IInstructionBuilder"/>.</param>
        public Parser(IInstructionBuilder builder)
        {
            _builder = builder;
        }

        /// <inheritdoc/>
        public IEnumerable<Instruction> Parse(IEnumerable<Token> tokens, IList<long> data, IList<SyntaxError> errors)
        {
            List<Instruction> instructionSet = new List<Instruction>();
            Instruction instruction = null;
            bool isData = false;
            foreach (var token in tokens)
            {
                try
                {
                    if (!_builder.BuildInstruction(token, ref instruction, isData) || instruction == null) continue;
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
            }
            if (instruction != null) errors.Add(new SyntaxError(-1, -1, "Unterminated program"));
            return instructionSet;
        }
    }
}
