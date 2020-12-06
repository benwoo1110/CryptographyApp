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

            BigInteger? parsedInput = Utilities.ConvertToBigInt(input, TextType);
            if (parsedInput == null)
            {
                result.ValidInput = TextOutcome.ParseError;
                return result;
            }
            
            BigInteger? parsedKey = Utilities.ConvertToBigInt(input, TextType);
            if (parsedKey == null)
            {
                result.ValidKey = TextOutcome.ParseError;
                return result;
            }

            result.ValidInput = Utilities.ConvertValidationResult(selectedCipher.IsValidInput((int) parsedInput)); 
            result.ValidKey = Utilities.ConvertValidationResult(selectedCipher.IsValidInput((int) parsedInput));

            if (result.ValidInput.Equals(TextOutcome.Invalid) || result.ValidKey.Equals(TextOutcome.Invalid))
            {
                return result;
            }

            BigInteger output = CipherMode.Equals(Mode.Encrypt)
                ? selectedCipher.Encrypt((BigInteger) parsedInput, (BigInteger) parsedKey)
                : selectedCipher.Decrypt( (BigInteger) parsedInput, (BigInteger) parsedKey);

            result.OutputText = Utilities.ConvertToString(output, TextType);

            return result;
        }
    }
}
