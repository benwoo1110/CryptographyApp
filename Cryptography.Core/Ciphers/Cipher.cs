namespace Cryptography.Core.Ciphers
{
    public abstract class Cipher
    {
        /*
         * Cryptography algorithm name.
         */
        public string Name { get; }
        
        protected Cipher(string name)
        {
            Name = name;
        }

        /*
         * Ensure that number of bits for plaintext/ciphertext can be used for algorithm.
         *
         * returns true if valid.
         */
        public abstract bool IsValidInput(int text);
        
        /*
         * Ensure that number of bits for key can be used for algorithm.
         *
         * returns true if valid.
         */
        public abstract bool IsValidKey(int key);

        /*
         * Converts plaintext into ciphertext with key given.
         *
         * returns ciphertext.
         */
        public abstract ulong Encrypt(ulong plaintext, ulong key);
        
        /*
         * Converts ciphertext into plaintext with key given.
         *
         * returns plaintext.
         */
        public abstract ulong Decrypt(ulong ciphertext, ulong key);
    }
}
