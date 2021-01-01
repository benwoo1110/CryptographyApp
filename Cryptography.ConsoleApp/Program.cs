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

            // Init cipher algorithms
            cipherFactory.RegisterCipher(new Blowfish());
            cipherFactory.RegisterCipher(new IDEA());

            Console.WriteLine("------ Please enter the input and key ------");
            Console.Write("Enter input: ");
            string input = Console.ReadLine();
            Console.Write("Enter key: ");
            string key = Console.ReadLine();
            
            int selectCipher = DisplayCipherMenu();
            while (true)
            {
                if (selectCipher == 1)
                {
                    cipherFactory.SelectCipher("IDEA");
                    Console.WriteLine("Selected IDEA cipher!");
                    break;
                } else if (selectCipher == 2)
                {
                    cipherFactory.SelectCipher("Blowfish");
                    Console.WriteLine("Selected Blowfish cipher!");
                    break;
                } else if (selectCipher == 3)
                {
                    cipherFactory.SelectCipher("Twofish");
                    Console.WriteLine("Selected Twofish cipher!");
                    break;
                } else if (selectCipher == 4)
                {
                    cipherFactory.SelectCipher("RC5");
                    Console.WriteLine("Selected RC5 cipher!");
                    break;
                } else if (selectCipher == 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid cipher!");
                    break;
                }
            }

            int selectMode = DisplayModeMenu();
            while (true)
            {
                if (selectMode == 1)
                {
                    cipherFactory.CipherMode = Mode.Encrypt;
                    cipherFactory.SetCipherMode("Encrypt");
                    Console.WriteLine("Selected Encryption!");
                    break;
                } else if (selectMode == 2)
                {
                    cipherFactory.CipherMode = Mode.Decrypt;
                    cipherFactory.SetCipherMode("Decrypt");
                    Console.WriteLine("Selected Decryption!");
                    break;
                } else if (selectMode == 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input!");
                    break;
                }
            }

            CipherResult result;
            int selectType = DisplayTypeMenu();
            while (true)
            {
                if (selectType == 1)
                {
                    result = cipherFactory.RunCipher(Utilities.ConvertToString(BigInteger.Parse(input), InputType.Decimal) , key);
                    cipherFactory.SetTextType("Decimal");
                    Console.WriteLine("Selected Decimal type!");
                    break;
                } else if (selectType == 2)
                {
                    result = cipherFactory.RunCipher(Utilities.ConvertToString(BigInteger.Parse(input), InputType.Hex) , key);
                    cipherFactory.SetTextType("Hex");
                    Console.WriteLine("Selected Hex type!");
                    break;
                } else if (selectType == 3)
                {
                    result = cipherFactory.RunCipher(Utilities.ConvertToString(BigInteger.Parse(input), InputType.Binary) , key);
                    cipherFactory.SetTextType("Binary");
                    Console.WriteLine("Selected Binary type!");
                    break;
                } else if (selectType == 4)
                {
                    result = cipherFactory.RunCipher(Utilities.ConvertToString(BigInteger.Parse(input), InputType.Ascii) , key);
                    cipherFactory.SetTextType("Ascii");
                    Console.WriteLine("Selected Ascii type!");
                    break;
                } else if (selectType == 0)
                {
                    result = null;
                    break;
                }
                else
                {
                    result = null;
                    Console.WriteLine("Invalid type!");
                    break;
                }
            }
            // CipherResult result = cipherFactory.RunCipher(Utilities.ConvertToString(BigInteger.Parse("2343464") , InputType.Hex), "4B7A70E9B5B32944DB75092EC4192623AD6EA6B049A7DF7D9CEE60B88FEDB266ECAA8C71699A17FF5664526CC2B19EE1193602A575094C29");

            if (!result.HasValidInputAndKey())
            {
                Console.WriteLine("Invalid input/key.");
            }

            Console.WriteLine(result.ToString());
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
            int option = Convert.ToInt32(Console.ReadLine());
            return option;
        }
        
        static int DisplayModeMenu()
        {
            Console.WriteLine();
            Console.WriteLine("------ Select the mode ------");
            Console.WriteLine("[1] Encrypt");
            Console.WriteLine("[2] Decrypt");
            Console.WriteLine("[0] Exit");
            Console.Write("Enter option: ");
            int option = Convert.ToInt32(Console.ReadLine());
            return option;
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
            int option = Convert.ToInt32(Console.ReadLine());
            return option;
        }
    }
}
