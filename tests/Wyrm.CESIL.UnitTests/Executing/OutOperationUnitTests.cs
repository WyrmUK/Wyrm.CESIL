using Moq;
using System.Text;
using Wyrm.CESIL.Executing;

namespace Wyrm.CESIL.UnitTests.Executing
{
    public class OutOperationUnitTests
    {
        [Fact]
        public void Execute_Should_Write_Accumulator()
        {
            InitialiseMock();
            var operation = new OutOperation();
            operation.Execute(null, _operationState, _writer);
            Mock.Get(_writer)
                .Verify(x => x.Write(InitialAccumulatorValue.ToString()), Times.Once);
            Mock.Get(_operationState)
                .VerifySet(x => x.Instruction = InitialInstructionValue + 1, Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_Should_Write_Accuulator()
        {
            InitialiseMock();
            var operation = new OutOperation();
            await operation.ExecuteAsync(null, _operationState, _writer, CancellationToken);
            Mock.Get(_writer)
                .Verify(x => x.WriteAsync(It.Is<StringBuilder>(b => b.ToString() == InitialAccumulatorValue.ToString()), CancellationToken), Times.Once);
            Mock.Get(_operationState)
                .VerifySet(x => x.Instruction = InitialInstructionValue + 1, Times.Once);
        }

        #region Test Data

        private const long InitialAccumulatorValue = 20L;
        private const int InitialInstructionValue = 1;
        private static readonly CancellationToken CancellationToken = new CancellationTokenSource().Token;

        #endregion

        #region Test Mocks

        private readonly IOperationState _operationState = Mock.Of<IOperationState>();
        private readonly TextWriter _writer = Mock.Of<TextWriter>();

        private void InitialiseMock()
        {
            Mock.Get(_operationState)
                .Setup(x => x.Accumulator)
                .Returns(InitialAccumulatorValue);
            Mock.Get(_operationState)
                .Setup(x => x.Instruction)
                .Returns(InitialInstructionValue);
        }

        #endregion
    }
}
