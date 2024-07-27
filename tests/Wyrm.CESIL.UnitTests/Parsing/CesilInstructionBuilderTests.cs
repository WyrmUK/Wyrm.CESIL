using Shouldly;
using Wyrm.CESIL.Lexical;
using Wyrm.CESIL.Parsing;

namespace Wyrm.CESIL.UnitTests.Parsing;

public class CesilInstructionBuilderTests
{
    [Fact]
    public void BuildInstruction_Should_Throw_NotSupportedException_If_Token_Not_Supported()
    {
        Instruction instruction = new Instruction(1);
        Should.Throw<NotSupportedException>(() => new CesilInstructionBuilder().BuildInstruction(
            new Token(1, 1, (TokenType)(-1), "bad"), ref instruction, false));
    }

    [Theory]
    [MemberData(nameof(BuildInstructionTheoryData))]
    public void BuildInstruction_Should_Parse_Token_Correctly(Token token, bool isData, bool createInstruction, bool expectedResult, Instruction? expectedInstruction)
    {
        Instruction? instruction = createInstruction ? new Instruction(token.LineNo) : null;
        var result = new CesilInstructionBuilder().BuildInstruction(token, ref instruction, isData);
        result.ShouldBe(expectedResult);
        instruction.ShouldBeEquivalentTo(expectedInstruction);
    }

    #region Test Data

    public static readonly TheoryData<Token, bool, bool, bool, Instruction?> BuildInstructionTheoryData = new ()
    {
        { new Token(1, 1, TokenType.Eol, "\r\n"), false, true, true, new Instruction(1) },
        { new Token(2, 1, TokenType.Comment, "** Comment"), false, false, true, null },
        { new Token(3, 1, TokenType.End, "*"), false, false, true, new Instruction(3) },
        { new Token(4, 1, TokenType.Integer, "10" ), true, false, true, new Instruction(4) { Value = 10L } },
        { new Token(5, 1, TokenType.Label, "LBL"), false, false, false, new Instruction(5) { Label = "LBL" } },
        { new Token(6, 1, TokenType.Variable, "VAR"), false, true, false, new Instruction(6) { Value = "VAR" } },
        { new Token(7, 1, TokenType.Instruction, "IN"), false, false, false, new Instruction(7) { InstructionType = InstructionType.IN } },
        { new Token(8, 1, TokenType.String, "STR"), false, true, false, new Instruction(8) { Value = "STR" } }
    };

    #endregion
}
