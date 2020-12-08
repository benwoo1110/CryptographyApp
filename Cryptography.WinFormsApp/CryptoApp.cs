using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cryptography.Core;
using Cryptography.Core.Ciphers;

namespace Cryptography.WinFormsApp
{
    public partial class CryptoApp : Form
    {
        private CipherFactory cipherFactory;
        
        public CryptoApp()
        {
            InitializeComponent();
            
            cipherFactory = new CipherFactory();
            
            cipherFactory.RegisterCipher(new Blowfish());
            cipherFactory.RegisterCipher(new IDEA());
            cipherFactory.RegisterCipher(new RC5());
            cipherFactory.RegisterCipher(new Twofish());
        }

        private void CryptoApp_Load(object sender, EventArgs e)
        {
            foreach (var availableCipher in cipherFactory.GetAvailableCiphers())
            {
                CipherAlgorithm.Items.Add(availableCipher);
            }
        }
    }
}