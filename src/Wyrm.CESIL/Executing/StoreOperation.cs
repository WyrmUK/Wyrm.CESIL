using System.IO;
using Wyrm.CESIL.Exceptions;

namespace Wyrm.CESIL.Executing
{
    internal class StoreOperation : IOperation
    {
        public void Execute(object value, OperationState state, TextWriter writer)
        {
            if (value is string) state[(string)value] = state.Accumulator;
            else throw new IllegalOperationException("Unknown data type for STORE.");
            ++state.Instruction;
        }
    }
}
