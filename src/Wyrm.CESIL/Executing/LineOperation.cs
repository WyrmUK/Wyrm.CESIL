using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace Wyrm.CESIL.Executing
{
    internal class LineOperation : IOperation
    {
        public void Execute(object value, IOperationState state, TextWriter writer)
        {
            writer.WriteLine();
            ++state.Instruction;
        }

        public async Task ExecuteAsync(object value, IOperationState state, TextWriter writer, CancellationToken cancellationToken)
        {
            await writer.WriteLineAsync();
            if (cancellationToken.IsCancellationRequested) throw new TaskCanceledException();
            ++state.Instruction;
        }
    }
}
