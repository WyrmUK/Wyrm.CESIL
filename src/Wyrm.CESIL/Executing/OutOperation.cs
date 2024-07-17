using System.IO;
using System.Threading.Tasks;
using System.Threading;
#if NET6_0_OR_GREATER
using Wyrm.CESIL.Extensions;
#endif

namespace Wyrm.CESIL.Executing
{
    internal class OutOperation : IOperation
    {
        public void Execute(object value, IOperationState state, TextWriter writer)
        {
            writer.Write(state.Accumulator.ToString());
            ++state.Instruction;
        }

        public async Task ExecuteAsync(object value, IOperationState state, TextWriter writer, CancellationToken cancellationToken)
        {
            var result = state.Accumulator.ToString();
#if NET6_0_OR_GREATER
            await writer.WriteAsync(result.ToStringBuilder(), cancellationToken);
#else
            await writer.WriteAsync(result);
            if (cancellationToken.IsCancellationRequested) throw new TaskCanceledException();
#endif
            ++state.Instruction;
        }
    }
}
