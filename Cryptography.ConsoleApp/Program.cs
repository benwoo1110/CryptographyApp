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
            Console.WriteLine(Utilities.ConvertToString(BigInteger.Parse("-141330175087587971603928506392887882537")));
            Console.WriteLine(Utilities.ConvertToString(BigInteger.Parse("198952191833350491859446101038880328919")));

            
            var cipherFactory = new CipherFactory();

            cipherFactory.RegisterCipher(new Blowfish());
            cipherFactory.RegisterCipher(new IDEA());
            cipherFactory.RegisterCipher(new Twofish());
            cipherFactory.RegisterCipher(new RC5());
            
            var cipherMenu = new Menu(
                "------ Select the cipher ------",
                cipherFactory.GetAvailableCiphers().ToArray()
            );

            var typeMenu = new Menu(
                "------ Select input type ------",
                Enum.GetNames(typeof(InputType))
            );
            
            var modeMenu = new Menu(
                "------ Select the mode ------",
                Enum.GetNames(typeof(Mode))
            );

            while (true)
            {
                cipherFactory.SelectCipher(cipherMenu.RunMenuOption());
                cipherFactory.SetCipherMode(modeMenu.RunMenuOption());
                cipherFactory.SetTextType(typeMenu.RunMenuOption());

                ConsoleHelper.EmptyLine();
                Console.WriteLine("------ Please enter the input and key ------");
                var input = ConsoleHelper.GetInput("Enter input: ");
                var key = ConsoleHelper.GetInput("Enter key: ");
                
                var result = cipherFactory.RunCipher(input, key);
                
                ConsoleHelper.EmptyLine();
                Console.WriteLine("------ Cipher Result Report ------");
                Console.WriteLine(result);
                
                ConsoleHelper.EmptyLine();
                if (!ConsoleHelper.Confirm("Do you want to run another cipher?"))
                {
                    ConsoleHelper.ExitProgram();
                }
            }
        }
    }
}
