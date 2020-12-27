using System;
using Cryptography.Core;
using Cryptography.Core.Ciphers;

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

            if (!result.HasValidInputAndKey())
            {
                Console.WriteLine("Invalid input/key.");
            }

            Console.WriteLine(result.ToString());
        }
    }
}
