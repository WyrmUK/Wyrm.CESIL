using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Wyrm.CESIL.Parsing;

namespace Wyrm.CESIL.Executing
{
    /// <summary>
    /// An interface to an operator for running instructions.
    /// </summary>
    public interface IOperator
    {
        /// <summary>
        /// Operates an <see cref="Instruction"/>.
        /// </summary>
        /// <param name="instruction">The <see cref="Instruction"/> to operate.</param>
        /// <param name="state">The current state of the program operation as an <see cref="IOperationState"/>.</param>
        /// <param name="writer"></param>
        /// <exception cref="System.NotSupportedException">Thrown if the instruction is not supported.</exception>
        /// <exception cref="Exceptions.IllegalOperationException">Thrown if the operation is invalid.</exception>
        /// <exception cref="Exceptions.IllegalLabelException">Thrown if the label is not found.</exception>
        /// <exception cref="Exceptions.NotInitialisedException">Thrown if the state has not been initialised.</exception>
        void Operate(Instruction instruction, IOperationState state, TextWriter writer);
        /// <summary>
        /// Operates an <see cref="Instruction"/>.
        /// </summary>
        /// <param name="instruction">The <see cref="Instruction"/> to operate.</param>
        /// <param name="state">The current state of the program operation as an <see cref="IOperationState"/>.</param>
        /// <param name="writer"></param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <exception cref="System.NotSupportedException">Thrown if the instruction is not supported.</exception>
        /// <exception cref="Exceptions.IllegalOperationException">Thrown if the operation is invalid.</exception>
        /// <exception cref="Exceptions.IllegalLabelException">Thrown if the label is not found.</exception>
        /// <exception cref="Exceptions.NotInitialisedException">Thrown if the state has not been initialised.</exception>
        Task OperateAsync(Instruction instruction, IOperationState state, TextWriter writer, CancellationToken cancellationToken);
    }
}
