using System.IO;

namespace Wyrm.CESIL.Executing
{
    internal class OutOperation : IOperation
    {
        public void Execute(object value, OperationState state, TextWriter writer)
        {
            writer.Write(state.Accumulator.ToString());
            ++state.Instruction;
        }
    }
}
