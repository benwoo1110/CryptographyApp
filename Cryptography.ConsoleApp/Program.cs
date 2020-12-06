using System;
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
            
            cipherFactory.RegisterCipher(new Blowfish());
            cipherFactory.RegisterCipher(new IDEA());
            cipherFactory.RegisterCipher(new RC5());
            cipherFactory.RegisterCipher(new Twofish());
            cipherFactory.RegisterCipher(new SimpleTest());

            cipherFactory.SelectCipher("SimpleTest");

            cipherFactory.CipherMode = Mode.Decrypt;
            CipherResult result = cipherFactory.RunCipher("F0", "20");
            
            Console.WriteLine(result);
        }
    }
}