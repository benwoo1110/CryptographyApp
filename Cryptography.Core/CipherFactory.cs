using System;
using System.Collections.Generic;
using System.Numerics;
using Cryptography.Core.Ciphers;
using Cryptography.Core.Enums;

namespace Cryptography.Core
{
    public class CipherFactory
    {
        private Dictionary<string, Cipher> ciphers;
        private Cipher selectedCipher;

        public InputType TextType { get; set; }
        
        public Mode CipherMode { get; set; }

        public CipherFactory()
        {
            ciphers = new Dictionary<string, Cipher>();
            TextType = InputType.Hex;
            CipherMode = Mode.Encrypt;
        }

        public void RegisterCipher(Cipher cipher)
        {
            ciphers.Add(cipher.Name, cipher);
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
    }
}
