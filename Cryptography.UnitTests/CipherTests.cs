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
        private CipherFactory cipherFactory;

        [SetUp]
        public void Setup()
        {
            cipherFactory = new CipherFactory();
        }
        
        [Test]
        [TestCase("0x0000000000000000", "80000000000000000000000000000000", "b1f5f7f87901370f")]
        [TestCase("0000000000000000", "40000000000000000000000000000000", "b3927dffb6358626")]
        [TestCase("f6f6f6f6f6f6f6f6", "f6f6f6f6f6f6f6f6f6f6f6f6f6f6f6f6", "4738c2be9cdd7a95")]
        [TestCase("c5cc7e174c80ed46", "9e9e9e9e9e9e9e9e9e9e9e9e9e9e9e9e", "9e9e9e9e9e9e9e9e")]
        [TestCase("ea024714ad5c4d84", "0x2bd6459f82c5b300952c49104881ff48", "c8fb51d3516627a8")]
        [TestCase("db2d4a92aa68273f", "000102030405060708090a0b0c0d0e0f", "11223344556677")]
        [TestCase("f129a6601ef62a47", "2bd6459f82c5b300952c49104881ff48", "ea024714ad5c4d84")]
        [TestCase("28886d814399e782", "ffffffffffffffffffffffffffffffff", "ffffffffffffffff")]
        [TestCase("F29166203ADB7263", "BAD123649FFF2903645ACFFE19038134", "E84CC601BF8B2C8F")]
        public void RunCipher_EncryptWithIDEA_ReturnCipherResult(string plaintext, string key, string ciphertext)
        {
            cipherFactory.RegisterCipher(new IDEA());
            cipherFactory.SelectCipher("IDEA");
            cipherFactory.TextType = InputType.Hex;
            cipherFactory.CipherMode = Mode.Encrypt;

            CipherResult result = cipherFactory.RunCipher(plaintext, key);

            Assert.That(result.OutputText, Is.Not.Null);
            Assert.That(result.OutputText.ToLower(), Is.EqualTo(ciphertext.ToLower()));
        }
    }
}