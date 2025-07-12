using Moq;
using Shouldly;
using Wyrm.CESIL.Exceptions;
using Wyrm.CESIL.Executing;

namespace Wyrm.CESIL.UnitTests.Executing
{
    public class LoadOperationUnitTests
    {
        [Theory]
        [InlineData(10L)]
        [InlineData(StoreName)]
        public async Task ExecuteAsync_Should_Load_Accumulator(object value)
        {
            InitialiseMock();
            var operation = new LoadOperation();
            await operation.ExecuteAsync(value, _operationState, _writer, CancellationToken.None);
            if (value is long longValue)
            {
                Mock.Get(_operationState)
                    .VerifySet(x => x.Accumulator = longValue, Times.Once);
            }
            else
            {
                Mock.Get(_operationState)
                    .VerifySet(x => x.Accumulator = InitialStoreValue, Times.Once);
            }
            Mock.Get(_operationState)
                .VerifySet(x => x.Instruction = InitialInstructionValue + 1, Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_Should_Throw_IllegalOperationException_For_Unknown_Type()
        {
            InitialiseMock();
            var operation = new LoadOperation();
            await Should.ThrowAsync<IllegalOperationException>(() => operation.ExecuteAsync(new DateTime(), _operationState, _writer, CancellationToken.None));
        }

        #region Test Data

        private const int InitialInstructionValue = 1;
        private const long InitialStoreValue = 15L;
        private const string StoreName = "stor";

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
                .Setup(x => x[StoreName])
                .Returns(InitialStoreValue);
        }

        #endregion
    }
}
