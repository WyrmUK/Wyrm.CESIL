namespace Wyrm.CESIL.Lexical
{
    /// <summary>
    /// An enumeration of CESIL token types.
    /// </summary>
    public enum TokenType
    {
        /// <summary>
        /// End of line
        /// </summary>
        Eol,
        /// <summary>
        /// Comment
        /// </summary>
        Comment,
        /// <summary>
        /// End
        /// </summary>
        End,
        /// <summary>
        /// Integer value
        /// </summary>
        Integer,
        /// <summary>
        /// Label
        /// </summary>
        Label,
        /// <summary>
        /// Variable name
        /// </summary>
        Variable,
        /// <summary>
        /// Instruction
        /// </summary>
        Instruction,
        /// <summary>
        /// String value
        /// </summary>
        String
    }
}
