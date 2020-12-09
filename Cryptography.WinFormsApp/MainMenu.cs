using System;
using System.Windows.Forms;

namespace Cryptography.WinFormsApp
{
    public partial class MainMenu : NavForm
    {
        public MainMenu(NavForm parentForm) : base(parentForm)
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Back();
        }
    }
}