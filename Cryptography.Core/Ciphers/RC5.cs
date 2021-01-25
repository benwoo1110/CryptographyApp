using System;
using System.Collections.Generic;
using System.Numerics;


namespace Cryptography.Core.Ciphers
{
    public class RC5 : Cipher
    {
        private const int Rounds = 12;
        private int Words { get; set; }
        private BigInteger MagicConst1 { get; set; }
        private BigInteger MagicConst2 { get; set; }
        private int KeyLengthByte { get; set; }
        private int KeyLengthWord { get; set; }
        private int WordLengthByte { get; set; }
        private int SubkeyNo { get; set; }
        private BigInteger[] TempL { get; set; }
        private BigInteger[] SubkeyL { get; set; }
        private BigInteger[] KeyL { get; set; }
        private BigInteger PTblock1 { get; set; }
        private BigInteger PTblock2 { get; set; }
        private BigInteger CTblock1 { get; set; }
        private BigInteger CTblock2 { get; set; }
        public RC5() : base()
        {
            Words = 32;
            WordLengthByte = Words / 8;
            KeyLengthByte = 16;

            KeyLengthWord = KeyLengthByte / WordLengthByte;

            SubkeyNo = 2*(12 + 1);
            
            MagicConst1 = 0xb7e15163;
            MagicConst2 = 0x9e3779b9;

            TempL = new BigInteger[] { 0, 0, 0, 0 }; //temporary list
            SubkeyL = new BigInteger[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}; //26 subkeys
            KeyL = new BigInteger[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
            PTblock1 = 0;
            PTblock2 = 0;
            CTblock1 = 0;
            CTblock2 = 0;
        }

        public override bool IsValidInput(BigInteger value)
        {
            return Utilities.NumberOfBits(value) <= 32; //16 hex digit = 64 bits
        }

        public override bool IsValidKey(BigInteger value)
        {
            return Utilities.NumberOfBits(value) <= 128;  //16 bytes = 16 * 4 = 128 bits
        }

        public double Odd (double value)
        {
            return 2 * Math.Floor(value / 2) + 1;
        }

        private BigInteger AddModulo(BigInteger num1, BigInteger num2)
        {
            return (num1 + num2) % (BigInteger) Math.Pow(2, 32);
        }

        private BigInteger Xor(BigInteger num1, BigInteger num2)
        {
            return num1 ^ num2;
        }

        public BigInteger RotateLeft(BigInteger value, BigInteger count)
        {
            return (value << (int) count) | (value >> (32 - (int) count));
}

        public BigInteger RotateRight(BigInteger value, BigInteger count)  //conversion problems, still facing issue
        {
            return (value >> (int) count) | (value << (32 - (int) count));
        }

        void DividePT(BigInteger pt)
        {
            PTblock1 = pt >> 16;
            PTblock2 = pt - PTblock1 << 16; 
        }

        void DivideCT(BigInteger ct)
        {
            CTblock1 = ct >> 16;
            CTblock2 = ct - PTblock1 << 16;
        }

        void DivideKey(BigInteger key)
        {
            KeyL[0] = key >> 120;
            KeyL[1] = (key >> 112) & 0xFF;
            KeyL[2] = (key >> 104) & 0xFF;
            KeyL[3] = (key >> 96) & 0xFF;
            KeyL[4] = (key >> 88) & 0xFF;
            KeyL[5] = (key >> 80) & 0xFF;
            KeyL[6] = (key >> 72) & 0xFF;
            KeyL[7] = (key >> 64) & 0xFF;
            KeyL[8] = (key >> 56) & 0xFF;
            KeyL[9] = (key >> 48) & 0xFF;
            KeyL[10] = (key >> 40) & 0xFF;
            KeyL[11] = (key >> 32) & 0xFF;
            KeyL[12] = (key >> 24) & 0xFF;
            KeyL[13] = (key >> 16) & 0xFF;
            KeyL[14] = (key >> 8) & 0xFF;
            KeyL[15] = key & 0xFF;
        }

        void RC5_SETUP(BigInteger key)
        {
            // length of word is bits, w = 16
            // magic constants, p = 0xb7e15163
            // magic constants, q = 0x9e3779b9
            // number of rounds, r = 12
            // length of key, b = 4
            // length of the key in words, c = max(1, ceil(8 * b/w))
            // number of round subkeys, t = 2 * (r+1)
            // Code is designed to work with w = 32, r = 12, and b = 16'

            //convert secret key K from bytes to words 
            for (int i = KeyLengthByte - 1; i != -1; i--)
            {
                TempL[i % WordLengthByte] = AddModulo(RotateLeft(TempL[i % WordLengthByte], 8), KeyL[i]);  //conversion problems
            }

            SubkeyL[0] = MagicConst1;
            for (int i = 1; i < SubkeyNo; i++)
            {
                SubkeyL[i] = SubkeyL[i - 1] + MagicConst2;
            }

            int z = 0;
            int j = 0;
            BigInteger A = 0;
            BigInteger B = 0;

            //need to take a look at the source code, dont quite understand A = S[z] part
            for (int i = 0; i < 12; i++)
            {
                A = SubkeyL[z] = RotateLeft(AddModulo(SubkeyL[z], AddModulo(A, B)), 3);
                B = TempL[j] = RotateLeft(AddModulo(TempL[j], AddModulo(A, B)), AddModulo(A, B));
                z = (z + 1) % SubkeyNo;
                j = (j + 1) % KeyLengthWord;
            }

        }


        public override BigInteger Encrypt(BigInteger plaintext, BigInteger key)
        {
            DivideKey(key);
            RC5_SETUP(key);
            DividePT(plaintext);


            PTblock1 = AddModulo(PTblock1, SubkeyL[0]);        //conversion error
            PTblock2 = AddModulo(PTblock2, SubkeyL[1]);

            for (int i = 1; i <= 12; i++)
            {
                PTblock1 = RotateLeft(PTblock1 ^ PTblock2, PTblock2) + SubkeyL[2 * i];    //conversion error
                PTblock2 = RotateLeft(PTblock2 ^ PTblock1, PTblock1) + SubkeyL[2 * i + 1];
            }

            BigInteger ciphertext = (PTblock1 << 16) + PTblock2;
            return ciphertext;
        }

        public override BigInteger Decrypt(BigInteger ciphertext, BigInteger key)
        {
            RC5_SETUP(key);
            DivideCT(ciphertext);

            for (int i = 12; i > 0; i--)
            {
                CTblock2 = RotateRight(CTblock2 - SubkeyL[2 * i + 1], CTblock1) ^ CTblock1;  //conversion error
                CTblock1 = RotateRight(CTblock1 - SubkeyL[2 * i], CTblock2) ^ CTblock2;
            }
            PTblock2 = CTblock2 - SubkeyL[1];
            PTblock1 = CTblock1 - SubkeyL[0];

            BigInteger plaintext = (PTblock1 << 16) + PTblock2;
            return plaintext;
        }
    }
}
