using Wyrm.CESIL.Executing;
using Wyrm.CESIL.IntegrationTests.TestHelpers;
using Wyrm.CESIL.Lexical;
using Wyrm.CESIL.Parsing;

namespace Wyrm.CESIL.IntegrationTests;

public class InterpreterIntegrationTests
{
    [Theory]
    [MemberData(nameof(ProgramFileTheoryData))]
    public void Load_And_Run_Should_Get_Correct_Results(string filename)
    {
        _interpreter.Load(ProgramReader(filename));
        _interpreter.Run(Writer, () => false);
        Writer.ShouldHaveWritten(filename);
    }

    [Theory]
    [MemberData(nameof(ProgramFileTheoryData))]
    public async Task LoadAsync_And_RunAsync_Should_Get_Correct_Results(string filename)
    {
        await _interpreter.LoadAsync(ProgramReader(filename));
        await _interpreter.RunAsync(Writer, () => false);
        Writer.ShouldHaveWritten(filename);
    }

    #region Test Helpers

    private readonly IInterpreter _interpreter = new Interpreter(new Analyser(new CesilTokenRules()), new Parser(new CesilInstructionBuilder()), new Executor(new CesilOperator(), new OperationStateFactory()));

    private static TextReader ProgramReader(string filename) =>
        File.OpenText($"Examples/{filename}.txt");

    private readonly StringWriter Writer = new StringWriter();

    #endregion

    #region Test Data

    public static readonly TheoryData<string> ProgramFileTheoryData = new()
    {
        "CesilProgram1",
        "CesilProgram2",
        "CesilProgram3",
        "CesilProgram4",
        "CesilProgram5",
        "CesilProgram6",
        "CesilProgram7",
        "CesilProgram8",
        "CesilProgram9",
        "CesilProgram10",
        "CesilProgram11",
        "CesilProgram12",
        "CesilProgram13",
        "CesilProgram14"
    };

    #endregion
}
