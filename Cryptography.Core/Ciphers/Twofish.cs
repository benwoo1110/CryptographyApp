using System;
using System.Numerics;

namespace Cryptography.Core.Ciphers
{
    // ReSharper disable InconsistentNaming
    public class Twofish : Cipher

    {
        /**
         * Constants
         */
        private const int BLOCK_SIZE = 16; // bytes in a data-block

        private const int ROUNDS = 16;
        private const int MAX_ROUNDS = 16; // max # rounds (for allocating subkeys)
        private static readonly int[] SUPPORTED_KEY_LENGTH = {64, 128, 192, 256};
        private const int INPUT_LENGTH = 128;

        /*
         * Subkey array indices
         */
        private const int INPUT_WHITEN = 0;
        private const int OUTPUT_WHITEN = INPUT_WHITEN + BLOCK_SIZE / 4;
        private const int ROUND_SUBKEYS = OUTPUT_WHITEN + BLOCK_SIZE / 4; // 2*(# rounds)

        private const int TOTAL_SUBKEYS = ROUND_SUBKEYS + 2 * MAX_ROUNDS;

        private const int SK_STEP = 0x02020202;
        private const int SK_BUMP = 0x01010101;
        private const int SK_ROTL = 9;

        /**
         * Fixed 8x8 permutation S-boxes
         */
        private static readonly byte[,] P = {
            { // p0
                0xA9, 0x67, 0xB3, 0xE8,
                0x04, 0xFD, 0xA3, 0x76,
                0x9A, 0x92, 0x80, 0x78,
                0xE4, 0xDD, 0xD1, 0x38,
                0x0D, 0xC6, 0x35, 0x98,
                0x18, 0xF7, 0xEC, 0x6C,
                0x43, 0x75, 0x37, 0x26,
                0xFA, 0x13, 0x94, 0x48,
                0xF2, 0xD0, 0x8B, 0x30,
                0x84, 0x54, 0xDF, 0x23,
                0x19, 0x5B, 0x3D, 0x59,
                0xF3, 0xAE, 0xA2, 0x82,
                0x63, 0x01, 0x83, 0x2E,
                0xD9, 0x51, 0x9B, 0x7C,
                0xA6, 0xEB, 0xA5, 0xBE,
                0x16, 0x0C, 0xE3, 0x61,
                0xC0, 0x8C, 0x3A, 0xF5,
                0x73, 0x2C, 0x25, 0x0B,
                0xBB, 0x4E, 0x89, 0x6B,
                0x53, 0x6A, 0xB4, 0xF1,
                0xE1, 0xE6, 0xBD, 0x45,
                0xE2, 0xF4, 0xB6, 0x66,
                0xCC, 0x95, 0x03, 0x56,
                0xD4, 0x1C, 0x1E, 0xD7,
                0xFB, 0xC3, 0x8E, 0xB5,
                0xE9, 0xCF, 0xBF, 0xBA,
                0xEA, 0x77, 0x39, 0xAF,
                0x33, 0xC9, 0x62, 0x71,
                0x81, 0x79, 0x09, 0xAD,
                0x24, 0xCD, 0xF9, 0xD8,
                0xE5, 0xC5, 0xB9, 0x4D,
                0x44, 0x08, 0x86, 0xE7,
                0xA1, 0x1D, 0xAA, 0xED,
                0x06, 0x70, 0xB2, 0xD2,
                0x41, 0x7B, 0xA0, 0x11,
                0x31, 0xC2, 0x27, 0x90,
                0x20, 0xF6, 0x60, 0xFF,
                0x96, 0x5C, 0xB1, 0xAB,
                0x9E, 0x9C, 0x52, 0x1B,
                0x5F, 0x93, 0x0A, 0xEF,
                0x91, 0x85, 0x49, 0xEE,
                0x2D, 0x4F, 0x8F, 0x3B,
                0x47, 0x87, 0x6D, 0x46,
                0xD6, 0x3E, 0x69, 0x64,
                0x2A, 0xCE, 0xCB, 0x2F,
                0xFC, 0x97, 0x05, 0x7A,
                0xAC, 0x7F, 0xD5, 0x1A,
                0x4B, 0x0E, 0xA7, 0x5A,
                0x28, 0x14, 0x3F, 0x29,
                0x88, 0x3C, 0x4C, 0x02,
                0xB8, 0xDA, 0xB0, 0x17,
                0x55, 0x1F, 0x8A, 0x7D,
                0x57, 0xC7, 0x8D, 0x74,
                0xB7, 0xC4, 0x9F, 0x72,
                0x7E, 0x15, 0x22, 0x12,
                0x58, 0x07, 0x99, 0x34,
                0x6E, 0x50, 0xDE, 0x68,
                0x65, 0xBC, 0xDB, 0xF8,
                0xC8, 0xA8, 0x2B, 0x40,
                0xDC, 0xFE, 0x32, 0xA4,
                0xCA, 0x10, 0x21, 0xF0,
                0xD3, 0x5D, 0x0F, 0x00,
                0x6F, 0x9D, 0x36, 0x42,
                0x4A, 0x5E, 0xC1, 0xE0
            },
            { // p1
                0x75, 0xF3, 0xC6, 0xF4,
                0xDB, 0x7B, 0xFB, 0xC8,
                0x4A, 0xD3, 0xE6, 0x6B,
                0x45, 0x7D, 0xE8, 0x4B,
                0xD6, 0x32, 0xD8, 0xFD,
                0x37, 0x71, 0xF1, 0xE1,
                0x30, 0x0F, 0xF8, 0x1B,
                0x87, 0xFA, 0x06, 0x3F,
                0x5E, 0xBA, 0xAE, 0x5B,
                0x8A, 0x00, 0xBC, 0x9D,
                0x6D, 0xC1, 0xB1, 0x0E,
                0x80, 0x5D, 0xD2, 0xD5,
                0xA0, 0x84, 0x07, 0x14,
                0xB5, 0x90, 0x2C, 0xA3,
                0xB2, 0x73, 0x4C, 0x54,
                0x92, 0x74, 0x36, 0x51,
                0x38, 0xB0, 0xBD, 0x5A,
                0xFC, 0x60, 0x62, 0x96,
                0x6C, 0x42, 0xF7, 0x10,
                0x7C, 0x28, 0x27, 0x8C,
                0x13, 0x95, 0x9C, 0xC7,
                0x24, 0x46, 0x3B, 0x70,
                0xCA, 0xE3, 0x85, 0xCB,
                0x11, 0xD0, 0x93, 0xB8,
                0xA6, 0x83, 0x20, 0xFF,
                0x9F, 0x77, 0xC3, 0xCC,
                0x03, 0x6F, 0x08, 0xBF,
                0x40, 0xE7, 0x2B, 0xE2,
                0x79, 0x0C, 0xAA, 0x82,
                0x41, 0x3A, 0xEA, 0xB9,
                0xE4, 0x9A, 0xA4, 0x97,
                0x7E, 0xDA, 0x7A, 0x17,
                0x66, 0x94, 0xA1, 0x1D,
                0x3D, 0xF0, 0xDE, 0xB3,
                0x0B, 0x72, 0xA7, 0x1C,
                0xEF, 0xD1, 0x53, 0x3E,
                0x8F, 0x33, 0x26, 0x5F,
                0xEC, 0x76, 0x2A, 0x49,
                0x81, 0x88, 0xEE, 0x21,
                0xC4, 0x1A, 0xEB, 0xD9,
                0xC5, 0x39, 0x99, 0xCD,
                0xAD, 0x31, 0x8B, 0x01,
                0x18, 0x23, 0xDD, 0x1F,
                0x4E, 0x2D, 0xF9, 0x48,
                0x4F, 0xF2, 0x65, 0x8E,
                0x78, 0x5C, 0x58, 0x19,
                0x8D, 0xE5, 0x98, 0x57,
                0x67, 0x7F, 0x05, 0x64,
                0xAF, 0x63, 0xB6, 0xFE,
                0xF5, 0xB7, 0x3C, 0xA5,
                0xCE, 0xE9, 0x68, 0x44,
                0xE0, 0x4D, 0x43, 0x69,
                0x29, 0x2E, 0xAC, 0x15,
                0x59, 0xA8, 0x0A, 0x9E,
                0x6E, 0x47, 0xDF, 0x34,
                0x35, 0x6A, 0xCF, 0xDC,
                0x22, 0xC9, 0xC0, 0x9B,
                0x89, 0xD4, 0xED, 0xAB,
                0x12, 0xA2, 0x0D, 0x52,
                0xBB, 0x02, 0x2F, 0xA9,
                0xD7, 0x61, 0x1E, 0xB4,
                0x50, 0x04, 0xF6, 0xC2,
                0x16, 0x25, 0x86, 0x56,
                0x55, 0x09, 0xBE, 0x91
            }
        };

