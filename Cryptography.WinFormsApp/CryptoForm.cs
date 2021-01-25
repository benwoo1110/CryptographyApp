using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using Cryptography.Core;
using Cryptography.Core.Enums;

namespace Cryptography.WinFormsApp
{
    public class CryptoForm : Form
    {
        private readonly CryptoForm parentForm;

        private readonly Color errorRed = ColorTranslator.FromHtml("#ffcccb");

        public CryptoForm()
        {
            Closed += NavForm_FormClosed;
        }

        public CryptoForm(CryptoForm parentForm)
        {
            this.parentForm = parentForm;
            Closed += NavForm_FormClosed;
        }
        
        public void InitComboBoxWithInputType(ComboBox comboBox)
        {
            InitComboBox(comboBox, Enum.GetValues(typeof(InputType)));
        }

        public void InitComboBox(ComboBox comboBox, Array objects)
        {
            foreach (var o in objects)
            {
                comboBox.Items.Add(o.ToString() ?? string.Empty);
            }
            
            comboBox.SelectedItem = comboBox.Items[0];
        }

        public void UpdateBits(RichTextBox textBox, Label bitsLabel, InputType textType)
        {
            var text = Utilities.TrimText(textBox.Text);
            
            if (string.IsNullOrEmpty(text))
            {
                textBox.BackColor = SystemColors.Window;
                bitsLabel.Text = "-";
                return;
            }
            
            var parsedNumber = Utilities.ConvertToBigInt(text, textType);
            if (parsedNumber == null)
            {
                textBox.BackColor = errorRed;
                bitsLabel.Text = "-";
                return;
            }

            textBox.BackColor = SystemColors.Window;
            bitsLabel.Text = Utilities.NumberOfBits((BigInteger)parsedNumber).ToString();
        }

        public void ShowErrorMessage(string title, string description)
        {
            MessageBox.Show(description, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public new void Show()
        {
            parentForm?.Hide();
            base.Show();
        }

        public void Back()
        {
            if (parentForm == null)
            {
                throw new InvalidOperationException("No parent form to go back to!");
            }
            
            Hide();
            parentForm.Show();
        }

        private void NavForm_FormClosed(object? sender, EventArgs e)
        {
            parentForm?.Close();
        }
    }
}