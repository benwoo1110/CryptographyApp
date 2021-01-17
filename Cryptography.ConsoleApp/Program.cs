using System;
using System.Numerics;
using Cryptography.Core;
using Cryptography.Core.Ciphers;

namespace Cryptography.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CipherFactory cipherFactory = new CipherFactory();

            cipherFactory.RegisterCipher(new Blowfish());
            cipherFactory.RegisterCipher(new IDEA());

            while (true)
            {
                while (true)
                {
                    int selectCipher = DisplayCipherMenu();
                    if (selectCipher == 1)
                    {
                        cipherFactory.SelectCipher("IDEA");
                        Console.WriteLine("Selected IDEA cipher!");
                        break;
                    }
                    else if (selectCipher == 2)
                    {
                        cipherFactory.SelectCipher("Blowfish");
                        Console.WriteLine("Selected Blowfish cipher!");
                        break;
                    }
                    else if (selectCipher == 3)
                    {
                        cipherFactory.SelectCipher("Twofish");
                        Console.WriteLine("Selected Twofish cipher!");
                        break;
                    }
                    else if (selectCipher == 4)
                    {
                        cipherFactory.SelectCipher("RC5");
                        Console.WriteLine("Selected RC5 cipher!");
                        break;
                    }
                    else if (selectCipher == 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid cipher! Please try again.");
                    }
                }


                while (true)
                {
                    int selectMode = DisplayModeMenu();
                    if (selectMode == 1)
                    {
                        cipherFactory.SetCipherMode("Encrypt");
                        Console.WriteLine("Selected Encryption!");
                        break;
                    }
                    else if (selectMode == 2)
                    {
                        cipherFactory.SetCipherMode("Decrypt");
                        Console.WriteLine("Selected Decryption!");
                        break;
                    }
                    else if (selectMode == 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input! Please try again.");
                    }
                }



                while (true)
                {
                    int selectType = DisplayTypeMenu();
                    if (selectType == 1)
                    {
                        cipherFactory.SetTextType("Decimal");
                        Console.WriteLine("Selected Decimal type!");
                        break;
                    }
                    else if (selectType == 2)
                    {
                        cipherFactory.SetTextType("Hex");
                        Console.WriteLine("Selected Hex type!");
                        break;
                    }
                    else if (selectType == 3)
                    {
                        cipherFactory.SetTextType("Binary");
                        Console.WriteLine("Selected Binary type!");
                        break;
                    }
                    else if (selectType == 4)
                    {
                        cipherFactory.SetTextType("Ascii");
                        Console.WriteLine("Selected Ascii type!");
                        break;
                    }
                    else if (selectType == 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid type! Please try again.");
                    }
                }

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
                    Console.Write("Do you want to run another cipher? Y/N:");
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

        static int DisplayCipherMenu()
        {
            Console.WriteLine();
            Console.WriteLine("------ Select the cipher ------");
            Console.WriteLine("[1] IDEA");
            Console.WriteLine("[2] Blowfish");
            Console.WriteLine("[3] TwoFish");
            Console.WriteLine("[4] RC5");
            Console.WriteLine("[0] Exit");
            Console.Write("Enter option: ");
            try
            {
                return Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                return -1;
            }
        }

        static int DisplayModeMenu()
        {
            Console.WriteLine();
            Console.WriteLine("------ Select the mode ------");
            Console.WriteLine("[1] Encrypt");
            Console.WriteLine("[2] Decrypt");
            Console.WriteLine("[0] Exit");
            Console.Write("Enter option: ");
            try
            {
                return Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                return -1;
            }
        }

        static int DisplayTypeMenu()
        {
            Console.WriteLine();
            Console.WriteLine("------ Select input type ------");
            Console.WriteLine("[1] Decimal");
            Console.WriteLine("[2] Hex");
            Console.WriteLine("[3] Binary");
            Console.WriteLine("[4] Ascii");
            Console.WriteLine("[0] Exit");
            Console.Write("Enter option: ");
            try
            {
                return Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
