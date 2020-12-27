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
        private CipherFactory cipherFactory;
        
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
                // TODO: show error message box
            }
        }
        
        private void TextTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!Enum.TryParse(TextTypeBox.Text, out InputType parsed))
            {
                // TODO: show error message box
            }

            cipherFactory.TextType = parsed;
            
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
            
            CipherResult result = cipherFactory.RunCipher(InputText.Text, KeyText.Text);

            if (result.HasParsingErrors())
            {
                ShowErrorMessage("Parse Error", "There is an error in the format of your text and/or key!");
                return;
            }

            if (!result.HasValidInputAndKey())
            {
                ShowErrorMessage("Invalid Input", "text and/or key does not conform to the requirements of the cipher!");
                return;
            }

            if (!result.HasOutput())
            {
                ShowErrorMessage("Cipher Error", "Hmmmm... there was an error generating the output for your cipher!");
                return;
            }

            OutputText.Text = result.Output.Text;
        }

        private void UpdateBits(RichTextBox textBox, Label bitsLabel)
        {
            UpdateBits(textBox, bitsLabel, cipherFactory.TextType);
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
    }
}