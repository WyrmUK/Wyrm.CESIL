using Moq;
using Shouldly;
using System.Text;
using Wyrm.CESIL.Exceptions;
using Wyrm.CESIL.Executing;
using Wyrm.CESIL.Lexical;
using Wyrm.CESIL.Parsing;

namespace Wyrm.CESIL.UnitTests.Executing
{
    public class ExecutorTests
    {
        [Fact]
        public void Prepare_Should_Run_Without_Exception()
        {
            InitialiseMocks();
            var executor = new Executor(_operator, _operationStateFactory);
            executor.Prepare(_instructions, _errors);
            Mock.Get(_errors)
                .Verify(x => x.Add(It.Is<SyntaxError>(e =>
                    e.LineNo == InstructionWithLabel2.LineNo &&
                    e.CharNo == -1 &&
                    e.Message == "Duplicate label")), Times.Once);
        }

        [Fact]
        public void Run_Should_Run_Until_Halted()
        {
            InitialiseMocks();
            Mock.Get(_instructions)
                .Setup(x => x.Count)
                .Returns(1);
            var executor = new Executor(_operator, _operationStateFactory);
            executor.Prepare(_instructions, _errors);
            executor.Run(_dataSet, _writer, () => true);
            Mock.Get(_operator)
                .Verify(x => x.Operate(InstructionWithLabel, _operationState, _writer), Times.Once);
            _halted.ShouldBeTrue();
        }

