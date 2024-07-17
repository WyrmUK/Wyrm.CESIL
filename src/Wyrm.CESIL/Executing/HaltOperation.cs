using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace Wyrm.CESIL.Executing
{
    internal class HaltOperation : IOperation
    {
        public void Execute(object value, IOperationState state, TextWriter _)
        {
            state.Halted = true;
        }

        public Task ExecuteAsync(object value, IOperationState state, TextWriter writer, CancellationToken _)
        {
            Execute(value, state, writer);
            return Task.CompletedTask;
        }
    }
}
