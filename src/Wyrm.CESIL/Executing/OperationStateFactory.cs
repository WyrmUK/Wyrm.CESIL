using System.Collections.Generic;

namespace Wyrm.CESIL.Executing
{
    /// <summary>
    /// A class implementing an <see cref="IOperationStateFactory"/>.
    /// </summary>
    public class OperationStateFactory : IOperationStateFactory
    {
        /// <inheritdoc/>
        public IOperationState CreateOperationState(IList<long> dataSet, IDictionary<string, int> labels) =>
            new OperationState(dataSet, labels);
    }
}
