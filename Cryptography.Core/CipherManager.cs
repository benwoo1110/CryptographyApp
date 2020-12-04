using System;
using System.Collections.Generic;
using Cryptography.Core.Ciphers;
using Cryptography.Core.Enums;

namespace Cryptography.Core
{
    public class CipherManager
    {
        private Dictionary<string, Cipher> ciphers;
        private Cipher selectedCipher;

        public InputType TextType { get; set; }
        
        public Mode CipherMode { get; set; }

        public CipherManager()
        {
            ciphers = new Dictionary<string, Cipher>();
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
            if (selectedCipher == null)
            {
                throw new InvalidOperationException("Invalid selected cipher.");
            }
            
            CipherResult result = new CipherResult(input, key, CipherMode);

            ulong? parsedInput = Utilities.ConvertToInt(input, TextType);
            if (parsedInput == null)
            {
                result.ValidInput = TextOutcome.ParseError;
                return result;
            }
            
            ulong? parsedKey = Utilities.ConvertToInt(input, TextType);
            if (parsedKey == null)
            {
                result.ValidKey = TextOutcome.ParseError;
                return result;
            }

            Utilities.ConvertValidationResult(selectedCipher.IsValidInput((int) parsedInput)); 
            Utilities.ConvertValidationResult(selectedCipher.IsValidInput((int) parsedInput));

            ulong output = CipherMode.Equals(Mode.Encrypt)
                ? selectedCipher.Encrypt((ulong) parsedInput, (ulong) parsedKey)
                : selectedCipher.Decrypt((ulong) parsedInput, (ulong) parsedKey);

            result.OutputText = Utilities.ConvertToString(output, TextType);

            return result;
        }
    }
}
