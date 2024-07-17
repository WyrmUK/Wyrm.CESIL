namespace Wyrm.CESIL.Lexical
{
    /// <summary>
    /// Class representing a syntax error.
    /// </summary>
    public class SyntaxError
    {
        /// <summary>
        /// Creates a new <see cref="SyntaxError"/>.
        /// </summary>
        /// <param name="lineNo">The line number.</param>
        /// <param name="charNo">The character number.</param>
        /// <param name="message">The error message.</param>
        public SyntaxError(int lineNo, int charNo, string message)
        {
            LineNo = lineNo;
            CharNo = charNo;
            Message = message;
        }
        /// <summary>
        /// Gets the line number of the error.
        /// </summary>
        public int LineNo { get; }
        /// <summary>
        /// Gets the character number where the error occurred.
        /// </summary>
        public int CharNo { get; }
        /// <summary>
        /// Gets the error message.
        /// </summary>
        public string Message { get; set; }
    }
}
