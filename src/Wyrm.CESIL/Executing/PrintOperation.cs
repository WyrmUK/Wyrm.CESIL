using System.IO;
using System.Threading.Tasks;
using System.Threading;
using Wyrm.CESIL.Exceptions;
#if NET6_0_OR_GREATER
using Wyrm.CESIL.Extensions;
#endif

namespace Wyrm.CESIL.Executing
{
    internal class PrintOperation : IOperation
    {
        internal const string DoubleQuotes = "\"";
        private const string InternalDoubleQuotes = "\"\"";

        public void Execute(object value, IOperationState state, TextWriter writer)
        {
            if (value is string && ((string)value).StartsWith(DoubleQuotes) && ((string)value).EndsWith(DoubleQuotes)) writer.Write(((string)value).Substring(1, ((string)value).Length - 2).Replace(InternalDoubleQuotes, DoubleQuotes));
            else throw new IllegalOperationException("Unknown data type for PRINT.");
            ++state.Instruction;
        }

        public async Task ExecuteAsync(object value, IOperationState state, TextWriter writer, CancellationToken cancellationToken)
        {
            if (value is string && ((string)value).StartsWith(DoubleQuotes) && ((string)value).EndsWith(DoubleQuotes))
            {
                var text = ((string)value).Substring(1, ((string)value).Length - 2).Replace(InternalDoubleQuotes, DoubleQuotes);
#if NET6_0_OR_GREATER
                await writer.WriteAsync(text.ToStringBuilder(), cancellationToken);
#else
                await writer.WriteAsync(text);
                if (cancellationToken.IsCancellationRequested) throw new TaskCanceledException();
#endif
            }
            else throw new IllegalOperationException("Unknown data type for PRINT.");
            ++state.Instruction;
        }
    }
}
