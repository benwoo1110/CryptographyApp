using System;
using Cryptography.Core;
using Cryptography.Core.Enums;
using NUnit.Framework;

namespace Cryptography.UnitTests
{
    [TestFixture]
    public class CipherFactoryTests
    {
        private CipherFactory cipherFactory;
        
        [SetUp]
        public void Setup()
        {
            cipherFactory = new CipherFactory();
        }

        [Test]
        [TestCase("Encrypt", Mode.Encrypt)]
        [TestCase("encrypt", Mode.Encrypt)]
        [TestCase("ENCRYPT", Mode.Encrypt)]
        [TestCase("enCrypT", Mode.Encrypt)]
        [TestCase("Decrypt", Mode.Decrypt)]
        [TestCase("decrypt", Mode.Decrypt)]
        [TestCase("DECRYPT", Mode.Decrypt)]
        [TestCase("DeCrYpT", Mode.Decrypt)]
        public void SetCipherMode_ValidMode_ReturnTrue(String mode, Mode expectedMode)
        {
            bool result = cipherFactory.SetCipherMode(mode);
         
            Assert.That(result, Is.True);
            Assert.That(cipherFactory.CipherMode, Is.EqualTo(expectedMode));
        }
        
        [Test]
        [TestCase("Dncypt")]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("error")]
        public void SetCipherMode_InvalidMode_ReturnFalse(String mode)
        {
            Mode previousMode = cipherFactory.CipherMode;
            
            bool result = cipherFactory.SetCipherMode(mode);
         
            Assert.That(result, Is.False);
            Assert.That(cipherFactory.CipherMode, Is.EqualTo(previousMode));
        }
        
        [Test]
        [TestCase("Ascii", InputType.Ascii)]
        [TestCase("asCII", InputType.Ascii)]
        [TestCase("Binary", InputType.Binary)]
        [TestCase("binary", InputType.Binary)]
        [TestCase("Decimal", InputType.Decimal)]
        [TestCase("decImal", InputType.Decimal)]
        [TestCase("Hex", InputType.Hex)]
        [TestCase("HEX", InputType.Hex)]
        public void SetTextType_ValidTextType_ReturnTrue(String type, InputType expectedType)
        {
            bool result = cipherFactory.SetTextType(type);
         
            Assert.That(result, Is.True);
            Assert.That(cipherFactory.TextType, Is.EqualTo(expectedType));
        }
        
        [Test]
        [TestCase("Asci")]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("error")]
        public void SetTextType_InvalidType_ReturnFalse(String type)
        {
            InputType previousMode = cipherFactory.TextType;
            
            bool result = cipherFactory.SetTextType(type);
         
            Assert.That(result, Is.False);
            Assert.That(cipherFactory.TextType, Is.EqualTo(previousMode));
        }
    }
}
