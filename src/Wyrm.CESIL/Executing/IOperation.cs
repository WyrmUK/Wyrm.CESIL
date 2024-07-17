using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Wyrm.CESIL.Executing
{
    internal interface IOperation
    {
        void Execute(object value, IOperationState state, TextWriter writer);
        Task ExecuteAsync(object value, IOperationState state, TextWriter writer, CancellationToken cancellationToken);
    }
}
