using System.Collections.Generic;
using Wyrm.CESIL.Exceptions;

namespace Wyrm.CESIL.Lexical
{
    /// <summary>
    /// A class providing matches for CESIL token types.
    /// </summary>
    public class CesilTokenRules : ITokenMatcher
    {
        private static readonly List<TokenRule> _rules = new List<TokenRule>
        {
            new TokenRule { TokenType = TokenType.Comment, CharMatch = chr => chr == '*' || chr == '(', PosMatch = pos => pos == 0 },
            new TokenRule { TokenType = TokenType.End, PreceedingType = new [] { TokenType.Eol }, CharMatch = chr => chr == '%' },
            new TokenRule { TokenType = TokenType.Integer, CharMatch = chr => char.IsDigit(chr) || chr == '-' || chr == '+' },
            new TokenRule { TokenType = TokenType.Label, CharMatch = char.IsLetter, PosMatch = pos => pos == 0 },
            new TokenRule { TokenType = TokenType.Variable, PreceedingType = new [] { TokenType.Instruction }, CharMatch = char.IsLetter },
            new TokenRule { TokenType = TokenType.Instruction, CharMatch = char.IsLetter },
            new TokenRule { TokenType = TokenType.String, CharMatch = chr => chr == '"' }
        };

        /// <inheritdoc/>
        public TokenType MatchToken(TokenType previousTokenType, char tokenChar, int charPos)
        {
            foreach (var rule in _rules)
            {
                if (rule.Match(previousTokenType, tokenChar, charPos)) return rule.TokenType;
            }
            throw new SyntaxException();
        }
    }
}
