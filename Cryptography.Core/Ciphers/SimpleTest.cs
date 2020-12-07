using System;
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
            return Utilities.NumberOfBits(value) > 1;
        }

        public override bool IsValidKey(BigInteger value)
        {
            return Utilities.NumberOfBits(value) > 1;
        }

        public override BigInteger Encrypt(BigInteger plaintext, BigInteger key)
        {
            return plaintext + key;
        }

        public override BigInteger Decrypt(BigInteger ciphertext, BigInteger key)
        {
            return ciphertext - key;
        }
    }
}