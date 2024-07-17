namespace Wyrm.CESIL.Lexical
{
    /// <summary>
    /// An interface to a Token Matcher.
    /// </summary>
    public interface ITokenMatcher
    {
        /// <summary>
        /// Matches a token type given the previous token type and the character to match.
        /// </summary>
        /// <param name="previousTokenType">The <see cref="TokenType"/> of the previous token.</param>
        /// <param name="tokenChar">The character of the token.</param>
        /// <param name="charPos">The position of the character in the line to match.</param>
        /// <returns>The <see cref="TokenType"/> of the character.</returns>
        /// <exception cref="Exceptions.SyntaxException">Thrown if there is no token type match.</exception>
        /// <exception cref="Exceptions.UnterminatedStringException">Thrown if a string is not terminated with a double quote.</exception>
        TokenType MatchToken(TokenType previousTokenType, char tokenChar, int charPos);
    }
}
