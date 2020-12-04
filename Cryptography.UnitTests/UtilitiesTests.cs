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
        public void ConvertToInt_Binary_ReturnsInteger()
        {
            ulong? result = Utilities.ConvertToInt("1111", InputType.Binary);
            
            Assert.That(result, Is.EqualTo(15));
        }
        
        [Test]
        public void ConvertToInt_Hex_ReturnsInteger()
        {
            ulong? result = Utilities.ConvertToInt("4F", InputType.Hex);
            
            Assert.That(result, Is.EqualTo(79));
        }
        
        [Test]
        public void ConvertToInt_Decimal_ReturnsInteger()
        {
            ulong? result = Utilities.ConvertToInt("2BD6459F82C5B300952C49104881FF48", InputType.Decimal);
            
            Assert.That(result, Is.EqualTo(129863));
        }
        
        [Test]
        public void ConvertToInt_Ascii_ReturnsInteger()
        {
            ulong? result = Utilities.ConvertToInt("HelloWo", InputType.Ascii);
            
            Assert.That(result, Is.EqualTo(129863));
        }
    }
}
