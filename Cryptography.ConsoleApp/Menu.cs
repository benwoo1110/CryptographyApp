using System;
using System.Collections.Generic;

namespace Cryptography.ConsoleApp
{
    public class Menu
    {
        private readonly string header;
        private readonly string[] contents;

        public Menu(string header, string[] contents)
        {
            this.header = header;
            this.contents = contents;
        }

        public string GetMenuOption()
        {
            ShowMenu();
            return GetInputOption();
        }

        private string GetInputOption()
        {
            while (true)
            {
                Console.Write("Enter option: ");
                int input;
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Input is not a number, please try again!");
                    continue;
                }
                
                if (input < 0 || input > contents.Length)
                {
                    Console.WriteLine($"Please enter a number between 0 and {contents.Length}!");
                    continue;
                }
                
                if (input == 0)
                {
                    Console.WriteLine("Exiting...");
                    Environment.Exit(0);
                }

                return contents[input - 1];
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine(header);
            for (var index = 0; index < contents.Length; index++)
            {
                Console.WriteLine($"[{index + 1}] {contents[index]}");
            }
            Console.WriteLine("[0] Exit");
        }
    }
}