using System.Numerics;
using Cryptography.Core.Ciphers;
using NUnit.Framework;

namespace Cryptography.UnitTests
{
    [TestFixture]
    public class CipherTests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Name_SimpleTest_ReturnCipherName()
        {
            Cipher cipher = new MockCipher();

            string result = cipher.Name;

            Assert.That(result, Is.EqualTo("MockCipher"));
        }

        [Test]
        [TestCase("0000000000000000", "170141183460469231731687303715884105728", "12823428160561428239")]
        [TestCase("0000000000000000", "85070591730234615865843651857942052864", "12939543216615949862")]
        [TestCase("17795682518166861558", "328272401029611223576431974228294039286", "5132065899320015509")]
        [TestCase("14252905559253642566", "210841623425522655792992894016546578078", "11429747308416114334")]
        [TestCase("16862118108961983876", "58269367470490382358246580833377976136", "14482258994785560488")]
        [TestCase("15793361462042830655", "5233100606242806050955395731361295", "4822678189205111")]
        [TestCase("17377603568952289863", "58269367470490382358246580833377976136", "16862118108961983876")]
        [TestCase("2920704760302135170", "340282366920938463463374607431768211455", "18446744073709551615")]
        [TestCase("17478863917414052451", "248322315116097096894562531538458870068", "16738971625840127119")]
        public void Encrypt_IDEA_ReturnCipherText(string plaintext, string key, string ciphertext)
        {
            Cipher cipher = new IDEA();

            BigInteger result = cipher.Encrypt(BigInteger.Parse(plaintext), BigInteger.Parse(key));
            
            Assert.That(result, Is.EqualTo(BigInteger.Parse(ciphertext)));
        }

        [Test]
        [TestCase("0000000000000000", "170141183460469231731687303715884105728", "12823428160561428239")]
        [TestCase("0000000000000000", "85070591730234615865843651857942052864", "12939543216615949862")]
        [TestCase("17795682518166861558", "328272401029611223576431974228294039286", "5132065899320015509")]
        [TestCase("14252905559253642566", "210841623425522655792992894016546578078", "11429747308416114334")]
        [TestCase("16862118108961983876", "58269367470490382358246580833377976136", "14482258994785560488")]
        [TestCase("15793361462042830655", "5233100606242806050955395731361295", "4822678189205111")]
        [TestCase("17377603568952289863", "58269367470490382358246580833377976136", "16862118108961983876")]
        [TestCase("2920704760302135170", "340282366920938463463374607431768211455", "18446744073709551615")]
        [TestCase("17478863917414052451", "248322315116097096894562531538458870068", "16738971625840127119")]
        public void Decrypt_IDEA_ReturnPlainText(string plaintext, string key, string ciphertext)
        {
            Cipher cipher = new IDEA();

            BigInteger result = cipher.Decrypt(BigInteger.Parse(ciphertext), BigInteger.Parse(key));
            
            Assert.That(result, Is.EqualTo(BigInteger.Parse(plaintext)));
        }
    }
}
