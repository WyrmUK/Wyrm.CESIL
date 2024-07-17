using System.Collections.Generic;

namespace Wyrm.CESIL.Executing
{
    /// <summary>
    /// An interface to a factory for creating <see cref="IOperationState"/>s.
    /// </summary>
    public interface IOperationStateFactory
    {
        /// <summary>
        /// Creates a new <see cref="IOperationState"/>.
        /// </summary>
        /// <param name="dataSet">An <see cref="IList{T}"/> of <see cref="long"/> data values.</param>
        /// <param name="labels">An <see cref="IDictionary{TKey, TValue}"/> of labels.</param>
        /// <returns>A new <see cref="IOperationState"/> instance.</returns>
        IOperationState CreateOperationState(IList<long> dataSet, IDictionary<string, int> labels);
    }
}