        /**
         * Define the fixed p0/p1 permutations used in keyed S-box lookup.
         * By changing the following constant definitions, the S-boxes will
         * automatically get changed in the Twofish engine.
         */
        private const int P_00 = 1;

        private const int P_01 = 0;
        private const int P_02 = 0;
        private const int P_03 = P_01 ^ 1;
        private const int P_04 = 1;

        private const int P_10 = 0;
        private const int P_11 = 0;
        private const int P_12 = 1;
        private const int P_13 = P_11 ^ 1;
        private const int P_14 = 0;

        private const int P_20 = 1;
        private const int P_21 = 1;
        private const int P_22 = 0;
        private const int P_23 = P_21 ^ 1;
        private const int P_24 = 0;

        private const int P_30 = 0;
        private const int P_31 = 1;
        private const int P_32 = 1;
        private const int P_33 = P_31 ^ 1;
        private const int P_34 = 1;

        /**
         * Primitive polynomial for GF(256)
         */
        private const int GF256_FDBK = 0x169;

        private const int GF256_FDBK_2 = 0x169 / 2;
        private const int GF256_FDBK_4 = 0x169 / 4;

        private static readonly int[,] MDS = new int[4, 256]; // blank final
        private const int RS_GF_FDBK = 0x14D; // field generator


