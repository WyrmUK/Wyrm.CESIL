using Shouldly;
using Wyrm.CESIL.Lexical;

namespace Wyrm.CESIL.UnitTests.Lexical;

public class TokenUnitTests
{
    [Fact]
    public void Token_Constructor_Should_Set_Values()
    {
        const int lineNo = 101;
        const int charNo = 18;
        const TokenType tokenType = TokenType.Comment;
        const string value = "## Comment";
        var token = new Token(lineNo, charNo, tokenType, value);
        token.LineNo.ShouldBe(lineNo);
        token.CharNo.ShouldBe(charNo);
        token.TokenType.ShouldBe(tokenType);
        token.Value.ShouldBe(value);
    }
}
