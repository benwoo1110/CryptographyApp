using System.Numerics;

namespace Cryptography.Core.Ciphers
{
    public class Blowfish : Cipher
    {
        private const string CipherName = "Blowfish";
        
        public Blowfish() : base(CipherName)
        {
            
        }

        public override bool IsValidInput(BigInteger text)
        {
            throw new System.NotImplementedException();
        }

        public override bool IsValidKey(BigInteger key)
        {
            throw new System.NotImplementedException();
        }

        public override BigInteger Encrypt(BigInteger plaintext, BigInteger key)
        {
            throw new System.NotImplementedException();
        }

        public override BigInteger Decrypt(BigInteger ciphertext, BigInteger key)
        {
            throw new System.NotImplementedException();
        }
    }
}