        /**
         * Helper methods
         */
        private static int LFSR1(int x)
        {
            return (x >> 1) ^
                   ((x & 0x01) != 0 ? GF256_FDBK_2 : 0);
        }

        private static int LFSR2(int x)
        {
            return (x >> 2) ^
                   ((x & 0x02) != 0 ? GF256_FDBK_2 : 0) ^
                   ((x & 0x01) != 0 ? GF256_FDBK_4 : 0);
        }

        private static int Mx_1(int x)
        {
            return x;
        }

        private static int Mx_X(int x)
        {
            return x ^ LFSR2(x);
        }

        private static int Mx_Y(int x)
        {
            return x ^ LFSR1(x) ^ LFSR2(x);
        }

        private static int GetLength(BigInteger num)
        {
            var bitLength = Utilities.NumberOfBits(num);
            foreach (var targetLength in SUPPORTED_KEY_LENGTH)
            {
                if (bitLength <= targetLength)
                {
                    return targetLength;
                }
            }

            throw new ArgumentException($"Invalid key bit length of {bitLength}");
        }

        private static int RS_MDS_Encode(int k0, int k1)
        {
            var r = k1;
            for (var i = 0; i < 4; i++) // shift 1 byte at a time
                r = RS_rem(r);
            r ^= k0;
            for (var i = 0; i < 4; i++)
                r = RS_rem(r);
            return r;
        }

        /*
         * Reed-Solomon code parameters: (12, 8) reversible code:<p>
         * <pre>
         *   g(x) = x**4 + (a + 1/a) x**3 + a x**2 + (a + 1/a) x + 1
         * </pre>
         * where a = primitive root of field generator 0x14D
         */
        private static int RS_rem(int x)
        {
            var b = (int) ((uint) x >> 24) & 0xFF;
            var g2 = ((b << 1) ^ ((b & 0x80) != 0 ? RS_GF_FDBK : 0)) & 0xFF;
            var g3 = (int) ((uint) b >> 1) ^ ((b & 0x01) != 0 ? (int) ((uint) RS_GF_FDBK >> 1) : 0) ^ g2;
            var result = (x << 8) ^ (g3 << 24) ^ (g2 << 16) ^ (g3 << 8) ^ b;
            return result;
        }



        private static int GetByteAtPos(BigInteger num, int pos, int totalLength)
        {
            return (int) ((num >> (totalLength - (pos + 1) * 8)) & 0xFF);
        }

        private static int B0(int x)
        {
            return x & 0xFF;
        }

        private static int B1(int x)
        {
            return (int) ((uint) x >> 8) & 0xFF;
        }

        private static int B2(int x)
        {
            return (int) ((uint) x >> 16) & 0xFF;
        }

        private static int B3(int x)
        {
            return (int) ((uint) x >> 24) & 0xFF;
        }

