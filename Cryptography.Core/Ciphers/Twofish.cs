using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Cryptography.Core.Ciphers
{
    public class Twofish : Cipher
    {
        private const string CipherName = "Twofish";
        
        public Twofish() : base(CipherName)
        {
            //sis i dont even know
            int r1 = 0;
            int l1 = 0;
            int k1 = 0;
            int k2 = 0;
            BigInteger f1 = Xor(r1, k1);
            BigInteger r2 = Xor(f1, l1);
            BigInteger l2 = r1;
            BigInteger f2 = Xor(r2, k2);
            BigInteger r3 = Xor(f2, l2);

            //i think i should drop out
            //Key generation
            static void ShiftBitLeft(int self, int block)
            {
                if (block >= self)
                {
                    block = block << 1;
                    block += 1;
                }
                else
                {
                    block = block << 1;
                }
                return;
            }

            static void CreateKeyArray(int self, int block)
            {
                List<Twofish> keyArray = new List<Twofish>();
                for (int i = 0; i < 128; i++)
                {
                    ShiftBitLeft(self, block);
                    keyArray.Add();
                }
            }


        }

        public override bool IsValidInput(BigInteger value)
        {
            return Utilities.NumberOfBits(value) <= 128;
        }

        public override bool IsValidKey(BigInteger value)
        {
            return Utilities.NumberOfBits(value) <= 256;
        }

        private BigInteger Xor(BigInteger num1, BigInteger num2)
        {
            return num1 ^ num2;
        }
        private BigInteger AddModulo(BigInteger num1, BigInteger num2)
        {
            return (num1 + num2) % 0x10000;
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

