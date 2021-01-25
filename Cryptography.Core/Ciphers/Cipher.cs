using System;
using System.Numerics;

namespace Cryptography.Core.Ciphers
{
    public abstract class Cipher
    {
        /*
         * Cryptography algorithm name.
         */
        public string Name { get; }

        protected Cipher()
        {
            Name = GetType().Name;
        }
        
        [Obsolete("Derive cipher name from classname itself.")]
        protected Cipher(string name)
        {
            Name = name;
        }

        /*
         * Ensure that number of bits for plaintext/ciphertext can be used for algorithm.
         *
         * returns true if valid.
         */
        public abstract bool IsValidInput(BigInteger value);
        
        /*
         * Ensure that number of bits for key can be used for algorithm.
         *
         * returns true if valid.
         */
        public abstract bool IsValidKey(BigInteger value);

        /*
         * Converts plaintext into ciphertext with key given.
         *
         * returns ciphertext.
         */
        public abstract BigInteger Encrypt(BigInteger plaintext, BigInteger key);
        
        /*
         * Converts ciphertext into plaintext with key given.
         *
         * returns plaintext.
         */
        public abstract BigInteger Decrypt(BigInteger ciphertext, BigInteger key);
    }
}