        private static int F32(int k64Cnt, int x, int[] k32)
        {
            var b0 = B0(x);
            var b1 = B1(x);
            var b2 = B2(x);
            var b3 = B3(x);
            var k0 = k32[0];
            var k1 = k32[1];
            var k2 = k32[2];
            var k3 = k32[3];

            var result = 0;
            var comparator = k64Cnt & 3;
            if (comparator == 1)
            {
                result =
                    MDS[0, (P[P_01, b0] & 0xFF) ^ B0(k0)] ^
                    MDS[1, (P[P_11, b1] & 0xFF) ^ B1(k0)] ^
                    MDS[2, (P[P_21, b2] & 0xFF) ^ B2(k0)] ^
                    MDS[3, (P[P_31, b3] & 0xFF) ^ B3(k0)];
            }
            if (comparator == 0)
            {
                b0 = (P[P_04, b0] & 0xFF) ^ B0(k3);
                b1 = (P[P_14, b1] & 0xFF) ^ B1(k3);
                b2 = (P[P_24, b2] & 0xFF) ^ B2(k3);
                b3 = (P[P_34, b3] & 0xFF) ^ B3(k3);
            }
            if (comparator == 0 || comparator == 3)
            {
                b0 = (P[P_03, b0] & 0xFF) ^ B0(k2);
                b1 = (P[P_13, b1] & 0xFF) ^ B1(k2);
                b2 = (P[P_23, b2] & 0xFF) ^ B2(k2);
                b3 = (P[P_33, b3] & 0xFF) ^ B3(k2);
            }
            if (comparator == 0 || comparator == 3 || comparator == 2)
            {
                result =
                    MDS[0, (P[P_01, (P[P_02, b0] & 0xFF) ^ B0(k1)] & 0xFF) ^ B0(k0)] ^
                    MDS[1, (P[P_11, (P[P_12, b1] & 0xFF) ^ B1(k1)] & 0xFF) ^ B1(k0)] ^
                    MDS[2, (P[P_21, (P[P_22, b2] & 0xFF) ^ B2(k1)] & 0xFF) ^ B2(k0)] ^
                    MDS[3, (P[P_31, (P[P_32, b3] & 0xFF) ^ B3(k1)] & 0xFF) ^ B3(k0)];
            }

            return result;
        }

        private static int Fe32(int[] sBox, int x, int R)
        {
            return sBox[2 * _b(x, R)] ^
                   sBox[2 * _b(x, R + 1) + 1] ^
                   sBox[0x200 + 2 * _b(x, R + 2)] ^
                   sBox[0x200 + 2 * _b(x, R + 3) + 1];
        }

        private static int _b(int x, int N)
        {
            var result = 0;
            switch (N % 4)
            {
                case 0:
                    result = B0(x);
                    break;
                case 1:
                    result = B1(x);
                    break;
                case 2:
                    result = B2(x);
                    break;
                case 3:
                    result = B3(x);
                    break;
            }

            return result;
        }

        private static int TripleShift(int num, int by)
        {
            return (int) ((uint) num >> by);
        }


