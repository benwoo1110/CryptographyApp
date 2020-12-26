using System.Numerics;

namespace Cryptography.Core.Ciphers
{
    public class SimpleTest : Cipher
    {
        private const string CipherName = "SimpleTest";
        
        public SimpleTest() : base(CipherName)
        {
            
        }

        public override bool IsValidInput(BigInteger value)
        {
            // At most 128 bits
            // int -> storage up to 32 bit number so we use BigInteger
            return Utilities.NumberOfBits(value) <= 128;
        }

        public override bool IsValidKey(BigInteger value)
        {
            throw new System.NotImplementedException();
        }

        public override BigInteger Encrypt(BigInteger plaintext, BigInteger key)
        {
            BigInteger cipherText = plaintext + key;
            return cipherText;
        }

        public override BigInteger Decrypt(BigInteger ciphertext, BigInteger key)
        {
            throw new System.NotImplementedException();
        }
    }
}