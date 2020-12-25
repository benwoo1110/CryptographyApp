using System;
using System.Numerics;
using Cryptography.Core;
using Cryptography.Core.Ciphers;
using Cryptography.Core.Enums;

namespace Cryptography.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CipherFactory cipherFactory = new CipherFactory();

            // Init cipher algorithms
            cipherFactory.RegisterCipher(new Blowfish());
            cipherFactory.RegisterCipher(new IDEA());

<<<<<<< HEAD
            cipherFactory.SelectCipher("Blowfish");

            cipherFactory.CipherMode = Mode.Encrypt;
            CipherResult result = cipherFactory.RunCipher(Utilities.ConvertToString(BigInteger.Parse("2343464") , InputType.Hex), "4B7A70E9B5B32944DB75092EC4192623AD6EA6B049A7DF7D9CEE60B88FEDB266ECAA8C71699A17FF5664526CC2B19EE1193602A575094C29");
            
            Console.WriteLine(result);
=======
            // Set cipher algorithm to use
            if (cipherFactory.SelectCipher("IDEA"))
            {
                Console.WriteLine("Selected cipher!");
            }
            else
            {
                Console.WriteLine("Invalid cipher!");
            }

            // Set cipher mode -> Encrypt / Decrypt
            cipherFactory.SetCipherMode("Encrypt");

            // Text type:
            // Decimal -> 1234567890
            // Hex -> 1234567890ABCDE
            // Binary -> 01
            // Ascii -> A -> table to convert to hex
            cipherFactory.SetTextType("Binary");

            string input = "10101010002";
            string key = "11101100";

            // Run the cipher algorithm
            CipherResult result = cipherFactory.RunCipher(input, key);

            if (result.HasInvalidInputAndKey())
            {
                Console.WriteLine("Invalid input/key.");
            }

            Console.WriteLine(result.ToString());
>>>>>>> 7dcbe6f7d0f6acfe4e328f34acad999dd9f8f518
        }
    }
}
