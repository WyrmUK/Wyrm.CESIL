using System.IO;
using Wyrm.CESIL.Exceptions;

namespace Wyrm.CESIL.Executing
{
    internal class JumpOperation : IOperation
    {
        public void Execute(object value, OperationState state, TextWriter writer)
        {
            if (value is string) state.Instruction = state.InstructionFor((string)value);
            else throw new IllegalOperationException("Unknown data type for JUMP.");
        }
    }
}
