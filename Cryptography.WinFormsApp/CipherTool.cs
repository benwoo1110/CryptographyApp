using System;
using System.Drawing;
using System.Windows.Forms;
using Cryptography.Core;
using Cryptography.Core.Ciphers;
using Cryptography.Core.Enums;

namespace Cryptography.WinFormsApp
{
    public partial class CipherTool : CryptoForm
    {
        private readonly CipherFactory cipherFactory;
        
        public CipherTool(CryptoForm parentForm) : base(parentForm)
        {
            InitializeComponent();
            
            cipherFactory = new CipherFactory();
            
            cipherFactory.RegisterCipher(new Blowfish());
            cipherFactory.RegisterCipher(new IDEA());
            cipherFactory.RegisterCipher(new RC5());
            cipherFactory.RegisterCipher(new Twofish());
            
            InitComboBox(CipherAlgorithmBox, cipherFactory.GetAvailableCiphers().ToArray());
            InitComboBoxWithInputType(TextTypeBox);
            
            ApplyModeButton();
        }
        
        private void BackBtn_Click(object sender, EventArgs e)
        {
            Back();
        }

        private void CipherAlgorithmBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cipherFactory.SelectCipher(CipherAlgorithmBox.Text))
            {
                ShowErrorMessage("Select Cipher Error", $"There was an error setting your cipher to {CipherAlgorithmBox.Text}!");
            }
        }
        
        private void TextTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cipherFactory.SetTextType(TextTypeBox.Text))
            {
                ShowErrorMessage("Text Type Error", $"There was an error setting your text type to {TextTypeBox.Text}!");
                return;
            }
            
            UpdateBits(InputText, InputBits);
            UpdateBits(KeyText, KeyBits);
        }
        
        private void InputText_TextChanged(object sender, EventArgs e)
        {
            UpdateBits(InputText, InputBits);
        }

        private void KeyText_TextChanged(object sender, EventArgs e)
        {
            UpdateBits(KeyText, KeyBits);
        }
        
        private void OutputText_TextChanged(object sender, EventArgs e)
        {
            UpdateBits(OutputText, OutputBits);
        }

        private void CipherMode_Click(object sender, EventArgs e)
        {
            SwitchModeButton();
        }

        private void CalculateBtn_Click(object sender, EventArgs e)
        {
            
            var result = cipherFactory.RunCipher(InputText.Text, KeyText.Text);

            if (!result.Input.IsValid())
            {
                ShowErrorMessage($"Input Parse Error", $"There is an error in the format of your {InputTextName(result)}!");
                return;
            }
            if (!result.Key.IsValid())
            {
                ShowErrorMessage($"Key Parse Error", $"There is an error in the format of your Key!");
                return;
            }
            if (!result.HasOutput())
            {
                ShowErrorMessage("Cipher Error", "Hmmmm... there was an error generating the output for your cipher!");
                return;
            }

            OutputText.Text = result.Output.Text;
        }

        private string InputTextName(CipherResult result)
        {
            return (result.CipherMode == Mode.Encrypt) ? "plain text" : "cipher text";
        }

        private void UpdateBits(RichTextBox textBox, Label bitsLabel)
        {
            UpdateBits(textBox, bitsLabel, cipherFactory.TextType);
        }

        private void SwitchModeButton()
        {
            cipherFactory.SwitchCipherMode();
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
    }
}