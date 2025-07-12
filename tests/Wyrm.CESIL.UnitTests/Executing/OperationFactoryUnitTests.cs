using Shouldly;
using Wyrm.CESIL.Executing;
using Wyrm.CESIL.Parsing;

namespace Wyrm.CESIL.UnitTests.Executing
{
    public class OperationFactoryUnitTests
    {
        [Theory]
        [InlineData(InstructionType.LOAD)]
        public void CreateOperation_Should_Create_Operation(InstructionType type)
        {
            var factory = new OperationFactory();
            var instruction = new Instruction(1)
            {
                InstructionType = type
            };
            var operation = factory.CreateOperation(instruction);
            operation.ShouldBeOfType(OperationTypes[type]);
        }

        [Theory]
        [InlineData(null)]
        [InlineData((InstructionType)(-1))]
        public void CreateOperation_Should_Throw_NotSupportedException_If_Unknown_Type(InstructionType? type)
        {
            var factory = new OperationFactory();
            var instruction = new Instruction(1)
            {
                InstructionType = type
            };
            Should.Throw<NotSupportedException>(() => factory.CreateOperation(instruction));
        }

        #region Test Data

        private static readonly Dictionary<InstructionType, Type> OperationTypes = new Dictionary<InstructionType, Type>
        {
            { InstructionType.LOAD, typeof(LoadOperation) },
            { InstructionType.STORE, typeof(StoreOperation) },
            { InstructionType.IN, typeof(InOperation) },
            { InstructionType.ADD, typeof(AddOperation) },
            { InstructionType.SUBTRACT, typeof(SubtractOperation) },
            { InstructionType.MULTIPLY, typeof(MultiplyOperation) },
            { InstructionType.DIVIDE, typeof(DivideOperation) },
            { InstructionType.JUMP, typeof(JumpOperation) },
            { InstructionType.JIZERO, typeof(JizeroOperation) },
            { InstructionType.JINEG, typeof(JinegOperation) },
            { InstructionType.PRINT, typeof(PrintOperation) },
            { InstructionType.OUT, typeof(OutOperation) },
            { InstructionType.LINE, typeof(LineOperation) },
            { InstructionType.HALT, typeof(HaltOperation) }
        };

        #endregion
    }
}
