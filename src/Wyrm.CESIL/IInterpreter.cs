using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Wyrm.CESIL
{
    public interface IInterpreter
    {
        void Load(TextReader reader);
        Task LoadAsync(TextReader reader, CancellationToken cancellationToken = default);
        void Run(TextWriter writer);
        Task RunAsync(TextWriter writer, CancellationToken cancellationToken = default);
    }
}
