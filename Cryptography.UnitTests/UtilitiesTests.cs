using System;
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
        public void ConvertToBigInt_Binary_ReturnsInteger()
        {
            BigInteger? result = Utilities.ConvertToBigInt("1011", InputType.Binary);
            
            Assert.That(result.ToString(), Is.EqualTo("11"));
        }
        
        [Test]
        public void ConvertToBigInt_Hex_ReturnsInteger()
        {
            BigInteger? result = Utilities.ConvertToBigInt("2BD6459F82C5B300952C49104881FF48", InputType.Hex);
            
            Assert.That(result.ToString(), Is.EqualTo("58269367470490382358246580833377976136"));
        }
        
        [Test]
        public void ConvertToBigInt_Decimal_ReturnsInteger()
        {
            BigInteger? result = Utilities.ConvertToBigInt("189732465783642956328495689237465234654", InputType.Decimal);
            
            Assert.That(result.ToString(), Is.EqualTo("189732465783642956328495689237465234654"));
        }
        
        [Test]
        public void ConvertToBigInt_Ascii_ReturnsInteger()
        {
            BigInteger? result = Utilities.ConvertToBigInt("Hello World", InputType.Ascii);
            
            Assert.That(result.ToString(), Is.EqualTo("87521618088882533792115812"));
        }

        [Test]
        public void ConvertToString_Binary_ReturnString()
        {
            string result = Utilities.ConvertToString(BigInteger.Parse("5552913087"), InputType.Binary);

            Assert.That(result, Is.EqualTo("101001010111110101011101010111111"));
        }
        
        [Test]
        public void ConvertToString_Hex_ReturnString()
        {
            string result = Utilities.ConvertToString(BigInteger.Parse("58269367470490382358246580833377976136"), InputType.Hex);
            
            Assert.That(result, Is.EqualTo("2BD6459F82C5B300952C49104881FF48"));
        }
        
        [Test]
        public void ConvertToString_Decimal_ReturnString()
        {
            string result = Utilities.ConvertToString(BigInteger.Parse("189732465783642956328495689237465234654"), InputType.Decimal);
            
            Assert.That(result, Is.EqualTo("189732465783642956328495689237465234654"));
        }
        
        [Test]
        public void ConvertToString_Ascii_ReturnString()
        {
            string result = Utilities.ConvertToString(BigInteger.Parse("87521618088882533792115812"), InputType.Ascii);
            
            Assert.That(result, Is.EqualTo("Hello World"));
        }
        
        [Test]
        public void NumberOfBits_WhenCalled_ReturnNumber()
        {
            BigInteger? num = Utilities.ConvertToBigInt("2BD6459F82C5B300952C49104881FF48", InputType.Hex);
            
            Console.WriteLine(Utilities.ConvertToString((BigInteger) num, InputType.Binary));

            int result = Utilities.NumberOfBits((BigInteger) num);
            
            Assert.That(result, Is.EqualTo(126));
        }
    }
}
