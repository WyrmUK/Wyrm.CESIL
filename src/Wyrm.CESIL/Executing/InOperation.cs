using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace Wyrm.CESIL.Executing
{
    internal class InOperation : IOperation
    {
        public void Execute(object value, IOperationState state, TextWriter _)
        {
            state.Accumulator = state.PopData();
            ++state.Instruction;
        }

        public Task ExecuteAsync(object value, IOperationState state, TextWriter writer, CancellationToken _)
        {
            Execute(value, state, writer);
            return Task.CompletedTask;
        }
    }
}
