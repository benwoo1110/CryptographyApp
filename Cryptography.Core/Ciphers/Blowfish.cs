namespace Cryptography.Core.Ciphers
{
    public class Blowfish : Cipher
    {
        private const string CipherName = "Blowfish";
        
        public Blowfish() : base(CipherName)
        {
            
        }

        public override bool IsValidInput(int text)
        {
            throw new System.NotImplementedException();
        }

        public override bool IsValidKey(int key)
        {
            throw new System.NotImplementedException();
        }

        public override ulong Encrypt(ulong plaintext, ulong key)
        {
            throw new System.NotImplementedException();
        }

        public override ulong Decrypt(ulong ciphertext, ulong key)
        {
            throw new System.NotImplementedException();
        }
    }
}
