using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Wyrm.CESIL.Lexical;
using Wyrm.CESIL.Parsing;

namespace Wyrm.CESIL
{
    /// <summary>
    /// An interface to an instruction Executor.
    /// </summary>
    public interface IExecutor
    {
        /// <summary>
        /// Prepares the executor for running the instructions.
        /// </summary>
        /// <param name="instructionSet">An <see cref="IList{T}"/> of <see cref="Instruction"/>s to run.</param>
        /// <param name="errors">An <see cref="IList{T}"/> to receive <see cref="SyntaxError"/>s.</param>
        void Prepare(IList<Instruction> instructionSet, IList<SyntaxError> errors);
        /// <summary>
        /// Runs the instructions and writes the output to a <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="dataSet">An <see cref="IList{T}"/> of <see cref="long"/> data values to run over.</param>
        /// <param name="writer">A <see cref="TextWriter"/> to write the output to.</param>
        /// <param name="terminate">Optional function that returns true when the execution should be terminated.</param>
        void Run(IList<long> dataSet, TextWriter writer, Func<bool> terminate);
        /// <summary>
        /// Runs the instructions and writes the output to a <see cref="TextWriter"/> asynchronously.
        /// </summary>
        /// <param name="dataSet">An <see cref="IList{T}"/> of <see cref="long"/> data values to run over.</param>
        /// <param name="writer">A <see cref="TextWriter"/> to write the output to.</param>
        /// <param name="terminate">Optional function that returns true when the execution should be terminated.</param>
        /// <param name="cancellationToken">A token to cancel the run.</param>
        Task RunAsync(IList<long> dataSet, TextWriter writer, Func<bool> terminate, CancellationToken cancellationToken);
    }
}
