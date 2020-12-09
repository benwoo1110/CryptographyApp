using System.Numerics;
using System.Text;
using Cryptography.Core.Enums;

namespace Cryptography.Core
{
    public class CipherResult
    {
        public ConvertResult ValidInput { get; set; }
        
        public ConvertResult ValidKey { get; set; }
        
        public string InputText { get; set; }
        
        public string KeyText { get; set; }

        public string OutputText { get; set; }
        
        public BigInteger InputNumber { get; set; }
        
        public BigInteger KeyNumber { get; set; }

        public BigInteger OutputNumber { get; set; }
        
        public string CipherName { get; set; }
        
        public InputType TextType { get; set; }
        
        public Mode CipherMode { get; set; }

        public CipherResult(string cipherName, InputType textType, Mode cipherMode, string inputText, string keyText)
        {
            CipherName = cipherName;
            TextType = textType;
            CipherMode = cipherMode;
            InputText = inputText;
            KeyText = keyText;
            ValidInput = ConvertResult.Unknown;
            ValidInput = ConvertResult.Unknown;
        }

        public bool HasParsingErrors()
        {
            return ValidInput.Equals(ConvertResult.ParseError) || ValidKey.Equals(ConvertResult.ParseError);
        }

        public bool HasInvalidInputAndKey()
        {
            return !ValidInput.Equals(ConvertResult.Valid) || !ValidKey.Equals(ConvertResult.Valid);
        }

        public bool HasOutput()
        {
            return OutputText != null;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("Cipher Name: ").Append(CipherName).Append("\n")
                .Append("Text Type: ").Append(TextType).Append("\n")
                .Append("Cipher Mode: ").Append(CipherMode).Append("\n")
                .Append("Input Text: ").Append(InputText).Append("\n")
                .Append("Key Text: ").Append(KeyText).Append("\n")
                .Append("Input State: ").Append(ValidInput).Append("\n")
                .Append("Key State: ").Append(ValidKey).Append("\n")
                .Append("Input Number: ").Append(InputNumber).Append("\n")
                .Append("Key Number: ").Append(KeyNumber).Append("\n")
                .Append("Output Number: ").Append(OutputNumber).Append("\n")
                .Append("Output Text: ").Append(OutputText).Append("\n")
                .ToString();
        }
    }
}
