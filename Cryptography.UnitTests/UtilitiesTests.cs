using System.Numerics;
using Cryptography.Core;
using Cryptography.Core.Enums;
using NUnit.Framework;

namespace Cryptography.UnitTests
{
    public class UtilitiesTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("000000", "0")]
        [TestCase("111", "7")]
        [TestCase("0001010", "10")]
        [TestCase("error", "")]
        public void ConvertToBigInt_Binary_ReturnsInteger(string binary, string expectedInteger)
        {
            var result = Utilities.ConvertToBigInt(binary, InputType.Binary);
            
            Assert.That(result.ToString(), Is.EqualTo(expectedInteger));
        }
        
        [Test]
        [TestCase("2BD6459F82C5B300952C49104881FF48", "58269367470490382358246580833377976136")]
        [TestCase("0ff", "255")]
        [TestCase("0xab", "171")]
        [TestCase("0x2Da", "730")]
        [TestCase("error",  "")]
        public void ConvertToBigInt_Hex_ReturnsInteger(string hex, string expectedInteger)
        {
            var result = Utilities.ConvertToBigInt(hex, InputType.Hex);
            
            Assert.That(result.ToString(), Is.EqualTo(expectedInteger));
        }
        
        [Test]
        public void ConvertToBigInt_Decimal_ReturnsInteger()
        {
            var result = Utilities.ConvertToBigInt("189732465783642956328495689237465234654", InputType.Decimal);
            
            Assert.That(result.ToString(), Is.EqualTo("189732465783642956328495689237465234654"));
        }
        
        [Test]
        public void ConvertToBigInt_Ascii_ReturnsInteger()
        {
            var result = Utilities.ConvertToBigInt("Hello World", InputType.Ascii);
            
            Assert.That(result.ToString(), Is.EqualTo("87521618088882533792115812"));
        }

        [Test]
        public void ConvertToString_Binary_ReturnBinaryString()
        {
            var result = Utilities.ConvertToString(BigInteger.Parse("5552913087"), InputType.Binary);

            Assert.That(result, Is.EqualTo("101001010111110101011101010111111"));
        }
        
        [Test]
        public void ConvertToString_Hex_ReturnHexString()
        {
            var result = Utilities.ConvertToString(BigInteger.Parse("58269367470490382358246580833377976136"), InputType.Hex);
            
            Assert.That(result, Is.EqualTo("2BD6459F82C5B300952C49104881FF48"));
        }
        
        [Test]
        public void ConvertToString_Decimal_ReturnDecimalString()
        {
            var result = Utilities.ConvertToString(BigInteger.Parse("189732465783642956328495689237465234654"), InputType.Decimal);
            
            Assert.That(result, Is.EqualTo("189732465783642956328495689237465234654"));
        }
        
        [Test]
        public void ConvertToString_Ascii_ReturnAsciiString()
        {
            var result = Utilities.ConvertToString(BigInteger.Parse("87521618088882533792115812"), InputType.Ascii);
            
            Assert.That(result, Is.EqualTo("Hello World"));
        }

        [Test]
        [TestCase("15", 4)]
        [TestCase("5552913087", 33)]
        public void NumberOfBits_WhenCalled_ReturnNumber(string number, int bitLength)
        {
            var result = Utilities.NumberOfBits(BigInteger.Parse(number));
            
            Assert.That(result, Is.EqualTo(bitLength));
        }

        [Test]
        [TestCase(true, ConvertResult.Valid)]
        [TestCase(false, ConvertResult.Invalid)]
        public void ValidationResult_WhenCalled_ReturnConvertResult(bool state, ConvertResult expectedResult)
        {
            var result = Utilities.ValidationResult(state);
            
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("word", "Word")]
        [TestCase("CAPS", "Caps")]
        [TestCase("tEST", "Test")]
        [TestCase("aMaZiNg", "Amazing")]
        [TestCase("", "")]
        [TestCase(null, "")]
        public void Capitalise_WhenCalled_ReturnCapitalisedWord(string word, string expectedResult)
        {
            var result = Utilities.Capitalise(word);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
        
        [Test]
        [TestCase("have spaces ", "havespaces")]
        [TestCase("CAPS", "CAPS")]
        [TestCase("Tab\t Tabs\t ", "TabTabs")]
        [TestCase("     ", "")]
        [TestCase("", "")]
        [TestCase(null, null)]
        public void Capitalise_WhenCalled_ReturnTrimedText(string text, string expectedResult)
        {
            var result = Utilities.TrimText(text);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
