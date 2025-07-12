using Moq;
using Shouldly;
using Wyrm.CESIL.Exceptions;
using Wyrm.CESIL.Executing;

namespace Wyrm.CESIL.UnitTests.Executing
{
    public class JumpOperationUnitTests
    {
        [Fact]
        public async Task ExecuteAsync_Should_Jump()
        {
            InitialiseMock();
            var operation = new JumpOperation();
            await operation.ExecuteAsync(JumpLabel, _operationState, _writer, CancellationToken.None);
            Mock.Get(_operationState)
                .VerifySet(x => x.Instruction = JumpInstructionValue, Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_Should_Throw_IllegalOperationException_For_Unknown_Type()
        {
            InitialiseMock();
            var operation = new JumpOperation();
            await Should.ThrowAsync<IllegalOperationException>(() => operation.ExecuteAsync(new DateTime(), _operationState, _writer, CancellationToken.None));
        }

        #region Test Data

        private const int InitialInstructionValue = 1;
        private const int JumpInstructionValue = 10;
        private const string JumpLabel = "JMPL";

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
                .Setup(x => x.InstructionFor(JumpLabel))
                .Returns(JumpInstructionValue);
        }

        #endregion
    }
}
