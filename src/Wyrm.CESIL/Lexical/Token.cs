namespace Wyrm.CESIL.Lexical
{
    /// <summary>
    /// A class representing a parsed <see cref="Token"/>.
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Creates a new <see cref="Token"/>.
        /// </summary>
        /// <param name="lineNo">The line number of the token.</param>
        /// <param name="charNo">the character number in the line.</param>
        /// <param name="tokenType">The <see cref="TokenType"/> of the token.</param>
        /// <param name="value">The value of the token.</param>
        public Token(int lineNo, int charNo, TokenType tokenType, string value)
        {
            LineNo = lineNo;
            CharNo = charNo;
            TokenType = tokenType;
            Value = value;
        }
        /// <summary>
        /// Gets the line number for the token.
        /// </summary>
        public int LineNo { get; }
        /// <summary>
        /// Gets the character number in the line for the token.
        /// </summary>
        public int CharNo { get; }
        /// <summary>
        /// Gets the <see cref="TokenType"/> of the token.
        /// </summary>
        public TokenType TokenType { get; }
        /// <summary>
        /// Gets the value of the token.
        /// </summary>
        public string Value { get; }
    }
}
