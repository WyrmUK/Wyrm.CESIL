using Shouldly;
using System.Text;
using Wyrm.CESIL.Extensions;

namespace Wyrm.CESIL.UnitTests.Extensions;

public class StringExtensionsTests
{
    [Theory]
    [InlineData(null, false)]
    [InlineData("0test", false)]
    [InlineData("$test", false)]
    [InlineData("Te%st", false)]
    [InlineData("Test=123", false)]
    [InlineData("Test", true)]
    [InlineData("Test123", true)]
    public void IsLettersAndDigits_Should_Return_Expected(string? value, bool expected)
    {
        value.IsLettersAndDigits().ShouldBe(expected);
    }

    [Fact]
    public void ToStringBuilder_Should_Create_StringBuilder()
    {
        const string testStr = "Test";
        testStr.ToStringBuilder().ShouldBeOfType<StringBuilder>().ToString().ShouldBe(testStr);
    }
}
