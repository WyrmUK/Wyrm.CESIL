namespace Wyrm.CESIL.Lexical
{
    public class SyntaxError
    {
        public SyntaxError(int lineNo, int charNo, string message)
        {
            LineNo = lineNo;
            CharNo = charNo;
            Message = message;
        }
        public int LineNo { get; }
        public int CharNo { get; }
        public string Message { get; set; }
    }
}
