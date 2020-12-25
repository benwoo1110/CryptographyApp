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
            if (ciphers.ContainsKey(cipher.Name))
            {
                throw new ArgumentException("You cannot register ciphers with the same name!");
            }
            
            ciphers.Add(cipher.Name, cipher);
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
            selectedCipher = ciphers.GetValueOrDefault(cipherName);
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
            if (result.HasInvalidInputAndKey())
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
                result.ValidInput = ConvertResult.ParseError;
                return;
            }
            
            BigInteger? parsedKey = Utilities.ConvertToBigInt(key, result.TextType);
            if (parsedKey == null)
            {
                result.ValidKey = ConvertResult.ParseError;
                return;
            }

            result.InputNumber = (BigInteger) parsedInput;
            result.KeyNumber = (BigInteger) parsedKey;
        }

        private void ValidateCipherInputs(CipherResult result)
        {
            result.ValidInput = Utilities.ValidationResult(selectedCipher.IsValidInput(result.InputNumber)); 
            result.ValidKey = Utilities.ValidationResult(selectedCipher.IsValidKey(result.KeyNumber));
        }

        private void ExecuteCipherAlgorithm(CipherResult result)
        {
            BigInteger output = result.CipherMode.Equals(Mode.Encrypt)
                ? selectedCipher.Encrypt(result.InputNumber, result.KeyNumber)
                : selectedCipher.Decrypt( result.InputNumber, result.KeyNumber);

            result.OutputNumber = output;
            result.OutputText = Utilities.ConvertToString(output, result.TextType);
        }

        public string GetCurrentSelected()
        {
            return HasAnySelected() ? selectedCipher.Name : null;
        }
        
        public bool HasAnySelected()
        {
            return selectedCipher != null;
        }

        public string[] GetAvailableCiphers()
        {
            return ciphers.Keys.ToArray();
        }

        public bool HasSuchCipher(string cipherName)
        {
            return ciphers.ContainsKey(cipherName);
        }

        public void Reset()
        {
            selectedCipher = null;
            TextType = InputType.Hex;
            CipherMode = Mode.Encrypt;
        }
    }
}
