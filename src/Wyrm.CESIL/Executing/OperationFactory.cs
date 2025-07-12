using System;
using System.Collections.Generic;
using Wyrm.CESIL.Parsing;

namespace Wyrm.CESIL.Executing
{
    internal class OperationFactory : IOperationFactory
    {
        private static readonly Dictionary<InstructionType, IOperation> Operations = new Dictionary<InstructionType, IOperation>
        {
            { InstructionType.LOAD, new LoadOperation() },
            { InstructionType.STORE, new StoreOperation() },
            { InstructionType.IN, new InOperation() },
            { InstructionType.ADD, new AddOperation() },
            { InstructionType.SUBTRACT, new SubtractOperation() },
            { InstructionType.MULTIPLY, new MultiplyOperation() },
            { InstructionType.DIVIDE, new DivideOperation() },
            { InstructionType.JUMP, new JumpOperation() },
            { InstructionType.JIZERO, new JizeroOperation() },
            { InstructionType.JINEG, new JinegOperation() },
            { InstructionType.PRINT, new PrintOperation() },
            { InstructionType.OUT, new OutOperation() },
            { InstructionType.LINE, new LineOperation() },
            { InstructionType.HALT, new HaltOperation() }
        };

        public IOperation CreateOperation(Instruction instruction)
        {
            if (!instruction.InstructionType.HasValue || !Operations.ContainsKey(instruction.InstructionType.Value)) throw new NotSupportedException();
            return Operations[instruction.InstructionType.Value];
        }
    }
}
