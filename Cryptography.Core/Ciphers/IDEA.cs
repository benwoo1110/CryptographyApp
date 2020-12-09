using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Cryptography.Core.Ciphers
{
    public class IDEA : Cipher
    {
        private const string CipherName = "IDEA";
        
        public IDEA() : base(CipherName)
        {
            
        }

        public override bool IsValidInput(BigInteger value)
        {
            return Utilities.NumberOfBits(value) <= 64;
        }

        public override bool IsValidKey(BigInteger value)
        {
            return Utilities.NumberOfBits(value) <= 128;
        }
        
        private BigInteger MultiplyModulo(BigInteger num1, BigInteger num2)
        {
            if (num1 == 0x0000)
            {
                num1 = 0x10000;
            }
            if (num2 == 0x0000)
            {
                num2 = 0x10000;
            }

            BigInteger result = (num1 * num2) % 0x10001;
            return result == 0x10000 ? 0 : result;
        }

        private BigInteger AddModulo(BigInteger num1, BigInteger num2)
        {
            return (num1 + num2) % 0x10000;
        }

        private BigInteger Xor(BigInteger num1, BigInteger num2)
        {
            return num1 ^ num2;

        }

        private List<BigInteger> GenerateSubText(BigInteger text)
        {
            return new List<BigInteger>
            {
                (text >> 48) & 0xFFFF,
                (text >> 32) & 0xFFFF,
                (text >> 16) & 0xFFFF,
                text & 0xFFFF
            };
        }

        private List<BigInteger> GenerateSubKeys(BigInteger key)
        {
            BigInteger modulus = new BigInteger(1) << 128;
            List<BigInteger> subKeys = new List<BigInteger>();

            for (int i = 0; i < 54; i++)
            { 
                subKeys.Add((key >> (112 - 16 * (i % 8))) % 0x10000);
                if (i % 8 == 7)
                {
                    key = ((key << 25) | (key >> 103)) % modulus;
                }
            }

            return subKeys;
        }

        private BigInteger CombineText(List<BigInteger> subText)
        {
            return (subText[0] << 48) | (subText[1] << 32) | (subText[2] << 16) | subText[3];
        }

        public override BigInteger Encrypt(BigInteger plaintext, BigInteger key)
        {
            List<BigInteger> subText = GenerateSubText(plaintext);
            List<BigInteger> subKeys = GenerateSubKeys(key);

            for (int i = 0; i < 8; i++)
            {
                subText = EncryptionRound(subText, subKeys.GetRange(i * 6, 6));
                
                if (i != 7)
                {
                    BigInteger temp = subText[1];
                    subText[1] = subText[2];
                    subText[2] = temp;
                }
            }
            
            subText = EncryptionLastRound(subText, subKeys.GetRange(48, 4));

            return CombineText(subText);
        }

        private List<BigInteger> EncryptionRound(List<BigInteger> subText, List<BigInteger> subKeys)
        {
            BigInteger step1 = MultiplyModulo(subText[0], subKeys[0]);
            BigInteger step2 = AddModulo(subText[1], subKeys[1]);
            BigInteger step3 = AddModulo(subText[2], subKeys[2]);
            BigInteger step4 = MultiplyModulo(subText[3], subKeys[3]);
            BigInteger step5 = Xor(step1, step3);
            BigInteger step6 = Xor(step2, step4);
            BigInteger step7 = MultiplyModulo(step5, subKeys[4]);
            BigInteger step8 = AddModulo(step6, step7);
            BigInteger step9 = MultiplyModulo(step8, subKeys[5]);
            BigInteger step10 = AddModulo(step7, step9);
            BigInteger step11 = Xor(step1, step9);
            BigInteger step12 = Xor(step2, step10);
            BigInteger step13 = Xor(step3, step9);
            BigInteger step14 = Xor(step4, step10);
            
            return new List<BigInteger> { step11, step12, step13, step14 };
        }
        
        private List<BigInteger> EncryptionLastRound(List<BigInteger> subText, List<BigInteger> subKeys)
        {
            BigInteger step1 = MultiplyModulo(subText[0], subKeys[0]);
            BigInteger step2 = AddModulo(subText[1], subKeys[1]);
            BigInteger step3 = AddModulo(subText[2], subKeys[2]);
            BigInteger step4 = MultiplyModulo(subText[3], subKeys[3]);
            
            return new List<BigInteger>{ step1, step2, step3, step4 };
        }

        public override BigInteger Decrypt(BigInteger ciphertext, BigInteger key)
        {
            throw new System.NotImplementedException();
        }
    }
}
