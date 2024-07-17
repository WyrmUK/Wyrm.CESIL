namespace Wyrm.CESIL.Parsing
{
    /// <summary>
    /// Represents an instruction.
    /// </summary>
    public class Instruction
    {
        /// <summary>
        /// Creates a new <see cref="Instruction"/> instance.
        /// </summary>
        /// <param name="lineNo">The line number of the instruction.</param>
        public Instruction(int lineNo)
        {
            LineNo = lineNo;
        }
        /// <summary>
        /// Gets the line number of the instruction.
        /// </summary>
        public int LineNo { get; }
        /// <summary>
        /// Gets and sets any label for the instruction.
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// Gets and sets the <see cref="InstructionType"/> if applicable.
        /// </summary>
        public InstructionType? InstructionType { get; set; }
        /// <summary>
        /// Gets and sets a value.
        /// </summary>
        public object Value { get; set; }
    }
}
