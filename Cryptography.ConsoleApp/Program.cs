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

            cipherFactory.SelectCipher("IDEA");

            cipherFactory.CipherMode = Mode.Encrypt;
            CipherResult result = cipherFactory.RunCipher("05320a6414c819fa", "006400c8012c019001f4025802bc0320");
            
            Console.WriteLine(result);
        }
    }
}