        [Fact]
        public void Run_Should_Handle_IllegalOperationException()
        {
            InitialiseMocks();
            Mock.Get(_operator)
                .Setup(x => x.Operate(InstructionWithLabel, _operationState, _writer))
                .Throws(new IllegalOperationException(string.Empty));
            var executor = new Executor(_operator, _operationStateFactory);
            executor.Prepare(_instructions, _errors);
            executor.Run(_dataSet, _writer, () => true);
            Mock.Get(_writer)
                .Verify(x => x.WriteLine(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Run_Should_Handle_IllegalLabelException()
        {
            InitialiseMocks();
            Mock.Get(_operator)
                .Setup(x => x.Operate(InstructionWithLabel, _operationState, _writer))
                .Throws<IllegalLabelException>();
            var executor = new Executor(_operator, _operationStateFactory);
            executor.Prepare(_instructions, _errors);
            executor.Run(_dataSet, _writer, () => true);
            Mock.Get(_writer)
                .Verify(x => x.WriteLine(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Run_Should_Handle_NotInitialisedException()
        {
            InitialiseMocks();
            Mock.Get(_operator)
                .Setup(x => x.Operate(InstructionWithLabel, _operationState, _writer))
                .Throws<NotInitialisedException>();
            var executor = new Executor(_operator, _operationStateFactory);
            executor.Prepare(_instructions, _errors);
            executor.Run(_dataSet, _writer, () => true);
            Mock.Get(_writer)
                .Verify(x => x.WriteLine(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Run_Should_Handle_Exception()
        {
            InitialiseMocks();
            Mock.Get(_operator)
                .Setup(x => x.Operate(InstructionWithLabel, _operationState, _writer))
                .Throws<Exception>();
            var executor = new Executor(_operator, _operationStateFactory);
            executor.Prepare(_instructions, _errors);
            executor.Run(_dataSet, _writer, () => true);
            Mock.Get(_writer)
                .Verify(x => x.WriteLine(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task RunAsync_Should_Run_Until_Halted()
        {
            InitialiseMocks();
            Mock.Get(_instructions)
                .Setup(x => x.Count)
                .Returns(1);
            var executor = new Executor(_operator, _operationStateFactory);
            executor.Prepare(_instructions, _errors);
            await executor.RunAsync(_dataSet, _writer, () => true, CancellationToken);
            Mock.Get(_operator)
                .Verify(x => x.OperateAsync(InstructionWithLabel, _operationState, _writer, CancellationToken), Times.Once);
            _halted.ShouldBeTrue();
        }

        [Fact]
        public async Task RunAsync_Should_Run_Until_Cancelled()
        {
            InitialiseMocks();
            Mock.Get(_instructions)
                .Setup(x => x.Count)
                .Returns(1);
            var executor = new Executor(_operator, _operationStateFactory);
            executor.Prepare(_instructions, _errors);
            await executor.RunAsync(_dataSet, _writer, () => true, new CancellationToken(true));
            Mock.Get(_operator)
                .Verify(x => x.OperateAsync(InstructionWithLabel, _operationState, _writer, It.IsAny<CancellationToken>()), Times.Never);
            _halted.ShouldBeFalse();
        }

        [Fact]
        public async Task RunAsync_Should_Handle_IllegalOperationException()
        {
            InitialiseMocks();
            Mock.Get(_operator)
                .Setup(x => x.OperateAsync(InstructionWithLabel, _operationState, _writer, CancellationToken))
                .Throws(new IllegalOperationException(string.Empty));
            var executor = new Executor(_operator, _operationStateFactory);
            executor.Prepare(_instructions, _errors);
            await executor.RunAsync(_dataSet, _writer, () => true, CancellationToken);
            Mock.Get(_writer)
                .Verify(x => x.WriteLineAsync(It.IsAny<StringBuilder>(), CancellationToken), Times.Once);
        }

        [Fact]
        public async Task RunAsync_Should_Handle_IllegalLabelException()
        {
            InitialiseMocks();
            Mock.Get(_operator)
                .Setup(x => x.OperateAsync(InstructionWithLabel, _operationState, _writer, CancellationToken))
                .Throws<IllegalLabelException>();
            var executor = new Executor(_operator, _operationStateFactory);
            executor.Prepare(_instructions, _errors);
            await executor.RunAsync(_dataSet, _writer, () => true, CancellationToken);
            Mock.Get(_writer)
                .Verify(x => x.WriteLineAsync(It.IsAny<StringBuilder>(), CancellationToken), Times.Once);
        }

        [Fact]
        public async Task RunAsync_Should_Handle_NotInitialisedException()
        {
            InitialiseMocks();
            Mock.Get(_operator)
                .Setup(x => x.OperateAsync(InstructionWithLabel, _operationState, _writer, CancellationToken))
                .Throws<NotInitialisedException>();
            var executor = new Executor(_operator, _operationStateFactory);
            executor.Prepare(_instructions, _errors);
            await executor.RunAsync(_dataSet, _writer, () => true, CancellationToken);
            Mock.Get(_writer)
                .Verify(x => x.WriteLineAsync(It.IsAny<StringBuilder>(), CancellationToken), Times.Once);
        }

        [Fact]
        public async Task RunAsync_Should_Handle_TimeoutException()
        {
            InitialiseMocks();
            Mock.Get(_operator)
                .Setup(x => x.OperateAsync(InstructionWithLabel, _operationState, _writer, CancellationToken))
                .Throws<TimeoutException>();
            var executor = new Executor(_operator, _operationStateFactory);
            executor.Prepare(_instructions, _errors);
            await executor.RunAsync(_dataSet, _writer, () => true, CancellationToken);
            Mock.Get(_writer)
                .Verify(x => x.WriteLineAsync(It.IsAny<StringBuilder>(), CancellationToken), Times.Once);
        }

        [Fact]
        public async Task RunAsync_Should_Handle_Exception()
        {
            InitialiseMocks();
            Mock.Get(_operator)
                .Setup(x => x.OperateAsync(InstructionWithLabel, _operationState, _writer, CancellationToken))
                .Throws<Exception>();
            var executor = new Executor(_operator, _operationStateFactory);
            executor.Prepare(_instructions, _errors);
            await executor.RunAsync(_dataSet, _writer, () => true, CancellationToken);
            Mock.Get(_writer)
                .Verify(x => x.WriteLineAsync(It.IsAny<StringBuilder>(), CancellationToken), Times.Once);
        }

        [Fact]
        public void Clear_Should_Clear_Instructions_And_Errors()
        {
            InitialiseMocks();
            var executor = new Executor(_operator, _operationStateFactory);
            executor.Prepare(_instructions, _errors);
            executor.Clear();
            Mock.Get(_instructions)
                .Verify(x => x.Clear(), Times.Once);
        }

        #region Test Data

        private static readonly Instruction InstructionWithLabel = new(1)
        {
            Label = "Label"
        };
        private static readonly Instruction InstructionWithLabel2 = new(2)
        {
            Label = "Label"
        };
        private static readonly Instruction InstructionWithoutLabel = new(3);

        private bool _halted;

        private static readonly CancellationToken CancellationToken = new CancellationTokenSource().Token;

        #endregion

        #region Test Mocks

        private readonly IOperator _operator = Mock.Of<IOperator>();
        private readonly IOperationStateFactory _operationStateFactory = Mock.Of<IOperationStateFactory>();

        private readonly IList<Instruction> _instructions = Mock.Of<IList<Instruction>>();
        private readonly IList<SyntaxError> _errors = Mock.Of<IList<SyntaxError>>();

        private readonly IList<long> _dataSet = Mock.Of<IList<long>>();
        private readonly IOperationState _operationState = Mock.Of<IOperationState>();
        private readonly TextWriter _writer = Mock.Of<TextWriter>();

        private void InitialiseMocks()
        {
            Mock.Get(_instructions)
                .Setup(x => x.Count)
                .Returns(3);
            Mock.Get(_instructions)
                .Setup(x => x[0])
                .Returns(InstructionWithLabel);
            Mock.Get(_instructions)
                .Setup(x => x[1])
                .Returns(InstructionWithLabel2);
            Mock.Get(_instructions)
                .Setup(x => x[2])
                .Returns(InstructionWithoutLabel);
            Mock.Get(_operationStateFactory)
                .Setup(x => x.CreateOperationState(_dataSet, It.IsAny<IDictionary<string, int>>()))
                .Returns(_operationState);
            Mock.Get(_operationState)
                .Setup(x => x.Halted)
                .Returns(() => _halted);
            Mock.Get(_operationState)
                .SetupSet(x => x.Halted = It.IsAny<bool>())
                .Callback((bool halted) => _halted = halted);
        }

        #endregion
    }
}