        /**
         * Matrix stuff
         */
        private void PrecomputeMDSMatrix()
        {
            var m1 = new int[2];
            var mX = new int[2];
            var mY = new int[2];
            int i, j;
            for (i = 0; i < 256; i++)
            {
                j = P[0, i] & 0xFF; // compute all the matrix elements
                m1[0] = j;
                mX[0] = Mx_X(j) & 0xFF;
                mY[0] = Mx_Y(j) & 0xFF;

                j = P[1, i] & 0xFF;
                m1[1] = j;
                mX[1] = Mx_X(j) & 0xFF;
                mY[1] = Mx_Y(j) & 0xFF;

                MDS[0, i] = m1[P_00] << 0 | // fill matrix w/ above elements
                            mX[P_00] << 8 |
                            mY[P_00] << 16 |
                            mY[P_00] << 24;
                MDS[1, i] = mY[P_10] << 0 |
                            mY[P_10] << 8 |
                            mX[P_10] << 16 |
                            m1[P_10] << 24;
                MDS[2, i] = mX[P_20] << 0 |
                            mY[P_20] << 8 |
                            m1[P_20] << 16 |
                            mY[P_20] << 24;
                MDS[3, i] = mX[P_30] << 0 |
                            m1[P_30] << 8 |
                            mY[P_30] << 16 |
                            mX[P_30] << 24;
            }


            Console.WriteLine("==========");
            Console.WriteLine();
            Console.WriteLine("Static Data");
            Console.WriteLine();
            Console.WriteLine("MDS[0][]:");
            for (i = 0; i < 64; i++)
            {
                for (j = 0; j < 4; j++) Console.Write("0x" + Utilities.ConvertToString(MDS[0, i * 4 + j]) + ", ");
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("MDS[1][]:");
            for (i = 0; i < 64; i++)
            {
                for (j = 0; j < 4; j++) Console.Write("0x" + Utilities.ConvertToString(MDS[1, i * 4 + j]) + ", ");
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("MDS[2][]:");
            for (i = 0; i < 64; i++)
            {
                for (j = 0; j < 4; j++) Console.Write("0x" + Utilities.ConvertToString(MDS[2, i * 4 + j]) + ", ");
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("MDS[3][]:");
            for (i = 0; i < 64; i++)
            {
                for (j = 0; j < 4; j++) Console.Write("0x" + Utilities.ConvertToString(MDS[3, i * 4 + j]) + ", ");
                Console.WriteLine();
            }

            Console.WriteLine();
        }


        /**
         * Kye gen stuff
         */
        private static object MakeKey(BigInteger key)
        {
            var bitLength = GetLength(key);
            var length = bitLength / 8;
            var k64Cnt = length / 8;
            var subkeyCnt = ROUND_SUBKEYS + 2 * ROUNDS;
            var k32e = new int[4]; // even 32-bit entities
            var k32o = new int[4]; // odd 32-bit entities
            var sBoxKey = new int[4];

            //
            // split user key material into even and odd 32-bit entities and
            // compute S-box keys using (12, 8) Reed-Solomon code over GF(256)
            //
            int i, j, offset = 0;

            for (i = 0, j = k64Cnt - 1; i < 4 && offset < length; i++, j--)
            {
                k32e[i] = (GetByteAtPos(key, offset++, bitLength) & 0xFF) |
                          (GetByteAtPos(key, offset++, bitLength) & 0xFF) << 8 |
                          (GetByteAtPos(key, offset++, bitLength) & 0xFF) << 16 |
                          (GetByteAtPos(key, offset++, bitLength) & 0xFF) << 24;

                k32o[i] = (GetByteAtPos(key, offset++, bitLength) & 0xFF) |
                          (GetByteAtPos(key, offset++, bitLength) & 0xFF) << 8 |
                          (GetByteAtPos(key, offset++, bitLength) & 0xFF) << 16 |
                          (GetByteAtPos(key, offset++, bitLength) & 0xFF) << 24;

                sBoxKey[j] = RS_MDS_Encode(k32e[i], k32o[i]); // reverse order

                Console.WriteLine($"POSSS >>>>> {sBoxKey[j]}");

            }

            // compute the round decryption subkeys for PHT. these same subkeys
            // will be used in encryption but will be applied in reverse order.
            int q, A, B;
            var subKeys = new int[subkeyCnt];
            for (i = q = 0; i < subkeyCnt / 2; i++, q += SK_STEP)
            {
                A = F32(k64Cnt, q, k32e); // A uses even key entities
                B = F32(k64Cnt, q + SK_BUMP, k32o); // B uses odd  key entities
                B = B << 8 | (int) ((uint) B >> 24);
                A += B;
                subKeys[2 * i] = A; // combine with a PHT
                A += B;
                subKeys[2 * i + 1] = A << SK_ROTL | (int) ((uint) A >> (32 - SK_ROTL));
            }

            //
            // fully expand the table for speed
            //
            var k0 = sBoxKey[0];
            var k1 = sBoxKey[1];
            var k2 = sBoxKey[2];
            var k3 = sBoxKey[3];
            int b0, b1, b2, b3;
            var sBox = new int[4 * 256];
            for (i = 0; i < 256; i++)
            {
                b0 = b1 = b2 = b3 = i;

                var comparator = k64Cnt & 3;
                if (comparator == 1)
                {
                    sBox[2 * i] = MDS[0, (P[P_01, b0] & 0xFF) ^ B0(k0)];
                    sBox[2 * i + 1] = MDS[1, (P[P_11, b1] & 0xFF) ^ B1(k0)];
                    sBox[0x200 + 2 * i] = MDS[2, (P[P_21, b2] & 0xFF) ^ B2(k0)];
                    sBox[0x200 + 2 * i + 1] = MDS[3, (P[P_31, b3] & 0xFF) ^ B3(k0)];
                }
                if (comparator == 0)
                {
                    b0 = (P[P_04, b0] & 0xFF) ^ B0(k3);
                    b1 = (P[P_14, b1] & 0xFF) ^ B1(k3);
                    b2 = (P[P_24, b2] & 0xFF) ^ B2(k3);
                    b3 = (P[P_34, b3] & 0xFF) ^ B3(k3);
                }
                if (comparator == 0 || comparator == 3)
                {
                    b0 = (P[P_03, b0] & 0xFF) ^ B0(k2);
                    b1 = (P[P_13, b1] & 0xFF) ^ B1(k2);
                    b2 = (P[P_23, b2] & 0xFF) ^ B2(k2);
                    b3 = (P[P_33, b3] & 0xFF) ^ B3(k2);
                }
                if (comparator == 0 || comparator == 3 || comparator == 2)
                {
                    sBox[2 * i] = MDS[0,
                        (P[P_01, (P[P_02, b0] & 0xFF) ^ B0(k1)] & 0xFF) ^ B0(k0)];
                    sBox[2 * i + 1] = MDS[1,
                        (P[P_11, (P[P_12, b1] & 0xFF) ^ B1(k1)] & 0xFF) ^ B1(k0)];
                    sBox[0x200 + 2 * i] = MDS[2,
                        (P[P_21, (P[P_22, b2] & 0xFF) ^ B2(k1)] & 0xFF) ^ B2(k0)];
                    sBox[0x200 + 2 * i + 1] = MDS[3,
                        (P[P_31, (P[P_32, b3] & 0xFF) ^ B3(k1)] & 0xFF) ^ B3(k0)];
                }
            }

            object sessionKey = new object[] {sBox, subKeys};

            Console.WriteLine("S-box[]:");
            for (i = 0; i < 64; i++)
            {
                for (j = 0; j < 4; j++)
                    Console.Write("0x" + Utilities.ConvertToString(sBox[i * 4 + j]) + ", ");
                Console.WriteLine();
            }

            Console.WriteLine();
            for (i = 0; i < 64; i++)
            {
                for (j = 0; j < 4; j++) Console.Write("0x" + Utilities.ConvertToString(sBox[256 + i * 4 + j]) + ", ");
                Console.WriteLine();
            }

            Console.WriteLine();
            for (i = 0; i < 64; i++)
            {
                for (j = 0; j < 4; j++) Console.Write("0x" + Utilities.ConvertToString(sBox[512 + i * 4 + j]) + ", ");
                Console.WriteLine();
            }

            Console.WriteLine();
            for (i = 0; i < 64; i++)
            {
                for (j = 0; j < 4; j++) Console.Write("0x" + Utilities.ConvertToString(sBox[768 + i * 4 + j]) + ", ");
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("User (odd, even) keys  --> S-Box keys:");
            for (i = 0; i < k64Cnt; i++)
            {
                Console.WriteLine("0x" + Utilities.ConvertToString(k32o[i]) + "  0x" +
                                  Utilities.ConvertToString(k32e[i]) + " --> 0x" +
                                  Utilities.ConvertToString(sBoxKey[k64Cnt - 1 - i]));
            }

            Console.WriteLine();
            Console.WriteLine("Round keys:");
            for (i = 0; i < ROUND_SUBKEYS + 2 * ROUNDS; i += 2)
            {
                Console.WriteLine("0x" + Utilities.ConvertToString(subKeys[i]) + "  0x" +
                                  Utilities.ConvertToString(subKeys[i + 1]));
            }

            Console.WriteLine();


            return sessionKey;
        }

        private static BigInteger BlockEncrypt(BigInteger input, int inOffset, object sessionKey)
        {
            // extract S-box and session key
            var sk = (object[]) sessionKey;
            var sBox = (int[]) sk[0];
            var sKey = (int[]) sk[1];
            
            Console.WriteLine("pt=" + Utilities.ConvertToString(input));

            // Split up input
            var x0 = (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) | 
                     (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) << 8 | 
                     (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) << 16 | 
                     (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) << 24;
            
            var x1 = (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) | 
                     (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) << 8 | 
                     (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) << 16 | 
                     (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) << 24;
            var x2 = (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) | 
                     (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) << 8 | 
                     (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) << 16 | 
                     (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) << 24;
            
            var x3 = (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) |
                     (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) << 8 | 
                     (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) << 16 | 
                     (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) << 24;
            
            Console.WriteLine($"{x0}   {x1}   {x2}   {x3}");

            x0 ^= sKey[INPUT_WHITEN];
            x1 ^= sKey[INPUT_WHITEN + 1];
            x2 ^= sKey[INPUT_WHITEN + 2];
            x3 ^= sKey[INPUT_WHITEN + 3];

            Console.WriteLine("PTw=" 
                              + Utilities.ConvertToString(x0)
                              + Utilities.ConvertToString(x1)
                              + Utilities.ConvertToString(x2)
                              + Utilities.ConvertToString(x3));
            
            int t0, t1;
            var k = ROUND_SUBKEYS;
            for (var R = 0; R < ROUNDS; R += 2)
            {
                t0 = Fe32(sBox, x0, 0);
                t1 = Fe32(sBox, x1, 3);
                x2 ^= t0 + t1 + sKey[k++];
                x2 = TripleShift(x2, 1) | x2 << 31;
                x3 = x3 << 1 | TripleShift(x3, 31);
                x3 ^= t0 + 2 * t1 + sKey[k++];
                
                Console.WriteLine("CT" + (R) + "=" 
                                  + Utilities.ConvertToString(x0) 
                                  + Utilities.ConvertToString(x1) 
                                  + Utilities.ConvertToString(x2) 
                                  + Utilities.ConvertToString(x3));

                t0 = Fe32(sBox, x2, 0);
                t1 = Fe32(sBox, x3, 3);
                x0 ^= t0 + t1 + sKey[k++];
                x0 = TripleShift(x0, 1) | x0 << 31;
                x1 = x1 << 1 | TripleShift(x1, 31);
                x1 ^= t0 + 2 * t1 + sKey[k++];
                
                Console.WriteLine("CT" + (R + 1) + "=" 
                                  + Utilities.ConvertToString(x0)
                                  + Utilities.ConvertToString(x1)
                                  + Utilities.ConvertToString(x2)
                                  + Utilities.ConvertToString(x3));
            }

            x2 ^= sKey[OUTPUT_WHITEN];
            x3 ^= sKey[OUTPUT_WHITEN + 1];
            x0 ^= sKey[OUTPUT_WHITEN + 2];
            x1 ^= sKey[OUTPUT_WHITEN + 3];

            Console.WriteLine("CTw="
                              + Utilities.ConvertToString(x0)
                              + Utilities.ConvertToString(x1)
                              + Utilities.ConvertToString(x2)
                              + Utilities.ConvertToString(x3));

            byte[] answer = {
                (byte) x2, (byte) TripleShift(x2, 8), (byte) TripleShift(x2, 16), (byte) TripleShift(x2, 24),
                (byte) x3, (byte) TripleShift(x3, 8), (byte) TripleShift(x3, 16), (byte) TripleShift(x3, 24),
                (byte) x0, (byte) TripleShift(x0, 8), (byte) TripleShift(x0, 16), (byte) TripleShift(x0, 24),
                (byte) x1, (byte) TripleShift(x1, 8), (byte) TripleShift(x1, 16), (byte) TripleShift(x1, 24),
            };

            var result = toBigInt(answer);
            
            Console.WriteLine("CT=" + Utilities.ConvertToString(result));
            Console.WriteLine();

            return result;
        }

        private static BigInteger BlockDecrypt(BigInteger input, int inOffset, object sessionKey)
        {
            // extract S-box and session key
            var sk = (object[]) sessionKey;
            var sBox = (int[]) sk[0];
            var sKey = (int[]) sk[1];

            Console.WriteLine("ct=" + Utilities.ConvertToString(input));

            // Split up input
            var x2 = (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) |
                     (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) << 8 |
                     (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) << 16 |
                     (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) << 24;

            var x3 = (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) |
                     (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) << 8 |
                     (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) << 16 |
                     (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) << 24;
            var x0 = (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) |
                     (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) << 8 |
                     (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) << 16 |
                     (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) << 24;

            var x1 = (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) |
                     (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) << 8 |
                     (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) << 16 |
                     (GetByteAtPos(input, inOffset++, INPUT_LENGTH) & 0xFF) << 24;

            Console.WriteLine($"{x0}   {x1}   {x2}   {x3}");

            x2 ^= sKey[OUTPUT_WHITEN];
            x3 ^= sKey[OUTPUT_WHITEN + 1];
            x0 ^= sKey[OUTPUT_WHITEN + 2];
            x1 ^= sKey[OUTPUT_WHITEN + 3];
            Console.WriteLine("CTw="
                              + Utilities.ConvertToString(x2)
                              + Utilities.ConvertToString(x3)
                              + Utilities.ConvertToString(x0)
                              + Utilities.ConvertToString(x1));

            var k = ROUND_SUBKEYS + 2 * ROUNDS - 1;
            int t0, t1;

            for (var R = 0; R < ROUNDS; R += 2)
            {
                t0 = Fe32(sBox, x2, 0);
                t1 = Fe32(sBox, x3, 3);
                x1 ^= t0 + 2 * t1 + sKey[k--];

                x1 = TripleShift(x1, 1) | x1 << 31;
                x0 = x0 << 1 | TripleShift(x0, 31);
                x0 ^= t0 + t1 + sKey[k--];

                Console.WriteLine("PT" + (ROUNDS - R) + "=" + Utilities.ConvertToString(x2) +
                                  Utilities.ConvertToString(x3) + Utilities.ConvertToString(x0) +
                                  Utilities.ConvertToString(x1));

                t0 = Fe32(sBox, x0, 0);
                t1 = Fe32(sBox, x1, 3);
                x3 ^= t0 + 2 * t1 + sKey[k--];
                x3 = TripleShift(x3, 1) | x3 << 31;
                x2 = x2 << 1 | TripleShift(x2, 31);
                x2 ^= t0 + t1 + sKey[k--];

                Console.WriteLine("PT" + (ROUNDS - R - 1) + "=" + Utilities.ConvertToString(x2) +
                                  Utilities.ConvertToString(x3) + Utilities.ConvertToString(x0) +
                                  Utilities.ConvertToString(x1));
            }

            x0 ^= sKey[INPUT_WHITEN];
            x1 ^= sKey[INPUT_WHITEN + 1];
            x2 ^= sKey[INPUT_WHITEN + 2];
            x3 ^= sKey[INPUT_WHITEN + 3];

            Console.WriteLine("PTw="
                              + Utilities.ConvertToString(x2)
                              + Utilities.ConvertToString(x3)
                              + Utilities.ConvertToString(x0)
                              + Utilities.ConvertToString(x1));

            byte[] answer = {
                (byte) x0, (byte) TripleShift(x0, 8), (byte) TripleShift(x0, 16), (byte) TripleShift(x0, 24),
                (byte) x1, (byte) TripleShift(x1, 8), (byte) TripleShift(x1, 16), (byte) TripleShift(x1, 24),
                (byte) x2, (byte) TripleShift(x2, 8), (byte) TripleShift(x2, 16), (byte) TripleShift(x2, 24),
                (byte) x3, (byte) TripleShift(x3, 8), (byte) TripleShift(x3, 16), (byte) TripleShift(x3, 24),
            };

            var result = toBigInt(answer);

            Console.WriteLine("PT=" + Utilities.ConvertToString(result));
            Console.WriteLine();

            return result;
        }

        private static BigInteger toBigInt(byte[] arr)
        {
            var rev = new byte[arr.Length];
            for (int i = 0, j = arr.Length - 1; j >= 0; i++, j--)
                rev[j] = arr[i];
            return new BigInteger(rev);
        }

        public Twofish()
        {
            PrecomputeMDSMatrix();
        }

        public override bool IsValidInput(BigInteger value)
        {
            return Utilities.NumberOfBits(value) <= 128;
        }

        public override bool IsValidKey(BigInteger value)
        {
            return Utilities.NumberOfBits(value) <= 256;
        }

        public override BigInteger Encrypt(BigInteger plaintext, BigInteger key)
        {
            return BlockEncrypt(plaintext, 0, MakeKey(key));
        }

        public override BigInteger Decrypt(BigInteger ciphertext, BigInteger key)
        {
            return BlockDecrypt(ciphertext, 0, MakeKey(key));
        }
    }
}