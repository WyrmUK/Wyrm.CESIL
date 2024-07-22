using Shouldly;
using Wyrm.CESIL.Exceptions;

namespace Wyrm.CESIL.UnitTests.Exceptions;

public class ExceptionsUnitTests
{
    [Theory]
    [MemberData(nameof(ConstructorTheoryData))]
    public void Exception_Derives_From_Exception_With_Default_Constructor(Type exception)
    {
        exception.GetConstructor(Type.EmptyTypes)?.Invoke(null)
            .ShouldNotBeNull()
            .ShouldBeAssignableTo(typeof(Exception));

    }

    #region Test Data

    public static readonly TheoryData<Type> ConstructorTheoryData = new()
    {
        typeof(BadStringException),
        typeof(IllegalDataException),
        typeof(IllegalInstructionException),
        typeof(IllegalIntegerException),
        typeof(IllegalLabelException),
        typeof(IllegalLocationException),
        typeof(IncompleteInstructionException),
        typeof(NoDataException),
        typeof(NotInitialisedException),
        typeof(SyntaxException),
        typeof(UnterminatedStringException)
    };

    #endregion
}
