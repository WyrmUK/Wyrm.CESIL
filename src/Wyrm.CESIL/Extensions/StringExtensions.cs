using System.Linq;

namespace Wyrm.CESIL.Extensions
{
    public static class StringExtensions
    {
        public static bool IsLettersAndDigits(this string val)
        {
            if (string.IsNullOrEmpty(val)) return false;
            if (!char.IsLetter(val[0])) return false;
            return val.ToCharArray().All(c => char.IsLetterOrDigit(c));
        }
    }
}
