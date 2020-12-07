using System;
using System.Globalization;
using System.Numerics;
using System.Text;
using Cryptography.Core.Enums;

namespace Cryptography.Core
{
    public static class Utilities
    {
        public static BigInteger? ConvertToBigInt(string value, InputType type)
        {
            try
            {
                switch (type)
                {
                    case InputType.Hex:
                        return BigInteger.Parse("0" + value, NumberStyles.HexNumber);
                    case InputType.Decimal:
                        return BigInteger.Parse(value, NumberStyles.Integer);
                    case InputType.Binary:
                        return BinToInt(value);
                    case InputType.Ascii:
                        return AsciiToInt(value);
                }
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
            
            Console.WriteLine(sb.ToString());
            return BigInteger.Parse(sb.ToString(), NumberStyles.HexNumber);
        }

        public static string ConvertToString(BigInteger value, InputType type)
        {
            switch (type)
            {
                case InputType.Hex:
                    return value.ToString("X");
                case InputType.Decimal:
                    return value.ToString();
                case InputType.Binary:
                    return IntToBin(value);
                case InputType.Ascii:
                    return IntToAscii(value);
            }

            return null;
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
        
        public static TextOutcome ValidationResult(bool result)
        {
            return result ? TextOutcome.Valid : TextOutcome.Invalid;
        }
    }
}
