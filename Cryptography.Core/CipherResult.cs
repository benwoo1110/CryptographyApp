using Cryptography.Core.Enums;

namespace Cryptography.Core
{
    public class CipherResult
    {
        public string CipherName { get; set; }
        
        public InputType TextType { get; set; }
        
        public Mode CipherMode { get; set; }
        
        public TextResult Input { get; }
        
        public TextResult Key { get; }
        
        public TextResult Output { get; }

        public CipherResult()
        {
            Input = new TextResult();
            Key = new TextResult();
            Output = new TextResult();
        }

        public CipherResult(string cipherName, InputType textType, Mode cipherMode, string inputText, string keyText)
        {
            CipherName = cipherName;
            TextType = textType;
            CipherMode = cipherMode;
            
            Input = new TextResult("Input", inputText);
            Key = new TextResult("Key", keyText);
            Output = new TextResult("Output");
        }

        public bool HasParsingErrors()
        {
            //TODO: Output as well.
            return Input.HasParseError() || Key.HasParseError();
        }

        public bool HasValidInputAndKey()
        {
            return Input.IsValid() && Key.IsValid();
        }

        public bool HasOutput()
        {
            return !string.IsNullOrEmpty(Output.Text);
        }

        public override string ToString()
        {
            return "Cipher Name: " + CipherName + "\n"
                   + "Text Type: " + TextType + "\n"
                   + "Cipher Mode: " + CipherMode + "\n"
                   + "\n"
                   + Input + "\n"
                   + "\n"
                   + Key + "\n"
                   + "\n"
                   + Output;
        }
    }
}
