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

        public override bool IsValidInput(BigInteger text)
        {
            return true;
        }

        public override bool IsValidKey(BigInteger key)
        {
            return true;
        }

        public override BigInteger Encrypt(BigInteger plaintext, BigInteger key)
        {
            return plaintext + key;
        }

        public override BigInteger Decrypt(BigInteger ciphertext, BigInteger key)
        {
            Console.WriteLine(ciphertext);
            Console.WriteLine(key);
            Console.WriteLine(ciphertext - key);

            
            return ciphertext - key;
        }
    }
}