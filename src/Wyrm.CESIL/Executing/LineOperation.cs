using System.IO;

namespace Wyrm.CESIL.Executing
{
    internal class LineOperation : IOperation
    {
        public void Execute(object value, OperationState state, TextWriter writer)
        {
            writer.WriteLine();
            ++state.Instruction;
        }
    }
}
