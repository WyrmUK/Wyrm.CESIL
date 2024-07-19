using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Wyrm.CESIL
{
    /// <summary>
    /// An interface to an Interpreter for an intepreted language.
    /// </summary>
    public interface IInterpreter
    {
        /// <summary>
        /// Loads the program from a <see cref="TextReader"/>.
        /// </summary>
        /// <param name="reader">The <see cref="TextReader"/> to read the program from.</param>
        void Load(TextReader reader);
        /// <summary>
        /// Loads the program from a <see cref="TextReader"/> asynchrnously.
        /// </summary>
        /// <param name="reader">The <see cref="TextReader"/> to read the program from.</param>
        /// <param name="cancellationToken">An optional token to cancel the load.</param>
        Task LoadAsync(TextReader reader, CancellationToken cancellationToken = default);
        /// <summary>
        /// Runs a loaded program writing the output to a <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="writer">A <see cref="TextWriter"/> to write the output to.</param>
        /// <param name="maxRunTime">The maximum amount of time the program will be allowed to run for. Null = no limit.</param>
        void Run(TextWriter writer, TimeSpan? maxRunTime = null);
        /// <summary>
        /// Runs a loaded program writing the output to a <see cref="TextWriter"/> asynchronously.
        /// </summary>
        /// <param name="writer">A <see cref="TextWriter"/> to write the output to.</param>
        /// <param name="maxRunTime">The maximum amount of time the program will be allowed to run for. Null = no limit.</param>
        /// <param name="cancellationToken">An optional token to cancel the run.</param>
        Task RunAsync(TextWriter writer, TimeSpan? maxRunTime = null, CancellationToken cancellationToken = default);
    }
}
