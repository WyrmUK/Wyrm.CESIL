using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Wyrm.CESIL.Exceptions;

namespace Wyrm.CESIL.Executing
{
    internal class LoadOperation : IOperation
    {
        public void Execute(object value, IOperationState state, TextWriter _)
        {
            if (value is long) state.Accumulator = (long)value;
            else if (value is string) state.Accumulator = state[(string)value];
            else throw new IllegalOperationException("Unknown data type for LOAD.");
            ++state.Instruction;
        }

        public Task ExecuteAsync(object value, IOperationState state, TextWriter writer, CancellationToken _)
        {
            Execute(value, state, writer);
            return Task.CompletedTask;
        }
    }
}
