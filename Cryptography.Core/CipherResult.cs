using System.Text;
using Cryptography.Core.Enums;

namespace Cryptography.Core
{
    public class CipherResult
    {
        public TextOutcome ValidInput { get; set; }
        
        public TextOutcome ValidKey { get; set; }
        
        public string InputText { get; set; }
        
        public string Key { get; set; }

        public string OutputText { get; set; }
        
        public Mode CipherMode { get; set; }

        public CipherResult(string inputText, string key, Mode cipherMode)
        {
            InputText = inputText;
            Key = key;
            CipherMode = cipherMode;
            ValidInput = TextOutcome.Unknown;
            ValidKey = TextOutcome.Unknown;
        }

        public bool HasValidInputAndKey()
        {
            return ValidInput.Equals(TextOutcome.Valid) && ValidKey.Equals(TextOutcome.Valid);
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("Input Text: ").Append(InputText).Append("\n")
                .Append("Key: ").Append(Key).Append("\n")
                .Append("Input State: ").Append(ValidInput).Append("\n")
                .Append("Key State: ").Append(ValidKey).Append("\n")
                .Append("Cipher Mode: ").Append(CipherMode).Append("\n")
                .Append("Output Text: ").Append(OutputText).Append("\n")
                .ToString();
        }
    }
}