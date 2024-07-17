using System.IO;
using System.Threading.Tasks;
using System.Threading;
using Wyrm.CESIL.Exceptions;

namespace Wyrm.CESIL.Executing
{
    internal class StoreOperation : IOperation
    {
        public void Execute(object value, IOperationState state, TextWriter _)
        {
            if (value is string) state[(string)value] = state.Accumulator;
            else throw new IllegalOperationException("Unknown data type for STORE.");
            ++state.Instruction;
        }

        public Task ExecuteAsync(object value, IOperationState state, TextWriter writer, CancellationToken _)
        {
            Execute(value, state, writer);
            return Task.CompletedTask;
        }
    }
}
