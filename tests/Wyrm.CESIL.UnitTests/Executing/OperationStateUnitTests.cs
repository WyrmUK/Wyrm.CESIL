using Moq;
using Shouldly;
using Wyrm.CESIL.Exceptions;
using Wyrm.CESIL.Executing;

namespace Wyrm.CESIL.UnitTests.Executing
{
    public class OperationStateUnitTests
    {
        [Fact]
        public void PopData_Should_Get_Top()
        {
            InitialiseMocks();
            var state = new OperationState(_dataSet, _labels);
            state.PopData().ShouldBe(StackDataValue);
            Mock.Get(_dataSet)
                .Verify(x => x.RemoveAt(0), Times.Once);
        }

        [Fact]
        public void PopData_Should_Throw_NoDataException_If_No_Data()
        {
            InitialiseMocks();
            Mock.Get(_dataSet)
                .Setup(x => x.Count)
                .Returns(0);
            var state = new OperationState(_dataSet, _labels);
            Should.Throw<NoDataException>(() => state.PopData());
        }

        [Fact]
        public void InstructionFor_Should_Get_Instruction()
        {
            InitialiseMocks();
            var state = new OperationState(_dataSet, _labels);
            state.InstructionFor(InstructionLabel).ShouldBe(InstructionNumber);
        }

        [Fact]
        public void InstructionFor_Should_Throw_IllegalLabelException_If_Not_Known()
        {
            InitialiseMocks();
            var state = new OperationState(_dataSet, _labels);
            Should.Throw<IllegalLabelException>(() => state.InstructionFor("Bad"));
        }

        [Fact]
        public void Indexer_Should_Return_Variable_Value_Set()
        {
            InitialiseMocks();
            var state = new OperationState(_dataSet, _labels);
            state[VariableName] = VariableValue;
            state[VariableName].ShouldBe(VariableValue);
        }

        [Fact]
        public void Indexer_Should_Overwrite_Variable_Value_Set()
        {
            InitialiseMocks();
            var state = new OperationState(_dataSet, _labels);
            state[VariableName] = VariableValue;
            state[VariableName] = StackDataValue;
            state[VariableName].ShouldBe(StackDataValue);
        }

        [Fact]
        public void Indexer_Should_Throw_NotInitialisedException_If_Variable_Not_Set()
        {
            InitialiseMocks();
            var state = new OperationState(_dataSet, _labels);
            Should.Throw<NotInitialisedException>(() => state[VariableName]);
        }

        #region Test Data

        private const long StackDataValue = 10L;
        private const string InstructionLabel = "ILABEL";
        private const int InstructionNumber = 5;
        private const string VariableName = "VAR1";
        private const long VariableValue = 15L;

        #endregion

        #region Test Mocks

        private readonly IList<long> _dataSet = Mock.Of<IList<long>>();
        private readonly IDictionary<string, int> _labels = Mock.Of<IDictionary<string, int>>();

        private void InitialiseMocks()
        {
            Mock.Get(_dataSet)
                .Setup(x => x.Count)
                .Returns(1);
            Mock.Get(_dataSet)
                .Setup(x => x[0])
                .Returns(StackDataValue);
            Mock.Get(_labels)
                .Setup(x => x.ContainsKey(InstructionLabel))
                .Returns(true);
            Mock.Get(_labels)
                .Setup(x => x[InstructionLabel])
                .Returns(InstructionNumber);
            // TODO
        }

        #endregion
    }
}
