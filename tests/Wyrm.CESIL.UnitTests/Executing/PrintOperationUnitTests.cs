using Moq;
using Shouldly;
using System.Text;
using Wyrm.CESIL.Exceptions;
using Wyrm.CESIL.Executing;

namespace Wyrm.CESIL.UnitTests.Executing
{
    public class PrintOperationUnitTests
    {
        [Theory]
        [InlineData("\"Text\"", "Text")]
        [InlineData("\"Text\"\"Line\"", "Text\"Line")]
        public void Execute_Should_Write_Text(string value, string expected)
        {
            InitialiseMock();
            var operation = new PrintOperation();
            operation.Execute(value, _operationState, _writer);
            Mock.Get(_writer)
                .Verify(x => x.Write(expected), Times.Once);
            Mock.Get(_operationState)
                .VerifySet(x => x.Instruction = InitialInstructionValue + 1, Times.Once);
        }

        [Fact]
        public void Execute_Should_Throw_IllegalOperationException_For_Unknown_Type()
        {
            InitialiseMock();
            var operation = new PrintOperation();
            Should.Throw<IllegalOperationException>(() => operation.Execute(new DateTime(), _operationState, _writer));
        }

        [Theory]
        [InlineData("\"Text\"", "Text")]
        [InlineData("\"Text\"\"Line\"", "Text\"Line")]
        public async Task ExecuteAsync_Should_Write_Text(string value, string expected)
        {
            InitialiseMock();
            var operation = new PrintOperation();
            await operation.ExecuteAsync(value, _operationState, _writer, CancellationToken);
            Mock.Get(_writer)
                .Verify(x => x.WriteAsync(It.Is<StringBuilder>(b => b.ToString() == expected), CancellationToken), Times.Once);
            Mock.Get(_operationState)
                .VerifySet(x => x.Instruction = InitialInstructionValue + 1, Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_Should_Throw_IllegalOperationException_For_Unknown_Type()
        {
            InitialiseMock();
            var operation = new PrintOperation();
            await Should.ThrowAsync<IllegalOperationException>(() => operation.ExecuteAsync(new DateTime(), _operationState, _writer, CancellationToken.None));
        }

        #region Test Data

        private const int InitialInstructionValue = 1;
        private static readonly CancellationToken CancellationToken = new CancellationTokenSource().Token;

        #endregion

        #region Test Mocks

        private readonly IOperationState _operationState = Mock.Of<IOperationState>();
        private readonly TextWriter _writer = Mock.Of<TextWriter>();

        private void InitialiseMock()
        {
            Mock.Get(_operationState)
                .Setup(x => x.Instruction)
                .Returns(InitialInstructionValue);
        }

        #endregion
    }
}
