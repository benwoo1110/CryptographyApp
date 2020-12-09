using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using Cryptography.Core;
using Cryptography.Core.Ciphers;
using Cryptography.Core.Enums;

namespace Cryptography.WinFormsApp
{
    partial class CryptoApp
    {
        private MainMenu menu;

        private CipherFactory cipherFactory;
        
        private readonly Color errorRed = System.Drawing.ColorTranslator.FromHtml("#ffcccb");

        private void InitCipherFactory()
        {
            menu = new MainMenu(this);
            
            cipherFactory = new CipherFactory();
            
            cipherFactory.RegisterCipher(new Blowfish());
            cipherFactory.RegisterCipher(new IDEA());
            cipherFactory.RegisterCipher(new RC5());
            cipherFactory.RegisterCipher(new Twofish());
        }
        
        private void SwitchModeButton()
        {
            cipherFactory.CipherMode = cipherFactory.CipherMode == Mode.Encrypt ? Mode.Decrypt : Mode.Encrypt; 
            ApplyModeButton();
        }

        private void ApplyModeButton()
        {
            if (cipherFactory.CipherMode == Mode.Encrypt)
            {
                CipherModeBtn.BackColor = Color.MediumSeaGreen;
                CipherModeBtn.Text = "Mode: Encrypt";
                InputLabel.Text = "Plain Text:";
                OutputLabel.Text = "Cipher Text:";
                return;
            }
            CipherModeBtn.BackColor = Color.IndianRed;
            CipherModeBtn.Text = "Mode: Decrypt";
            InputLabel.Text = "Cipher Text:";
            OutputLabel.Text = "Plain Text:";
        }

        private void UpdateBits(RichTextBox textBox, Label bitsLabel)
        {
            if (textBox.Text.Length == 0)
            {
                textBox.BackColor = SystemColors.Window;
                bitsLabel.Text = "-";
                return;
            }
            
            BigInteger? parsedNumber = Utilities.ConvertToBigInt(textBox.Text, cipherFactory.TextType);
            if (parsedNumber == null)
            {
                textBox.BackColor = errorRed;
                bitsLabel.Text = "-";
                return;
            }

            textBox.BackColor = SystemColors.Window;
            bitsLabel.Text = Utilities.NumberOfBits((BigInteger) parsedNumber).ToString();
        }

        private void ShowErrorMessage(string title, string description)
        {
            MessageBox.Show(description, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
        void OpenMenuForm()
        {
            menu.Show();
        }
    }
}