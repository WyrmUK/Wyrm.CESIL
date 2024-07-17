using System;
using System.Collections.Generic;
using System.IO;
using Wyrm.CESIL.Parsing;

namespace Wyrm.CESIL.Executing
{
    public class CesilOperator : IOperator
    {
        private Dictionary<InstructionType, IOperation> operations;

        public CesilOperator()
        {
            operations = new Dictionary<InstructionType, IOperation>
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
                { InstructionType.HALT, new HaltOperation() },
            };
        }

        public void Operate(Instruction instruction, OperationState state, TextWriter writer)
        {
            if (!instruction.InstructionType.HasValue || !operations.ContainsKey(instruction.InstructionType.Value)) throw new NotSupportedException();
            var operation = operations[instruction.InstructionType.Value];
            operation.Execute(instruction.Value, state, writer);
        }
    }
}
