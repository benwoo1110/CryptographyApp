using System;
using System.Windows.Forms;

namespace Cryptography.WinFormsApp
{
    public abstract class NavForm : Form
    {
        private NavForm parentForm;

        public NavForm()
        {
            Closed += NavForm_FormClosed;
        }

        public NavForm(NavForm parentForm)
        {
            this.parentForm = parentForm;
            Closed += NavForm_FormClosed;
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