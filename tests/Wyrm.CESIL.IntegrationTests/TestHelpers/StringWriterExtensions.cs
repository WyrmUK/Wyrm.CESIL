using Shouldly;

namespace Wyrm.CESIL.IntegrationTests.TestHelpers;

public static class StringWriterExtensions
{
    public static void ShouldHaveWritten(this StringWriter writer, string filename)
    {
        writer.GetStringBuilder().ToString().Replace("\r", "")
            .ShouldBe(File.OpenText($"Examples/{filename}.out").ReadToEnd()
                .Replace("\r", ""));
    }
}
