using System;
using Cryptography.Core;
using Cryptography.Core.Ciphers;
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
            cipherFactory.RegisterCipher(new MockCipher());

            cipherFactory.HasSuchCipher("MockCipher");
            Assert.That(cipherFactory.GetCurrentSelected(), Is.Null);
            Assert.That(cipherFactory.CipherMode, Is.EqualTo(CipherFactory.DefaultCipherMode));
            Assert.That(cipherFactory.TextType, Is.EqualTo(CipherFactory.DefaultTextType));
        }

        [Test]
        public void RegisterCipher_Null_ThrowArgumentException()
        {
            Assert.That(
                () => cipherFactory.RegisterCipher(null),
                Throws.ArgumentException.With.Message.Contains("null")
            );
        }
        
        [Test]
        public void RegisterCipher_Repeated_ThrowArgumentException()
        {
            Assert.That(
                () => cipherFactory.RegisterCipher(new MockCipher()),
                Throws.ArgumentException.With.Message.Contains("same name")
                );
        }
        
        [Test]
        public void RegisterCipher_NewValidCipher_HasSuchCipher()
        {
            cipherFactory.RegisterCipher(new IDEA());
            
            Assert.That(cipherFactory.HasSuchCipher("IDEA"));
        }

        [Test]
        public void SelectCipher_ValidCipher_CipherIsSelected()
        {
            bool result = cipherFactory.SelectCipher("MockCipher");
            
            Assert.That(result, Is.True);
            Assert.That(cipherFactory.HasAnySelected(), Is.True);
            Assert.That(cipherFactory.GetCurrentSelected(), Is.EqualTo("MockCipher"));
        }
        
        [Test]
        [TestCase("None")]
        [TestCase("")]
        [TestCase(null)]
        public void SelectCipher_InvalidCipher_CipherNotSelected(string cipherName)
        {
            bool result = cipherFactory.SelectCipher(cipherName);
            
            Assert.That(result, Is.False);
            Assert.That(cipherFactory.HasAnySelected(), Is.False);
            Assert.That(cipherFactory.GetCurrentSelected(), Is.Null);
        }
        
        [Test]
        [TestCase("MockCipher", true)]
        [TestCase("mockcipher", true)]
        [TestCase("MOCKCIPHER", true)]
        [TestCase("None", false)]
        [TestCase("", false)]
        [TestCase(null, false)]
        public void HasSuchCipher_WhenCalled_ReturnIfCipherPresent(string cipherName, bool expectedResult)
        {
            bool result = cipherFactory.HasSuchCipher(cipherName);

            Assert.That(result, Is.EqualTo(expectedResult));
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

        [Test]
        public void SwitchCipherMode_WhenCalled_ReturnNewMode()
        {
            Mode newMode = cipherFactory.SwitchCipherMode();

            Assert.That(newMode, Is.EqualTo(Mode.Decrypt));
            
            Mode newMode2 = cipherFactory.SwitchCipherMode();

            Assert.That(newMode2, Is.EqualTo(Mode.Encrypt));
        }
        
        [Test]
        [TestCase("8AB", "B78", "1423")]
        [TestCase("11", "22", "33")]
        [TestCase("1D", "A2", "BF")]
        public void ValidInputAndKey_ReturnValidOutput(string input, string key, string output)
        {
            cipherFactory.SelectCipher("MockCipher");
            Assert.That(cipherFactory.GetCurrentSelected(), Is.EqualTo("MockCipher"));
            
            CipherResult result = cipherFactory.RunCipher(input, key);
            
            Assert.That(result.Output.Text, Is.EqualTo(output));
        }
    }
}
