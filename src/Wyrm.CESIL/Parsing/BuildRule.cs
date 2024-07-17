using System;
using Wyrm.CESIL.Exceptions;
using Wyrm.CESIL.Executing;
using Wyrm.CESIL.Extensions;
using Wyrm.CESIL.Lexical;

namespace Wyrm.CESIL.Parsing
{
    internal class BuildRule
    {
        public bool IsData { get; set; }
        public bool CanBeData { get; set; }
        public bool InDataSection { get; set; }
        public bool StartsInstruction { get; set; }
        public bool AddsToInstruction { get; set; }
        public bool CreatesInstruction { get; set; }
        public bool IsInstruction { get; set; }
        public bool IsInteger { get; set; }
        public bool IsLabel { get; set; }
        public bool IsVariable { get; set; }
        public bool IsString { get; set; }
        public bool NullInstruction { get; set; }
        public bool Ends { get; set; }

        public bool Build(bool isData, ref Instruction instruction, Token token)
        {
            if (!IsData && !CanBeData && !InDataSection && isData) throw new IllegalIntegerException();
            if (CanBeData && isData && instruction != null) throw new SyntaxException();
            if (CanBeData && !isData && (instruction == null || !instruction.InstructionType.HasValue)) throw new IllegalInstructionException();
            if (AddsToInstruction && instruction == null) throw new IllegalInstructionException();
            if (StartsInstruction && instruction != null) throw new IncompleteInstructionException();
            if (CreatesInstruction && instruction == null) instruction = new Instruction(token.LineNo);
            if (IsInstruction && instruction.InstructionType.HasValue) throw new IncompleteInstructionException();
            if (IsInteger)
            {
                if (!long.TryParse(token.Value, out long intVal)) throw new IllegalIntegerException();
                instruction.Value = intVal;
            }
            if (IsLabel)
            {
                if (!token.Value.IsLettersAndDigits()) throw new IllegalLabelException();
                instruction.Label = token.Value;
            }
            if (IsVariable)
            {
                if (!token.Value.IsLettersAndDigits()) throw new IllegalLocationException();
                instruction.Value = token.Value;
            }
            if (IsString)
            {
                if (instruction.Value != null)
                {
                    if (!(instruction.Value is string)) throw new SyntaxException();
                    if (!((string)instruction.Value).StartsWith(PrintOperation.DoubleQuotes) || !((string)instruction.Value).EndsWith(PrintOperation.DoubleQuotes)) throw new BadStringException();
                    instruction.Value = ((string)instruction.Value) + token.Value;
                }
                else instruction.Value = token.Value;
            }
            if (IsInstruction && !NullInstruction)
            {
                if (!Enum.TryParse(token.Value, out InstructionType instructionType)) throw new IllegalInstructionException();
                instruction.InstructionType = instructionType;
            }
            return (CanBeData && isData) || Ends;
        }
    }
}
