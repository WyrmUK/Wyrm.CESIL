using System.IO;
using System.Threading.Tasks;
using System.Threading;
using Wyrm.CESIL.Exceptions;

namespace Wyrm.CESIL.Executing
{
    internal class JumpOperation : IOperation
    {
        public void Execute(object value, IOperationState state, TextWriter _)
        {
            if (value is string) state.Instruction = state.InstructionFor((string)value);
            else throw new IllegalOperationException("Unknown data type for JUMP.");
        }

        public Task ExecuteAsync(object value, IOperationState state, TextWriter writer, CancellationToken _)
        {
            Execute(value, state, writer);
            return Task.CompletedTask;
        }
    }
}
