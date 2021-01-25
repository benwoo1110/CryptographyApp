using System;
using System.Numerics;
using Cryptography.Core;
using Cryptography.Core.Ciphers;
using Cryptography.Core.Enums;
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

        private void EncryptDecryptRunner<T>(string plaintext, string key, string ciphertext, InputType type) where T : Cipher
        {
            T cipher = Activator.CreateInstance<T>();
            var pt = (BigInteger) Utilities.ConvertToBigInt(plaintext, type);
            var k = (BigInteger) Utilities.ConvertToBigInt(key, type);
            var ct = (BigInteger) Utilities.ConvertToBigInt(ciphertext, type);

            BigInteger encryptResult = cipher.Encrypt(pt, k);
            Assert.That(encryptResult, Is.EqualTo(ct));

            BigInteger decryptResult = cipher.Decrypt(ct, k);
            Assert.That(decryptResult, Is.EqualTo(pt));
        }

        [Test]
        [TestCase("0000000000000000", "85070591730234615865843651857942052864", "12939543216615949862")]
        [TestCase("17795682518166861558", "328272401029611223576431974228294039286", "5132065899320015509")]
        [TestCase("14252905559253642566", "210841623425522655792992894016546578078", "11429747308416114334")]
        [TestCase("16862118108961983876", "58269367470490382358246580833377976136", "14482258994785560488")]
        [TestCase("15793361462042830655", "5233100606242806050955395731361295", "4822678189205111")]
        [TestCase("17377603568952289863", "58269367470490382358246580833377976136", "16862118108961983876")]
        [TestCase("2920704760302135170", "340282366920938463463374607431768211455", "18446744073709551615")]
        [TestCase("17478863917414052451", "248322315116097096894562531538458870068", "16738971625840127119")]
        public void EncryptDecrypt_IDEA_ReturnMatchingText(string plaintext, string key, string ciphertext)
        {
            EncryptDecryptRunner<IDEA>(plaintext, key, ciphertext, InputType.Decimal);
        }

        [Test]
        [TestCase("000102030405060708090A0B0C0D0E0F", "000102030405060708090A0B0C0D0E0F", "9FB63337151BE9C71306D159EA7AFAA4")]
        [TestCase("000102030405060708090A0B0C0D0E0F", "000102030405060708090A0B0C0D0E0F1011121314151617", "95ACCC625366547617F8BE4373D10CD7")]
        [TestCase("000102030405060708090A0B0C0D0E0F", "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F", "8EF0272C42DB838BCF7B07AF0EC30F38")]
        public void EncryptDecrypt_Twofish_ReturnMatchingText(string plaintext, string key, string ciphertext)
        {
            EncryptDecryptRunner<Twofish>(plaintext, key, ciphertext, InputType.Hex);
        }

        [Test]
        [TestCase("2472f9675", "4B7A70E9B5B32944DB75092EC4192623AD6EA6B049A7DF7D9CEE60B88FEDB266ECAA8C71699A17FF5664526CC2B19EE1193602A575094C29", "0df123c1afb186c6f")]
        [TestCase("0x2472f9675", "4B7A70E9B5B32944DB75092EC4192623AD6EA6B049A7DF7D9CEE60B88FEDB266ECAA8C71699A17FF5664526CC2B19EE1193602A575094C29", "0df123c1afb186c6f")]
        public void EncryptDecrypt_Blowfish_ReturnMatchingText(string plaintext, string key, string ciphertext)
        {
            EncryptDecryptRunner<Blowfish>(plaintext, key, ciphertext, InputType.Hex);
        }
    }
}
