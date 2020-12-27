using System.Numerics;
using Cryptography.Core.Enums;

namespace Cryptography.Core
{
    public class TextResult
    {
        public string Name { get; set; }
        
        public string Text { get; set; }

        public BigInteger Number { get; set; }

        public ConvertResult State { get; set; } = ConvertResult.Unknown;

        public TextResult()
        {
        }

        public TextResult(string name)
        {
            Name = name;
        }

        public TextResult(string name, string text)
        {
            Name = name;
            Text = text;
        }

        public bool IsValid()
        {
            return State == ConvertResult.Valid;
        }
        
        public bool HasParseError()
        {
            return State == ConvertResult.ParseError;
        }

        public override string ToString()
        {
            return Name + " Text: " + Text + "\n"
                   + Name + " State: " + State + "\n"
                   + Name + " Number: " + Number;
        }
    }
}
