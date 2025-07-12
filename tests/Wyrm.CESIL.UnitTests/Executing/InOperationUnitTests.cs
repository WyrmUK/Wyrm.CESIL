using Moq;
using Wyrm.CESIL.Executing;

namespace Wyrm.CESIL.UnitTests.Executing
{
    public class InOperationUnitTests
    {
        [Fact]
        public async Task ExecuteAsync_Should_Pop_To_Accumulator()
        {
            InitialiseMock();
            var operation = new InOperation();
            await operation.ExecuteAsync(null, _operationState, _writer, CancellationToken.None);
            Mock.Get(_operationState)
                .VerifySet(x => x.Accumulator = PopValue, Times.Once);
            Mock.Get(_operationState)
                .VerifySet(x => x.Instruction = InitialInstructionValue + 1, Times.Once);
        }

        #region Test Data

        private const long PopValue = 20L;
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
            Mock.Get(_operationState)
                .Setup(x => x.PopData())
                .Returns(PopValue);
        }

        #endregion
    }
}
