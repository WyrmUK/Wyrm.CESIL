using Moq;
using Shouldly;
using Wyrm.CESIL.Exceptions;
using Wyrm.CESIL.Executing;

namespace Wyrm.CESIL.UnitTests.Executing
{
    public class JizeroOperationUnitTests
    {
        [Theory]
        [InlineData(-1L)]
        [InlineData(0L)]
        [InlineData(1L)]
        public async Task ExecuteAsync_Should_Jump_If_Zero(long accumulatorValue)
        {
            InitialiseMock();
            Mock.Get(_operationState)
                .Setup(x => x.Accumulator)
                .Returns(accumulatorValue);
            var operation = new JizeroOperation();
            await operation.ExecuteAsync(JumpLabel, _operationState, _writer, CancellationToken.None);
            Mock.Get(_operationState)
                .VerifySet(x => x.Instruction = JumpInstructionValue, accumulatorValue == 0L ? Times.Once : Times.Never);
            Mock.Get(_operationState)
                .VerifySet(x => x.Instruction = InitialInstructionValue + 1, accumulatorValue == 0L ? Times.Never : Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_Should_Throw_IllegalOperationException_For_Unknown_Type()
        {
            InitialiseMock();
            Mock.Get(_operationState)
                .Setup(x => x.Accumulator)
                .Returns(0L);
            var operation = new JizeroOperation();
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
