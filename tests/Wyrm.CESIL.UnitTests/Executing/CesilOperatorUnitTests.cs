using Moq;
using Wyrm.CESIL.Executing;
using Wyrm.CESIL.Parsing;

namespace Wyrm.CESIL.UnitTests.Executing
{
    public class CesilOperatorUnitTests
    {
        [Fact]
        public void Operate_Should_Execute_Operation()
        {
            InitialiseMock();
            var cesilOperation = new CesilOperator(_operationFactory);
            cesilOperation.Operate(Instruction, _operationState, _writer);
            Mock.Get(_operation)
                .Verify(x => x.Execute(Instruction.Value, _operationState, _writer), Times.Once);
        }

        [Fact]
        public async Task OperateAsync_Should_Execute_Operation()
        {
            InitialiseMock();
            var cesilOperation = new CesilOperator(_operationFactory);
            await cesilOperation.OperateAsync(Instruction, _operationState, _writer, CancellationToken);
            Mock.Get(_operation)
                .Verify(x => x.ExecuteAsync(Instruction.Value, _operationState, _writer, CancellationToken), Times.Once);
        }

        #region Test Data

        private static readonly Instruction Instruction = new Instruction(1);
        private static readonly CancellationToken CancellationToken = new CancellationTokenSource().Token;

        #endregion

        #region Test Mocks

        private readonly IOperationFactory _operationFactory = Mock.Of<IOperationFactory>();
        private readonly IOperation _operation = Mock.Of<IOperation>();
        private readonly IOperationState _operationState = Mock.Of<IOperationState>();
        private readonly TextWriter _writer = Mock.Of<TextWriter>();

        private void InitialiseMock()
        {
            Mock.Get(_operationFactory)
                .Setup(x => x.CreateOperation(Instruction))
                .Returns(_operation);
        }

        #endregion
    }
}
