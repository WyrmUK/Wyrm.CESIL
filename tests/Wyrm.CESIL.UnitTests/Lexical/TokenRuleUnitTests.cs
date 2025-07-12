using Shouldly;
using Wyrm.CESIL.Lexical;

namespace Wyrm.CESIL.UnitTests.Lexical;

public class TokenRuleUnitTests
{
    [Theory]
    [MemberData(nameof(MatchTheoryData))]
    internal void Match_Should_Return_Expected(object tokenRule, TokenType preceedingTokenType, char tokenChar, int charPos, bool expected)
    {
        ((tokenRule as TokenRule)?.Match(preceedingTokenType, tokenChar, charPos) ?? !expected).ShouldBe(expected);
    }

    #region Test Data

    public static readonly TheoryData<object, TokenType, char, int, bool> MatchTheoryData = new()
    {
        { new TokenRule(), TokenType.Comment, '#', 1, true },
        { new TokenRule { PreceedingType = Array.Empty<TokenType>() }, TokenType.Comment, '#', 1, true },
        { new TokenRule { PosMatch = p => p == 10 }, TokenType.Label, 'X', 10, true },
        { new TokenRule { PosMatch = p => p == 10 }, TokenType.Label, 'X', 9, false },
        { new TokenRule { CharMatch = c => c == 'X' }, TokenType.Variable, 'X', 1, true },
        { new TokenRule { CharMatch = c => c == 'X' }, TokenType.Variable, 'Y', 1, false },
        { new TokenRule { PreceedingType = new[] { TokenType.Label, TokenType.Instruction } }, TokenType.Label, 'X', 1, true },
        { new TokenRule { PreceedingType = new[] { TokenType.Label, TokenType.Instruction } }, TokenType.Variable, 'X', 1, false }
    };

    #endregion
}
