namespace Wyrm.CESIL.Executing
{
    /// <summary>
    /// An interface to an operation state.
    /// </summary>
    public interface IOperationState
    {
        /// <summary>
        /// Pops a data item from the data set.
        /// </summary>
        /// <returns>A <see cref="long"/> data item.</returns>
        /// <exception cref="Exceptions.NoDataException">Thrown if there is no more data.</exception>
        long PopData();
        /// <summary>
        /// Gets the instruction index for a label.
        /// </summary>
        /// <param name="label">The label to find.</param>
        /// <returns>An <see cref="int"/> instruction index.</returns>
        /// <exception cref="Exceptions.IllegalLabelException">Thrown if the label is not found.</exception>
        int InstructionFor(string label);
        /// <summary>
        /// Gets and sets the accumulator value.
        /// </summary>
        long Accumulator { get; set; }
        /// <summary>
        /// Gets and sets the current instruction index.
        /// </summary>
        int Instruction { get; set; }
        /// <summary>
        /// True if the program has halted.
        /// </summary>
        bool Halted { get; set; }
        /// <summary>
        /// Gets and sets variable values.
        /// </summary>
        /// <param name="variable">The name of the variable to get or set.</param>
        /// <returns>The <see cref="long"/> value of the variable.</returns>
        /// <exception cref="Exceptions.NotInitialisedException">Thrown if the variable hasn't been written to when getting.</exception>
        long this[string variable] { get; set; }
    }
}
