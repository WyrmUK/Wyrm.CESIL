namespace Wyrm.CESIL.Lexical
{
    public class Token
    {
        public Token(int lineNo, int charNo, TokenType tokenType, string value)
        {
            LineNo = lineNo;
            CharNo = charNo;
            TokenType = tokenType;
            Value = value;
        }
        public int LineNo { get; }
        public int CharNo { get; }
        public TokenType TokenType { get; }
        public string Value { get; }
    }
}
