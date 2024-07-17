using System;
using System.Linq;

namespace Wyrm.CESIL.Lexical
{
    internal class TokenRule
    {
        public TokenType TokenType { get; set; }
        public TokenType[] PreceedingType { get; set; }
        public Func<char, bool> CharMatch { get; set; }
        public Func<int, bool> PosMatch { get; set; }
        public bool Match(TokenType preceedingTokenType, char tokenChar, int charPos)
        {
            if (PreceedingType != null && PreceedingType.Length > 0 && !PreceedingType.Any(t => t == preceedingTokenType)) return false;
            if (CharMatch != null && !CharMatch(tokenChar)) return false;
            if (PosMatch != null && !PosMatch(charPos)) return false;
            return true;
        }
    }
}
