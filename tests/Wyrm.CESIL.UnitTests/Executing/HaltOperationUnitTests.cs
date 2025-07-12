using Moq;
using Wyrm.CESIL.Executing;

namespace Wyrm.CESIL.UnitTests.Executing
{
    public class HaltOperationUnitTests
    {
        [Fact]
        public async Task ExecuteAsync_Should_Halt()
        {
            InitialiseMock();
            var operation = new HaltOperation();
            await operation.ExecuteAsync(null, _operationState, _writer, CancellationToken.None);
            Mock.Get(_operationState)
                .VerifySet(x => x.Halted = true, Times.Once);
        }

        #region Test Mocks

        private readonly IOperationState _operationState = Mock.Of<IOperationState>();
        private readonly TextWriter _writer = Mock.Of<TextWriter>();

        private void InitialiseMock()
        {
            Mock.Get(_operationState)
                .Setup(x => x.Halted)
                .Returns(false);
        }

        #endregion
    }
}
