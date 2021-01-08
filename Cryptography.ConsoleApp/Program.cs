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
                cipherFactory.SelectCipher(cipherMenu.GetMenuOption());

                cipherFactory.SetCipherMode(modeMenu.GetMenuOption());

                cipherFactory.SetTextType(typeMenu.GetMenuOption());

                Console.WriteLine();
                Console.WriteLine("------ Please enter the input and key ------");
                Console.Write("Enter input: ");
                string input = Console.ReadLine();
                Console.Write("Enter key: ");
                string key = Console.ReadLine();
                CipherResult result = cipherFactory.RunCipher(input, key);
                Console.WriteLine(result);
                
                while (true)
                {
                    Console.WriteLine();
                    Console.Write("Do you want to run another cipher? Y/N: ");
                    string response = Console.ReadLine().ToUpper();
                    if (response == "N")
                    {
                        Console.WriteLine("Exiting...");
                        return;
                    }
                    else if (response == "Y")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please try again.");
                    }
                }
            }
        }
    }
}
