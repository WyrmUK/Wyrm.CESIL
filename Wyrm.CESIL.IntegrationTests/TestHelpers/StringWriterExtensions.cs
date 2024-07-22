using Shouldly;

namespace Wyrm.CESIL.IntegrationTests.TestHelpers;

public static class StringWriterExtensions
{
    public static void ShouldHaveWritten(this StringWriter writer, string filename)
    {
        File.OpenText($"Examples/{filename}.out").ReadToEnd()
            .ShouldBe(writer.GetStringBuilder().ToString());
    }
}
