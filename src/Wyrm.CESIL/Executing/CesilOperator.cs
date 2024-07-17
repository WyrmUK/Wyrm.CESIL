using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Wyrm.CESIL.Parsing;

namespace Wyrm.CESIL.Executing
{
    /// <summary>
    /// A class for operating CESIL instructions.
    /// </summary>
    public class CesilOperator : IOperator
    {
        private static readonly Dictionary<InstructionType, IOperation> operations = new Dictionary<InstructionType, IOperation>
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

        /// <inheritdoc/>
        public void Operate(Instruction instruction, IOperationState state, TextWriter writer)
        {
            if (!instruction.InstructionType.HasValue || !operations.ContainsKey(instruction.InstructionType.Value)) throw new NotSupportedException();
            var operation = operations[instruction.InstructionType.Value];
            operation.Execute(instruction.Value, state, writer);
        }

        /// <inheritdoc/>
        public Task OperateAsync(Instruction instruction, IOperationState state, TextWriter writer, CancellationToken cancellationToken)
        {
            if (!instruction.InstructionType.HasValue || !operations.ContainsKey(instruction.InstructionType.Value)) throw new NotSupportedException();
            var operation = operations[instruction.InstructionType.Value];
            return operation.ExecuteAsync(instruction.Value, state, writer, cancellationToken);
        }
    }
}
