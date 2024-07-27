using Moq;
using Shouldly;
using Wyrm.CESIL.Exceptions;
using Wyrm.CESIL.Lexical;
using Wyrm.CESIL.Parsing;

namespace Wyrm.CESIL.UnitTests.Parsing;

public class ParserTests
{
    [Fact]
    public void Parse_Should_Parse_Tokens()
    {
        InitialiseMock();
        var parser = new Parser(_instructionBuilder);
        var data = new List<long>();
        var errors = new List<SyntaxError>();
        var instructions = parser.Parse(_tokens, data, errors);
        instructions.ShouldBeEquivalentTo(_expectedInstructions);
        data.ShouldBeEquivalentTo(_expectedData);
        errors.ShouldBeEquivalentTo(_expectedErrors);
    }

    #region Test Data

    private static readonly List<Token> _tokens = new()
    {
        new Token(0, 0, TokenType.Eol, null),
        new Token(1, 0, TokenType.Label, "LBL"),
        new Token(1, 4, TokenType.Instruction, "IN"),
        new Token(1, 0, TokenType.Eol, null),
        new Token(2, 4, TokenType.Instruction, "%"),
        new Token(2, 0, TokenType.Eol, null),
        new Token(3, 4, TokenType.Integer, "10"),
        new Token(4, 1, TokenType.Instruction, "SyntaxException"),
        new Token(5, 2, TokenType.Instruction, "NotSupportedException"),
        new Token(6, 3, TokenType.Instruction, "IncompleteInstructionException"),
        new Token(7, 4, TokenType.Instruction, "IllegalIntegerException"),
        new Token(7, 4, TokenType.Instruction, "IllegalDataException"),
        new Token(7, 4, TokenType.Instruction, "IllegalInstructionException"),
        new Token(7, 4, TokenType.Instruction, "IllegalLabelException"),
        new Token(7, 4, TokenType.Instruction, "IllegalLocationException"),
        new Token(8, 0, TokenType.Instruction, "JUMP"),
    };

    private static readonly List<Instruction> _expectedInstructions = new()
    {
        new Instruction(1) { InstructionType = InstructionType.IN, Label = "LBL" },
        new Instruction(2) { InstructionType = InstructionType.HALT }
    };

    private static readonly List<long> _expectedData = new()
    {
        10L
    };

    private static readonly List<SyntaxError> _expectedErrors = new()
    {
        new SyntaxError(4, 1, "Syntax error"),
        new SyntaxError(5, 2, "Unsupported token"),
        new SyntaxError(6, 3, "Incomplete instruction"),
        new SyntaxError(7, 4, "Illegal integer"),
        new SyntaxError(7, 4, "Illegal data"),
        new SyntaxError(7, 4, "Illegal instruction"),
        new SyntaxError(7, 4, "Illegal label"),
        new SyntaxError(7, 4, "Illegal variable or label"),
        new SyntaxError(-1, -1, "Unterminated program")
    };

    #endregion

    #region Test Mocks

    private readonly IInstructionBuilder _instructionBuilder = Mock.Of<IInstructionBuilder>();

    delegate bool BuildInstructionCallback(Token token, ref Instruction instruction, bool isData);

    private void InitialiseMock()
    {
        Mock.Get(_instructionBuilder)
            .Setup(x => x.BuildInstruction(It.IsAny<Token>(), ref It.Ref<Instruction>.IsAny, It.IsAny<bool>()))
            .Returns(new BuildInstructionCallback((Token token, ref Instruction instruction, bool isData) =>
            {
                if (token.TokenType == TokenType.Eol) return true;
                if (token.TokenType == TokenType.Label)
                {
                    instruction = new Instruction(token.LineNo) { Label = token.Value };
                    return false;
                }
                if (token.TokenType == TokenType.Instruction)
                {
                    if (instruction == null) instruction = new Instruction(token.LineNo);
                    if (token.Value == "IN") instruction.InstructionType = InstructionType.IN;
                    if (token.Value == "%") instruction.InstructionType = null;
                    if (token.Value == "SyntaxException") throw new SyntaxException();
                    if (token.Value == "NotSupportedException") throw new NotSupportedException();
                    if (token.Value == "IncompleteInstructionException") throw new IncompleteInstructionException();
                    if (token.Value == "IllegalIntegerException") throw new IllegalIntegerException();
                    if (token.Value == "IllegalDataException") throw new IllegalDataException();
                    if (token.Value == "IllegalInstructionException") throw new IllegalInstructionException();
                    if (token.Value == "IllegalLabelException") throw new IllegalLabelException();
                    if (token.Value == "IllegalLocationException") throw new IllegalLocationException();
                    if (token.Value == "JUMP") instruction.InstructionType = InstructionType.JUMP;
                    return false;
                }
                if (token.TokenType == TokenType.Integer)
                {
                    instruction = new Instruction(token.LineNo) { Value = long.Parse(token.Value) };
                    return true;
                }
                return false;
            }));
    }

    #endregion
}
