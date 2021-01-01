using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Cryptography.Core.Ciphers
{
    // Reference links:
    // https://github.com/davidmigloz/IDEA-cipher
    // https://github.com/bozhu/IDEA-Python
    public class IDEA : Cipher
    {
        private const string CipherName = "IDEA";
        private const int Rounds = 8;
        private const int KeysPerRound = 6;
        private const int KeysLastRound = 4;
        
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
        
        private BigInteger AddInverse(BigInteger x)
        {
            return (0x10000 - x) & 0xFFFF;
        }

        private BigInteger MultiplyInverse(BigInteger x)
        {
            if (x <= 1) {
                // 0 and 1 are their own inverses
                return x;
            }
            
            try {
                BigInteger y = 0x10001;
                BigInteger t0 = 1;
                BigInteger t1 = 0;
                while (true)
                {
                    t1 += y / x * t0;
                    y %= x;
                    if (y == 1) {
                        return (1 - t1) & 0xFFFF;
                    }
                    t0 += x / y * t1;
                    x %= y;
                    if (x == 1) {
                        return t0;
                    }
                }
            }
            catch (ArithmeticException) {
                return 0;
            }
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

            int numberOfKeys = Rounds * KeysPerRound + KeysLastRound;
            for (int i = 0; i < numberOfKeys; i++)
            { 
                subKeys.Add((key >> (112 - 16 * (i % 8))) % 0x10000);
                if (i % 8 == 7)
                {
                    key = ((key << 25) | (key >> 103)) % modulus;
                }
            }

            return subKeys;
        }
        
        private List<BigInteger> InverseSubKeys(List<BigInteger> subKey)
        {
            BigInteger[] invSubKey = new BigInteger[subKey.Count];
            int p = 0;
            int i = Rounds  * KeysPerRound;
            
            // For the final output transformation (round 9)
            invSubKey[i]     = MultiplyInverse(subKey[p++]);    // 48 <- 0
            invSubKey[i + 1] = AddInverse(subKey[p++]);         // 49 <- 1
            invSubKey[i + 2] = AddInverse(subKey[p++]);         // 50 <- 2
            invSubKey[i + 3] = MultiplyInverse(subKey[p++]);    // 51 <- 3
            
            // From round 8 to 2
            for (int r = Rounds - 1; r > 0; r--) {
                i = r * 6;
                invSubKey[i + 4] = subKey[p++];                    // 46 <- 4 ...
                invSubKey[i + 5] = subKey[p++];                    // 47 <- 5 ...
                invSubKey[i]     = MultiplyInverse(subKey[p++]);   // 42 <- 6 ...
                invSubKey[i + 2] = AddInverse(subKey[p++]);        // 44 <- 7 ...
                invSubKey[i + 1] = AddInverse(subKey[p++]);        // 43 <- 8 ...
                invSubKey[i + 3] = MultiplyInverse(subKey[p++]);   // 45 <- 9 ...
            }
            
            // Round 1
            invSubKey[4] = subKey[p++];                     // 4 <- 46
            invSubKey[5] = subKey[p++];                     // 5 <- 47
            invSubKey[0] = MultiplyInverse(subKey[p++]);    // 0 <- 48
            invSubKey[1] = AddInverse(subKey[p++]);         // 1 <- 49
            invSubKey[2] = AddInverse(subKey[p++]);         // 2 <- 50
            invSubKey[3] = MultiplyInverse(subKey[p]);      // 3 <- 51
            
            return invSubKey.ToList();
        }

        private BigInteger CombineText(List<BigInteger> subText)
        {
            return (subText[0] << 48) | (subText[1] << 32) | (subText[2] << 16) | subText[3];
        }
        
        private List<BigInteger> CipherRound(List<BigInteger> subText, List<BigInteger> subKeys)
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
        
        private List<BigInteger> CipherLastRound(List<BigInteger> subText, List<BigInteger> subKeys)
        {
            BigInteger step1 = MultiplyModulo(subText[0], subKeys[0]);
            BigInteger step2 = AddModulo(subText[1], subKeys[1]);
            BigInteger step3 = AddModulo(subText[2], subKeys[2]);
            BigInteger step4 = MultiplyModulo(subText[3], subKeys[3]);
            
            return new List<BigInteger>{ step1, step2, step3, step4 };
        }

        private BigInteger DoCipher(List<BigInteger> subText, List<BigInteger> subKeys)
        {
            for (int currentRound = 0; currentRound < Rounds; currentRound++)
            {
                subText = CipherRound(subText, subKeys.GetRange(currentRound * KeysPerRound, KeysPerRound));
                
                if (currentRound != Rounds - 1)
                {
                    BigInteger temp = subText[1];
                    subText[1] = subText[2];
                    subText[2] = temp;
                }
            }
            
            subText = CipherLastRound(subText, subKeys.GetRange(Rounds * KeysPerRound, KeysLastRound));

            return CombineText(subText);
        }

        public override BigInteger Encrypt(BigInteger plaintext, BigInteger key)
        {
            List<BigInteger> subText = GenerateSubText(plaintext);
            List<BigInteger> subKeys = GenerateSubKeys(key);

            return DoCipher(subText, subKeys);
        }

        public override BigInteger Decrypt(BigInteger ciphertext, BigInteger key)
        {
            List<BigInteger> subText = GenerateSubText(ciphertext);
            List<BigInteger> subKeys = InverseSubKeys(GenerateSubKeys(key));
            
            return DoCipher(subText, subKeys);
        }
    }
}
