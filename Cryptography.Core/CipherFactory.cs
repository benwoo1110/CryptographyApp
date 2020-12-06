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
                throw new ArgumentException();
            }
            
            CipherResult result = new CipherResult(selectedCipher.Name, TextType, input, key, CipherMode);

            BigInteger? parsedInput = Utilities.ConvertToBigInt(input, TextType);
            if (parsedInput == null)
            {
                result.ValidInput = TextOutcome.ParseError;
                return result;
            }
            
            BigInteger? parsedKey = Utilities.ConvertToBigInt(key, TextType);
            if (parsedKey == null)
            {
                result.ValidKey = TextOutcome.ParseError;
                return result;
            }

            BigInteger targetInput = (BigInteger) parsedInput;
            BigInteger targetKey = (BigInteger) parsedKey;

            result.InputNumber = targetInput;
            result.KeyNumber = targetKey;

            result.ValidInput = Utilities.ConvertValidationResult(selectedCipher.IsValidInput(targetInput)); 
            result.ValidKey = Utilities.ConvertValidationResult(selectedCipher.IsValidInput(targetKey));

            if (!result.HasValidInputAndKey())
            {
                return result;
            }
            
            BigInteger output = CipherMode.Equals(Mode.Encrypt)
                ? selectedCipher.Encrypt(targetInput, targetKey)
                : selectedCipher.Decrypt( targetInput, targetKey);

            result.OutputNumber = output;
            result.OutputText = Utilities.ConvertToString(output, TextType);

            return result;
        }
    }
}
