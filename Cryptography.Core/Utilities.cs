using System;
using System.Text;
using Cryptography.Core.Enums;

namespace Cryptography.Core
{
    public static class Utilities
    {
        public static ulong? ConvertToInt(string input, InputType type)
        {
            try
            {
                return type.Equals(InputType.Ascii) ? ConvertAscii(input) : Convert.ToUInt64(input, (int) type);
            }
            catch (Exception)
            {
                // ignored
            }

            return null;
        }

        private static ulong? ConvertAscii(string input)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in input)
            {
                sb.AppendFormat("{0:X2}", (int) c);
            }
            Console.WriteLine(sb.ToString());
            return Convert.ToUInt64(sb.ToString(), 16);
        }

        public static string ConvertToString(ulong input, InputType type)
        {
            return Convert.ToString((long) input, (int) type);
        }
        
        public static TextOutcome ConvertValidationResult(bool result)
        {
            return result ? TextOutcome.Valid : TextOutcome.Invalid;
        }
    }
}
