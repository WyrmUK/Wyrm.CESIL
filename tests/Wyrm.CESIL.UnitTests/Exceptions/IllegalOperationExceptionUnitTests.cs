using Shouldly;
using Wyrm.CESIL.Exceptions;

namespace Wyrm.CESIL.UnitTests.Exceptions;

public class IllegalOperationExceptionUnitTests
{
    [Fact]
    public void Constructor_Should_Initialise_Message_Of_Base_Exception()
    {
        new IllegalOperationException(Message).Message.ShouldBe(Message);
    }

    #region Test Data

    private const string Message = "The message.";

    #endregion
}
