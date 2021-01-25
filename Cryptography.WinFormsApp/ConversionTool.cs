using System;
using System.Numerics;
using Cryptography.Core;
using Cryptography.Core.Enums;

namespace Cryptography.WinFormsApp
{
    public partial class ConversionTool : CryptoForm
    {
        private InputType fromType;
        private InputType toType;
        
        public ConversionTool(CryptoForm parentForm) : base(parentForm)
        {
            InitializeComponent();
            
            InitComboBoxWithInputType(fromTypeBox);
            InitComboBoxWithInputType(toTypeBox);
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            Back();
        }

        private void fromTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Enum.TryParse(fromTypeBox.Text, out InputType parsed))
            {
                // TODO: show error message box
            }

            fromType = parsed;
        }

        private void toTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Enum.TryParse(toTypeBox.Text, out InputType parsed))
            {
                // TODO: show error message box
            }

            toType = parsed;
        }

        private void InputText_TextChanged(object sender, EventArgs e)
        {

        }

        private void OutputText_TextChanged(object sender, EventArgs e)
        {

        }

        private void ConvertBtn_Click(object sender, EventArgs e)
        {
            var parsedNumber = Utilities.ConvertToBigInt(InputText.Text, fromType);
            if (parsedNumber == null)
            {
                ShowErrorMessage("Invalid Input", "Invalid Input");
                return;
            }

            OutputText.Text = Utilities.ConvertToString((BigInteger) parsedNumber, toType);
        }
    }
}