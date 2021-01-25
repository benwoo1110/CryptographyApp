using System;
using System.Globalization;
using System.Numerics;
using System.Text;
using Cryptography.Core.Enums;

namespace Cryptography.Core
{
    public static class Utilities
    {
        public static BigInteger? ConvertToBigInt(string value, InputType type = InputType.Hex)
        {
            try
            {
                value = value.Replace(" ", "");
                return type switch
                {
                    InputType.Hex => BigInteger.Parse(RemoveLeadingValue(value, "0x"), NumberStyles.HexNumber),
                    InputType.Decimal => BigInteger.Parse(value, NumberStyles.Integer),
                    InputType.Binary => BinToInt(value),
                    InputType.Ascii => AsciiToInt(value),
                    _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
                };
            }
            catch (Exception)
            {
                // ignored
            }

            return null;
        }
        
        private static BigInteger BinToInt(string value)
        {
            BigInteger parsedNum = 0;

            foreach(char c in value)
            {
                parsedNum <<= 1;
                if (c.Equals('1'))
                {
                    parsedNum += 1;
                }
                else if (!c.Equals('0'))
                {
                    throw new ArgumentException("Invalid binary: " + value);
                }
            }

            return parsedNum;
        }

        private static BigInteger AsciiToInt(string value)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in value)
            {
                sb.AppendFormat("{0:X2}", (int) c);
            }
            
            return BigInteger.Parse(sb.ToString(), NumberStyles.HexNumber);
        }

        public static string ConvertToString(BigInteger value, InputType type = InputType.Hex)
        {
            switch (type)
            {
                case InputType.Hex:
                    return RemoveLeadingValue(value.ToString("X"), "0");
                case InputType.Decimal:
                    return value.ToString();
                case InputType.Binary:
                    return IntToBin(value);
                case InputType.Ascii:
                    return IntToAscii(value);
            }

            return null;
        }

        private static string RemoveLeadingValue(string value, string toRemove)
        {
            return value.StartsWith(toRemove) ? value.Substring(toRemove.Length) : value;
        }

        private static string IntToAscii(BigInteger value)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var b in value.ToByteArray())
            {
                sb.Insert(0, Convert.ToChar(b));
            }

            return sb.ToString();
        }
        
        private static string IntToBin(BigInteger value)
        {
            StringBuilder sb = new StringBuilder();
            while (value > 0)
            {
                 sb.Insert(0, value % 2 == 0 ? '0' : '1');
                 value /= 2;
            }
            
            return  sb.ToString();
        }

        public static int NumberOfBits(BigInteger value)
        {
            int number = 0;
            while (value > 0)
            { 
                value /= 2;
                number++;
            }

            return number;
        }
        
        public static ConvertResult ValidationResult(bool result)
        {
            return result ? ConvertResult.Valid : ConvertResult.Invalid;
        }

        public static string Capitalise(string word)
        {
            return (string.IsNullOrEmpty(word))
                ? ""
                : char.ToUpper(word[0]) + word.Substring(1).ToLower(); 
        }

        public static string TrimText(string text)
        {
            return (string.IsNullOrEmpty(text))
                ? text 
                : text.Replace(" ", "").Replace("\t", "");
        }
    }
}
