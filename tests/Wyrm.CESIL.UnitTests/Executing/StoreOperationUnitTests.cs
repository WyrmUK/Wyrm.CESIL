using Moq;
using Shouldly;
using Wyrm.CESIL.Exceptions;
using Wyrm.CESIL.Executing;

namespace Wyrm.CESIL.UnitTests.Executing
{
    public class StoreOperationUnitTests
    {
        [Fact]
        public async Task ExecuteAsync_Should_Store_Accumulator()
        {
            InitialiseMock();
            var operation = new StoreOperation();
            await operation.ExecuteAsync(StoreName, _operationState, _writer, CancellationToken.None);
            Mock.Get(_operationState)
                .VerifySet(x => x[StoreName] = InitialAccumulatorValue, Times.Once);
            Mock.Get(_operationState)
                .VerifySet(x => x.Instruction = InitialInstructionValue + 1, Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_Should_Throw_IllegalOperationException_For_Unknown_Type()
        {
            InitialiseMock();
            var operation = new StoreOperation();
            await Should.ThrowAsync< IllegalOperationException>(() => operation.ExecuteAsync(new DateTime(), _operationState, _writer, CancellationToken.None));
        }

        #region Test Data

        private const long InitialAccumulatorValue = 20L;
        private const int InitialInstructionValue = 1;
        private const string StoreName = "stor";

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
