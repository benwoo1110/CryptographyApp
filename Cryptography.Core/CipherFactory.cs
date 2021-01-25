using System;
using System.Collections.Generic;
using System.Linq;
using Cryptography.Core.Ciphers;
using Cryptography.Core.Enums;

namespace Cryptography.Core
{
    public class CipherFactory
    {
        public const InputType DefaultTextType = InputType.Hex;
        public const Mode DefaultCipherMode = Mode.Encrypt;

        private Dictionary<string, Cipher> Ciphers { get; }
        private Cipher SelectedCipher { get; set; }
        public InputType TextType { get; private set; }
        public Mode CipherMode { get; private set; }
        
        public CipherFactory()
        {
            Ciphers = new Dictionary<string, Cipher>();
            Reset();
        }

        public void RegisterCipher(Cipher cipher)
        {
            if (cipher == null)
            {
                throw new ArgumentException("Cannot register null cipher!");
            }
            
            var name = cipher.Name.ToLower();
            if (Ciphers.ContainsKey(name))
            {
                throw new ArgumentException("You cannot register ciphers with the same name!");
            }
            
            Ciphers.Add(name, cipher);
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

        public Mode SwitchCipherMode()
        {
            CipherMode = (CipherMode == Mode.Encrypt) ? Mode.Decrypt : Mode.Encrypt;
            return CipherMode;
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
            if (string.IsNullOrEmpty(cipherName))
            {
                return false;
            }
            
            SelectedCipher = Ciphers.GetValueOrDefault(cipherName.ToLower());
            return SelectedCipher != null;
        }

        public CipherResult RunCipher(string input, string key)
        {
            if (SelectedCipher == null)
            {
                throw new InvalidOperationException("No cipher selected.");
            }
            
            return new CipherRunInstance(SelectedCipher, CipherMode, TextType).Run(input, key);
        }

        public string GetCurrentSelected()
        {
            return HasAnySelected() ? SelectedCipher.Name : null;
        }
        
        public bool HasAnySelected()
        {
            return SelectedCipher != null;
        }

        public List<string> GetAvailableCiphers()
        {
            return Ciphers.Values.ToList().ConvertAll(c => c.Name);
        }

        public bool HasSuchCipher(string cipherName)
        {
            if (string.IsNullOrEmpty(cipherName))
            {
                return false;
            }
            
            return Ciphers.ContainsKey(cipherName.ToLower());
        }

        public void Reset()
        {
            SelectedCipher = null;
            TextType = DefaultTextType;
            CipherMode = DefaultCipherMode;
        }
    }
}
