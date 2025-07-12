using Moq;
using Shouldly;
using Wyrm.CESIL.Executing;

namespace Wyrm.CESIL.UnitTests.Executing
{
    public class OperationStateFactoryUnitTests
    {
        [Fact]
        public void CreateOperationState_Should_Create_OperationState()
        {
            var factory = new OperationStateFactory();
            var state = factory.CreateOperationState(_dataSet, _labels);
            state.ShouldBeOfType<OperationState>();
        }

        #region Test Mocks

        private readonly IList<long> _dataSet = Mock.Of<IList<long>>();
        private readonly IDictionary<string, int> _labels = Mock.Of<IDictionary<string, int>>();

        #endregion
    }
}
