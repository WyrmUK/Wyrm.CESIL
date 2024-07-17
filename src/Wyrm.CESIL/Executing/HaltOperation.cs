using System.IO;

namespace Wyrm.CESIL.Executing
{
    internal class HaltOperation : IOperation
    {
        public void Execute(object value, OperationState state, TextWriter writer)
        {
            state.Halted = true;
        }
    }
}
