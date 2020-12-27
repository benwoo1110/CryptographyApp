using System.Numerics;
using Cryptography.Core;
using Cryptography.Core.Ciphers;

namespace Cryptography.UnitTests
{
    public class MockCipher : Cipher
    {
        private const string CipherName = "MockCipher";
        
        public MockCipher() : base(CipherName)
        {
            
        }

        public override bool IsValidInput(BigInteger value)
        {
            return Utilities.NumberOfBits(value) <= 16;
        }

        public override bool IsValidKey(BigInteger value)
        {
            return Utilities.NumberOfBits(value) <= 8;
        }

        public override BigInteger Encrypt(BigInteger plaintext, BigInteger key)
        {
            BigInteger cipherText = plaintext + key;
            return cipherText;
        }

        public override BigInteger Decrypt(BigInteger ciphertext, BigInteger key)
        {
            BigInteger plaintext = ciphertext - key;
            return plaintext;
        }
    }
}
