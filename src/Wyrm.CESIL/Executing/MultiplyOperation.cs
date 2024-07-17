using System.IO;
using Wyrm.CESIL.Exceptions;

namespace Wyrm.CESIL.Executing
{
    internal class MultiplyOperation : IOperation
    {
        public void Execute(object value, OperationState state, TextWriter writer)
        {
            if (value is long) state.Accumulator *= (long)value;
            else if (value is string) state.Accumulator *= state[(string)value];
            else throw new IllegalOperationException("Unknown data type for MULTIPLY.");
            ++state.Instruction;
        }
    }
}