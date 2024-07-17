using System.IO;
using System.Threading.Tasks;
using System.Threading;
using Wyrm.CESIL.Exceptions;

namespace Wyrm.CESIL.Executing
{
    internal class JizeroOperation : IOperation
    {
        public void Execute(object value, IOperationState state, TextWriter _)
        {
            if (state.Accumulator == 0)
            {
                if (value is string) state.Instruction = state.InstructionFor((string)value);
                else throw new IllegalOperationException("Unknown data type for JIZERO.");
            }
            else ++state.Instruction;
        }

        public Task ExecuteAsync(object value, IOperationState state, TextWriter writer, CancellationToken _)
        {
            Execute(value, state, writer);
            return Task.CompletedTask;
        }
    }
}
