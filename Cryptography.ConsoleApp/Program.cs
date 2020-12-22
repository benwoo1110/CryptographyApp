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
            
            cipherFactory.RegisterCipher(new Blowfish());
            cipherFactory.RegisterCipher(new IDEA());
            cipherFactory.RegisterCipher(new RC5());
            cipherFactory.RegisterCipher(new Twofish());
            cipherFactory.RegisterCipher(new SimpleTest());

            cipherFactory.SelectCipher("Blowfish");

            cipherFactory.CipherMode = Mode.Encrypt;
            CipherResult result = cipherFactory.RunCipher(Utilities.ConvertToString(BigInteger.Parse("2343464") , InputType.Hex), "4B7A70E9B5B32944DB75092EC4192623AD6EA6B049A7DF7D9CEE60B88FEDB266ECAA8C71699A17FF5664526CC2B19EE1193602A575094C29");
            
            Console.WriteLine(result);
        }
    }
}
