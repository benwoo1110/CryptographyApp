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
            cipherFactory.RegisterCipher(new Twofish());
            cipherFactory.RegisterCipher(new RC5());
            
            Menu cipherMenu = new Menu(
                "------ Select the cipher ------",
                cipherFactory.GetAvailableCiphers().ToArray()
            );

            Menu typeMenu = new Menu(
                "------ Select input type ------",
                Enum.GetNames(typeof(InputType))
            );
            
            Menu modeMenu = new Menu(
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
                string input = ConsoleHelper.GetInput("Enter input: ");
                string key = ConsoleHelper.GetInput("Enter key: ");
                
                CipherResult result = cipherFactory.RunCipher(input, key);
                
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
