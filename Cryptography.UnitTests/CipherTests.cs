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

            var result = cipher.Name;

            Assert.That(result, Is.EqualTo("MockCipher"));
        }

        private void EncryptDecryptRunner<T>(string plaintext, string key, string ciphertext, InputType type) where T : Cipher
        {
            var cipher = Activator.CreateInstance<T>();
            var pt = (BigInteger) Utilities.ConvertToBigInt(plaintext, type);
            var k = (BigInteger) Utilities.ConvertToBigInt(key, type);
            var ct = (BigInteger) Utilities.ConvertToBigInt(ciphertext, type);

            var encryptResult = cipher.Encrypt(pt, k);
            Assert.That(encryptResult, Is.EqualTo(ct));

            var decryptResult = cipher.Decrypt(ct, k);
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
        public void EncryptDecrypt_IDEA_CipherCorrectly(string plaintext, string key, string ciphertext)
        {
            EncryptDecryptRunner<IDEA>(plaintext, key, ciphertext, InputType.Decimal);
        }

        [Test]
        [TestCase("000102030405060708090A0B0C0D0E0F", "000102030405060708090A0B0C0D0E0F", "9FB63337151BE9C71306D159EA7AFAA4")]
        [TestCase("000102030405060708090A0B0C0D0E0F", "000102030405060708090A0B0C0D0E0F1011121314151617", "95ACCC625366547617F8BE4373D10CD7")]
        [TestCase("000102030405060708090A0B0C0D0E0F", "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F", "8EF0272C42DB838BCF7B07AF0EC30F38")]
        public void EncryptDecrypt_Twofish_CipherCorrectly(string plaintext, string key, string ciphertext)
        {
            EncryptDecryptRunner<Twofish>(plaintext, key, ciphertext, InputType.Hex);
        }

        [Test]
        [TestCase("2472f9675", "4B7A70E9B5B32944DB75092EC4192623AD6EA6B049A7DF7D9CEE60B88FEDB266ECAA8C71699A17FF5664526CC2B19EE1193602A575094C29", "0df123c1afb186c6f")]
        [TestCase("0x2472f9675", "4B7A70E9B5B32944DB75092EC4192623AD6EA6B049A7DF7D9CEE60B88FEDB266ECAA8C71699A17FF5664526CC2B19EE1193602A575094C29", "0df123c1afb186c6f")]
        public void EncryptDecrypt_Blowfish_CipherCorrectly(string plaintext, string key, string ciphertext)
        {
            EncryptDecryptRunner<Blowfish>(plaintext, key, ciphertext, InputType.Hex);
        }

        [Test]
        [TestCase("00000000 00000000", "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00", "F29D3A7F 161D5E44C")]
        [TestCase("F29D3A7F 61D5E44C", "4A 0D 98 D7 CD 95 62 7F 58 6B 59 D3 BC 09 AD EF", "8D9D4B27 141F84247")]
        [TestCase("8D9D4B27 41F84247", "9D A7 AB 1F 7F 61 61 2F 82 31 11 0F 28 69 EF 07", "EB1EC88E 1ABBE6729")]
        [TestCase("EB1EC88E ABBE6729", "61 02 14 52 8F B6 CA E6 46 2E 0C 5E C7 14 05 8E", "8334F602 16420E581")]
        [TestCase("8334F602 6420E581", "B0 E2 BF 7A 52 92 98 CA A3 A4 65 82 B0 B4 63 92", "7BC7BA11 1B9D17D67")]
        public void EncryptDecrypt_RC5_CipherCorrectly(string plaintext, string key, string ciphertext)
        {
            EncryptDecryptRunner<RC5>(plaintext, key, ciphertext, InputType.Hex);
        }
    }
}
