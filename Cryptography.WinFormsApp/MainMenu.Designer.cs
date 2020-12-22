using System.ComponentModel;

namespace Cryptography.WinFormsApp
{
    partial class MainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ConversionPageBtn = new System.Windows.Forms.Button();
            this.CipherPageBtn = new System.Windows.Forms.Button();
            this.Title = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ConversionPageBtn
            // 
            this.ConversionPageBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ConversionPageBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ConversionPageBtn.Location = new System.Drawing.Point(0, 556);
            this.ConversionPageBtn.Margin = new System.Windows.Forms.Padding(10);
            this.ConversionPageBtn.Name = "ConversionPageBtn";
            this.ConversionPageBtn.Size = new System.Drawing.Size(1067, 102);
            this.ConversionPageBtn.TabIndex = 0;
            this.ConversionPageBtn.Text = "Convert da hex to binary!";
            this.ConversionPageBtn.UseVisualStyleBackColor = true;
            this.ConversionPageBtn.Click += new System.EventHandler(this.ConversionPageBtn_Click);
            // 
            // CipherPageBtn
            // 
            this.CipherPageBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.CipherPageBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CipherPageBtn.Location = new System.Drawing.Point(0, 454);
            this.CipherPageBtn.Margin = new System.Windows.Forms.Padding(10);
            this.CipherPageBtn.Name = "CipherPageBtn";
            this.CipherPageBtn.Size = new System.Drawing.Size(1067, 102);
            this.CipherPageBtn.TabIndex = 1;
            this.CipherPageBtn.Text = "Lets cipher some text!";
            this.CipherPageBtn.UseVisualStyleBackColor = true;
            this.CipherPageBtn.Click += new System.EventHandler(this.CipherPageBtn_Click);
            // 
            // Title
            // 
            this.Title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Title.Location = new System.Drawing.Point(0, 0);
            this.Title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(1067, 454);
            this.Title.TabIndex = 2;
            this.Title.Text = "Cryptography App";
            this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 658);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.CipherPageBtn);
            this.Controls.Add(this.ConversionPageBtn);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainMenu";
            this.Text = "CryptoApp";
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button CipherPageBtn;
        private System.Windows.Forms.Button ConversionPageBtn;

        private System.Windows.Forms.Label Title;

        #endregion
    }
}