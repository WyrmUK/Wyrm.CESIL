using Shouldly;
using Wyrm.CESIL.Exceptions;
using Wyrm.CESIL.Lexical;
using Wyrm.CESIL.Parsing;

namespace Wyrm.CESIL.UnitTests.Parsing;

public class BuildRuleUnitTests
{
    [Theory]
    [MemberData(nameof(BuildTheoryData))]
    public void Build_Should_Get_Expected_Values(object buildRule, bool isData, Instruction? instruction, Token? token, bool? expectedResult, Instruction? expectedInstruction, Type? expectedExceptionType)
    {
        if (expectedExceptionType != null)
        {
            Should.Throw<Exception>(() => (buildRule as BuildRule)?.Build(isData, ref instruction, token))
                .ShouldBeOfType(expectedExceptionType);
        }
        else
        {
            ((buildRule as BuildRule)?.Build(isData, ref instruction, token))
                .ShouldBe(expectedResult);
            instruction.ShouldBeEquivalentTo(expectedInstruction);
        }
    }

    #region Test Data

    private const int LineNo = 11;
    private const long TestInteger = 123;
    private const string TestLabel = "LB1";
    private const string TestVariable = "VAR1";
    private const string TestString = "\"Hello World\"";
    private const string StartString = "\"Test \"";

    public static readonly TheoryData<object, bool, Instruction?, Token?, bool?, Instruction?, Type?> BuildTheoryData = new ()
    {
        { new BuildRule { IsData = false, CanBeData = false, InDataSection = false }, true, null, null, null, null, typeof(IllegalIntegerException) },
        { new BuildRule { CanBeData = true }, true, new Instruction(LineNo), null, null, null, typeof(SyntaxException) },
        { new BuildRule { CanBeData = true }, false, null, null, null, null, typeof(IllegalInstructionException) },
        { new BuildRule { CanBeData = true }, false, new Instruction(LineNo), null, null, null, typeof(IllegalInstructionException) },
        { new BuildRule { AddsToInstruction = true }, false, null, null, null, null, typeof(IllegalInstructionException) },
        { new BuildRule { StartsInstruction = true }, false, new Instruction(LineNo), null, null, null, typeof(IncompleteInstructionException) },
        { new BuildRule { IsInstruction = true }, false, new Instruction(LineNo) { InstructionType = InstructionType.LINE }, null, null, null, typeof(IncompleteInstructionException) },
        { new BuildRule { IsInteger = true }, false, new Instruction(LineNo), new Token(LineNo, 0, TokenType.Integer, "Bad"), null, null, typeof(IllegalIntegerException) },
        { new BuildRule { IsLabel = true }, false, new Instruction(LineNo), new Token(LineNo, 0, TokenType.Label, "$1"), null, null, typeof(IllegalLabelException) },
        { new BuildRule { IsVariable = true }, false, new Instruction(LineNo), new Token(LineNo, 10, TokenType.Variable, "=1"), null, null, typeof(IllegalLocationException) },
        { new BuildRule { IsString = true }, false, new Instruction(LineNo) { Value = 10 }, null, null, null, typeof(SyntaxException) },
        { new BuildRule { IsString = true }, false, new Instruction(LineNo) { Value = "Bad\"" }, null, null, null, typeof(BadStringException) },
        { new BuildRule { IsString = true }, false, new Instruction(LineNo) { Value = "\"Bad" }, null, null, null, typeof(BadStringException) },
        { new BuildRule { IsInstruction = true, NullInstruction = false }, false, new Instruction(LineNo), new Token(LineNo, 1, TokenType.Instruction, "Bad"), null, null, typeof(IllegalInstructionException) },
        { new BuildRule { CreatesInstruction = true, IsInteger = true }, false, null, new Token(LineNo, 10, TokenType.Integer, TestInteger.ToString()), false, new Instruction(LineNo) { Value = TestInteger }, null },
        { new BuildRule { IsLabel = true }, false, new Instruction(LineNo), new Token(LineNo, 0, TokenType.Label, TestLabel), false, new Instruction(LineNo) { Label = TestLabel }, null },
        { new BuildRule { IsVariable = true }, false, new Instruction(LineNo), new Token(LineNo, 10, TokenType.Variable, TestVariable), false, new Instruction(LineNo) { Value = TestVariable }, null },
        { new BuildRule { IsString = true }, false, new Instruction(LineNo), new Token(LineNo, 10, TokenType.String, TestString), false, new Instruction(LineNo) { Value = TestString }, null },
        { new BuildRule { IsString = true }, false, new Instruction(LineNo) { Value = StartString }, new Token(LineNo, 10, TokenType.String, TestString), false, new Instruction(LineNo) { Value = $"{StartString}{TestString}" }, null },
        { new BuildRule { IsInstruction = true, NullInstruction = false }, false, new Instruction(LineNo), new Token(LineNo, 1, TokenType.Instruction, "LINE"), false, new Instruction(LineNo) { InstructionType = InstructionType.LINE }, null },
        { new BuildRule { CreatesInstruction = true, CanBeData = true, IsInteger = true }, true, null, new Token(LineNo, 0, TokenType.Integer, TestInteger.ToString()), true, new Instruction(LineNo) { Value = TestInteger }, null },
        { new BuildRule { CreatesInstruction = true, Ends = true }, false, null, new Token(LineNo, 0, TokenType.End, "%"), true, new Instruction(LineNo), null }
    };

    #endregion
}
