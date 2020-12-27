using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Cryptography.Core.Ciphers;
using Cryptography.Core.Enums;

namespace Cryptography.Core
{
    public class CipherFactory
    {
        private readonly Dictionary<string, Cipher> ciphers;
        private Cipher selectedCipher;

        public InputType TextType { get; set; }
        
        public Mode CipherMode { get; set; }

        public CipherFactory()
        {
            ciphers = new Dictionary<string, Cipher>();
            Reset();
        }

        public void RegisterCipher(Cipher cipher)
        {
            string name = cipher.Name.ToLower();
            if (ciphers.ContainsKey(name))
            {
                throw new ArgumentException("You cannot register ciphers with the same name!");
            }
            
            ciphers.Add(name, cipher);
        }
        
        public bool SetCipherMode(string cipherMode)
        {
            if (Enum.TryParse(Utilities.Capitalise(cipherMode), out Mode mode))
            {
                CipherMode = mode;
                return true;
            }

            return false;
        }
        
        public bool SetTextType(string textType)
        {
            if (Enum.TryParse(Utilities.Capitalise(textType), out InputType type))
            {
                TextType = type;
                return true;
            }

            return false;
        }

        public bool SelectCipher(string cipherName)
        {
            selectedCipher = ciphers.GetValueOrDefault(cipherName.ToLower());
            return selectedCipher != null;
        }

        public CipherResult RunCipher(string input, string key)
        {
            if (selectedCipher == null || input == null || key == null)
            {
                throw new ArgumentNullException();
            }
            
            CipherResult result = new CipherResult(selectedCipher.Name, TextType, CipherMode, input, key);

            ParseCipherInputs(input, key, result);
            if (result.HasParsingErrors())
            {
                return result;
            }

            ValidateCipherInputs(result);
            if (!result.HasValidInputAndKey())
            {
                return result;
            }

            ExecuteCipherAlgorithm(result);
            
            return result;
        }

        private void ParseCipherInputs(string input, string key, CipherResult result)
        {
            BigInteger? parsedInput = Utilities.ConvertToBigInt(input, result.TextType);
            if (parsedInput == null)
            {
                result.Input.State = ConvertResult.ParseError;
                return;
            }
            
            BigInteger? parsedKey = Utilities.ConvertToBigInt(key, result.TextType);
            if (parsedKey == null)
            {
                result.Input.State = ConvertResult.ParseError;
                return;
            }

            result.Input.Number = (BigInteger) parsedInput;
            result.Key.Number = (BigInteger) parsedKey;
        }

        private void ValidateCipherInputs(CipherResult result)
        {
            result.Input.State = Utilities.ValidationResult(selectedCipher.IsValidInput(result.Input.Number)); 
            result.Key.State = Utilities.ValidationResult(selectedCipher.IsValidKey(result.Key.Number));
        }

        private void ExecuteCipherAlgorithm(CipherResult result)
        {
            BigInteger output = result.CipherMode.Equals(Mode.Encrypt)
                ? selectedCipher.Encrypt(result.Input.Number, result.Key.Number)
                : selectedCipher.Decrypt( result.Input.Number, result.Key.Number);

            result.Output.Number = output;
            result.Output.State = Utilities.ValidationResult(selectedCipher.IsValidInput(output));
            if (!result.Output.IsValid())
            {
                return;
            }

            string parsedResult = Utilities.ConvertToString(output, result.TextType);
            if (string.IsNullOrEmpty(parsedResult))
            {
                result.Output.State = ConvertResult.ParseError;
                return;
            }

            result.Output.Text = parsedResult;
        }

        public string GetCurrentSelected()
        {
            return HasAnySelected() ? selectedCipher.Name : null;
        }
        
        public bool HasAnySelected()
        {
            return selectedCipher != null;
        }

        public List<string> GetAvailableCiphers()
        {
            return ciphers.Values.ToList().ConvertAll(c => c.Name);
        }

        public bool HasSuchCipher(string cipherName)
        {
            return ciphers.ContainsKey(cipherName.ToLower());
        }

        public void Reset()
        {
            selectedCipher = null;
            TextType = InputType.Hex;
            CipherMode = Mode.Encrypt;
        }
    }
}
