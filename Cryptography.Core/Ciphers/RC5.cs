using System;
using System.Collections.Generic;
using System.Numerics;


namespace Cryptography.Core.Ciphers
{
    public class RC5 : Cipher
    {
        private const string CipherName = "RC5";
        private const int Rounds = 12;
        
        public RC5() : base(CipherName)
        {
            
        }

        public override bool IsValidInput(BigInteger value)
        {
            return Utilities.NumberOfBits(value) == 16;
        }

        public override bool IsValidKey(BigInteger value)
        {
            return Utilities.NumberOfBits(value) == 32;
        }

        public double Odd (double value)
        {
            return 2 * Math.Floor(value / 2) + 1;
        }

        private double AddModulo(double num1, double num2)
        {
            return (num1 + num2) % Math.Pow(2, 32);
        }

        private BigInteger Xor(BigInteger num1, BigInteger num2)
        {
            return num1 ^ num2;

        }

        public static uint RotateLeft(this uint value, int count)
        {
            return (value << count) | (value >> (32 - count));
}

        public static uint RotateRight(this uint value, int count)
        {
            return (value >> count) | (value << (32 - count));
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
            BigInteger K = key;
            double w = 16;
            double E = 2.7182818284590451;
            double GoldenRatio = 1.61803398874989484820458683436;
            double p = Odd((E - 2) * Math.Pow(2, w));
            double q = Odd((GoldenRatio - 2) * Math.Pow(2, w));
            double r = 12;
            double b = 4;
            double t = 2 * (r + 1);
            double u = w / 8;
            double c = b / u;
            double[] L = { 0, 0 }; //temporary list
            double[] S = { 0, 0 }; //subkeys

            //convert secret key K from bytes to words 
            double v = c - 1;
            L[Convert.ToInt32(v)] = 0;
            for (double i = b - 1; i != -1; i--)
            {
                L[Convert.ToInt32(i)] = (RotateLeft(L[Convert.ToInt32(u/i)], 8)) + K[i];
            }

            S[0] = p;
            for (int i = 1; i < t; i++)
            {
                S[i] = S[i - 1] + q;
            }

            double z = 0;
            double j = 0;
            double A = 0;
            double B = 0;
            for (double i = 0; i < 12; i++)
            {
                A = S[Convert.ToInt32(z)] = RotateLeft(AddModulo(S[Convert.ToInt32(z)], AddModulo(A, B)), 3);
                B = L[Convert.ToInt32(j)] = RotateLeft(AddModulo(L[Convert.ToInt32(j)], AddModulo(A, B)), AddModulo(A, B));
                z = (z + 1) % t;
                j = (j + 1) % c;
            }

            return S , A , B;
        }


        public override BigInteger Encrypt(string plaintext, Array S)
        {
            string[] pt = { "0", "0" };
            pt[0] = plaintext.Substring(0, (plaintext.Length / 2) - 1);
            pt[1] = plaintext.Substring(plaintext.Length / 2, plaintext.Length -1);



            A = AddModulo(pt[0], S[0]);
            B = AddModulo(pt[1], S[1]);

            for (i = 1; i <= r; i++)
            {
                A = RotateLeft(A ^ B, B) + S[2 * i];
                B = RotateLeft(B ^ A, A) + S[2 * i + 1];
            }
            return ct[0] = A| ct[1] = B;
        }

        public override BigInteger Decrypt(BigInteger ciphertext, BigInteger key)
        {
            WORD i, B = ct[1], A = ct[0];

            for (i = r; i > 0; i--)
            {
                B = RotateRight(B - S[2 * i + 1], A) ^ A;
                A = RotateRight(A - S[2 * i], B) ^ B;
            }

            return pt[1] = B - S[1]| pt[0] = A - S[0];
        }
    }
}
