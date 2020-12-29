using System;
using System.Numerics;
using Cryptography.Core.Ciphers;
using Cryptography.Core.Enums;

namespace Cryptography.Core
{
    public class CipherRunInstance
    {
        private CipherResult Result { get; set; }
        private Cipher SelectedCipher { get; }
        private Mode CipherMode { get; }
        private InputType TextType { get; }

        public CipherRunInstance(Cipher selectedCipher, Mode cipherMode, InputType textType)
        {
            SelectedCipher = selectedCipher;
            CipherMode = cipherMode;
            TextType = textType;
        }

        public CipherResult Run(string input, string key)
        {
            if (!SetUp(input, key) 
                || !ParseInputs() 
                || !ValidateCipherInputs() 
                || !ExecuteCipherAlgorithm())
            {
                return Result;
            }

            ParseOutput();
            return Result;
        }

        private bool SetUp(string input, string key)
        {
            input = Utilities.TrimText(input);
            key = Utilities.TrimText(key);

            if (SelectedCipher == null || string.IsNullOrEmpty(input) || string.IsNullOrEmpty(key))
            {
                return false;
            }

            Result = new CipherResult(SelectedCipher.Name, TextType, CipherMode, input, key);
            return true;
        }

        private bool ParseInputs()
        {
            BigInteger? parsedInput = Utilities.ConvertToBigInt(Result.Input.Text, TextType);
            if (parsedInput == null)
            {
                Result.Input.State = ConvertResult.ParseError;
                return false;
            }
            Result.Input.Number = (BigInteger) parsedInput;


            BigInteger? parsedKey = Utilities.ConvertToBigInt(Result.Key.Text, TextType);
            if (parsedKey == null)
            {
                Result.Input.State = ConvertResult.ParseError;
                return false;
            }
            Result.Key.Number = (BigInteger) parsedKey;

            return true;
        }

        private bool ValidateCipherInputs()
        {
            Result.Input.State = Utilities.ValidationResult(SelectedCipher.IsValidInput(Result.Input.Number));
            Result.Key.State = Utilities.ValidationResult(SelectedCipher.IsValidKey(Result.Key.Number));
            
            return Result.HasValidInputAndKey();
        }

        private bool ExecuteCipherAlgorithm()
        {
            try
            {
                Result.Output.Number = Result.CipherMode.Equals(Mode.Encrypt)
                    ? SelectedCipher.Encrypt(Result.Input.Number, Result.Key.Number)
                    : SelectedCipher.Decrypt(Result.Input.Number, Result.Key.Number);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Result.Output.State = ConvertResult.RunTimeError;
                return false;
            }

            return true;
        }

        private void ParseOutput()
        {
            Result.Output.State = Utilities.ValidationResult(SelectedCipher.IsValidInput(Result.Output.Number));
            if (!Result.Output.IsValid())
            {
                return;
            }

            string parsedResult = Utilities.ConvertToString(Result.Output.Number, TextType);
            if (string.IsNullOrEmpty(parsedResult))
            {
                Result.Output.State = ConvertResult.ParseError;
                return;
            }

            Result.Output.Text = parsedResult;
        }
    }
}
