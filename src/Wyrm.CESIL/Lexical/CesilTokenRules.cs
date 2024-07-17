using System.Collections.Generic;
using Wyrm.CESIL.Exceptions;

namespace Wyrm.CESIL.Lexical
{
    public class CesilTokenRules : ITokenMatcher
    {
        private readonly List<TokenRule> rules;

        public CesilTokenRules()
        {
            rules = new List<TokenRule>
            {
                new TokenRule { TokenType = TokenType.Comment, CharMatch = (chr) => chr == '*' || chr == '(', PosMatch = (pos) => pos == 0 },
                new TokenRule { TokenType = TokenType.End, PreceedingType = new [] { TokenType.Eol }, CharMatch = (chr) => chr == '%' },
                new TokenRule { TokenType = TokenType.Integer, CharMatch = (chr) => char.IsDigit(chr) || chr == '-' || chr == '+' },
                new TokenRule { TokenType = TokenType.Label, CharMatch = (chr) => char.IsLetter(chr), PosMatch = (pos) => pos == 0 },
                new TokenRule { TokenType = TokenType.Variable, PreceedingType = new [] { TokenType.Instruction }, CharMatch = (chr) => char.IsLetter(chr) },
                new TokenRule { TokenType = TokenType.Instruction, CharMatch = (chr) => char.IsLetter(chr) },
                new TokenRule { TokenType = TokenType.String, CharMatch = (chr) => chr == '"' }
            };
        }

        public TokenType MatchToken(TokenType previousTokenType, char tokenChar, int charPos)
        {
            foreach (var rule in rules)
            {
                if (rule.Match(previousTokenType, tokenChar, charPos)) return rule.TokenType;
            }
            throw new SyntaxException();
        }
    }
}
