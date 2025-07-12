using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Wyrm.CESIL.Parsing;

namespace Wyrm.CESIL.Executing
{
    /// <summary>
    /// A class for operating CESIL instructions.
    /// </summary>
    public class CesilOperator : IOperator
    {
        private readonly IOperationFactory _operationFactory;

        /// <summary>
        /// Creates a new <see cref="CesilOperator"/>.
        /// </summary>
        public CesilOperator() : this(new OperationFactory())
        {
        }

        internal CesilOperator(IOperationFactory operationFactory)
        {
            _operationFactory = operationFactory;
        }

        /// <inheritdoc/>
        public void Operate(Instruction instruction, IOperationState state, TextWriter writer)
        {
            var operation = _operationFactory.CreateOperation(instruction);
            operation.Execute(instruction.Value, state, writer);
        }

        /// <inheritdoc/>
        public Task OperateAsync(Instruction instruction, IOperationState state, TextWriter writer, CancellationToken cancellationToken)
        {
            var operation = _operationFactory.CreateOperation(instruction);
            return operation.ExecuteAsync(instruction.Value, state, writer, cancellationToken);
        }
    }
}
