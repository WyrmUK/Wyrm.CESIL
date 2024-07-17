using System.Linq;
#if NET6_0_OR_GREATER
using System.Text;
#endif

namespace Wyrm.CESIL.Extensions
{
    internal static class StringExtensions
    {
        public static bool IsLettersAndDigits(this string val)
        {
            if (string.IsNullOrEmpty(val)) return false;
            if (!char.IsLetter(val[0])) return false;
            return val.ToCharArray().All(c => char.IsLetterOrDigit(c));
        }

#if NET6_0_OR_GREATER
        public static StringBuilder ToStringBuilder(this string val)
            => new StringBuilder(val);
#endif
    }
}
