using System.IO;

namespace Wyrm.CESIL.Executing
{
    internal class InOperation : IOperation
    {
        public void Execute(object value, OperationState state, TextWriter writer)
        {
            state.Accumulator = state.PopData();
            ++state.Instruction;
        }
    }
}
