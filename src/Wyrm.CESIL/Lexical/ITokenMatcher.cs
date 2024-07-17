namespace Wyrm.CESIL.Lexical
{
    public interface ITokenMatcher
    {
        TokenType MatchToken(TokenType previousTokenType, char tokenChar, int charPos);
    }
}
