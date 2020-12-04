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
        }

        public bool HasValidInputAndKey()
        {
            return ValidInput.Equals(TextOutcome.Valid);
        }
    }
}