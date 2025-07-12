using Moq;
using Shouldly;
using Wyrm.CESIL.Executing;

namespace Wyrm.CESIL.UnitTests.Executing
{
    public class LineOperationUnitTests
    {
        [Fact]
        public void Execute_Should_Write_Line()
        {
            InitialiseMock();
            var operation = new LineOperation();
            operation.Execute(null, _operationState, _writer);
            Mock.Get(_writer)
                .Verify(x => x.WriteLine(), Times.Once);
            Mock.Get(_operationState)
                .VerifySet(x => x.Instruction = InitialInstructionValue + 1, Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_Should_Write_Line()
        {
            InitialiseMock();
            var operation = new LineOperation();
            await operation.ExecuteAsync(null, _operationState, _writer, CancellationToken.None);
            Mock.Get(_writer)
                .Verify(x => x.WriteLineAsync(), Times.Once);
            Mock.Get(_operationState)
                .VerifySet(x => x.Instruction = InitialInstructionValue + 1, Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_Should_Handle_Cancellation()
        {
            InitialiseMock();
            var operation = new LineOperation();
            await Should.ThrowAsync<TaskCanceledException>(() => operation.ExecuteAsync(null, _operationState, _writer, new CancellationToken(true)));
        }

        #region Test Data

        private const int InitialInstructionValue = 1;

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
