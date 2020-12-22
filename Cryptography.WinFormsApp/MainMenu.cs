using System;
using System.Windows.Forms;

namespace Cryptography.WinFormsApp
{
    public partial class MainMenu : CryptoForm
    {
        private readonly CryptoForm conversionTool;
        private readonly CryptoForm cipherTool;
        
        public MainMenu()
        {
            InitializeComponent();
            conversionTool = new ConversionTool(this);
            cipherTool = new CipherTool(this);
        }

        private void CipherPageBtn_Click(object sender, EventArgs e)
        {
            cipherTool.Show();
        }

        private void ConversionPageBtn_Click(object sender, EventArgs e)
        {
            conversionTool.Show();
        }
    }
}