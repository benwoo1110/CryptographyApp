namespace Cryptography.WinFormsApp
{
    partial class CryptoApp
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
            this.CipherAlgorithm = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.InputText = new System.Windows.Forms.RichTextBox();
            this.KeyText = new System.Windows.Forms.RichTextBox();
            this.OutputText = new System.Windows.Forms.RichTextBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.Calculate = new System.Windows.Forms.Button();
            this.CipherMode = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CipherAlgorithm
            // 
            this.CipherAlgorithm.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.CipherAlgorithm.FormattingEnabled = true;
            this.CipherAlgorithm.Location = new System.Drawing.Point(348, 31);
            this.CipherAlgorithm.Margin = new System.Windows.Forms.Padding(4);
            this.CipherAlgorithm.Name = "CipherAlgorithm";
            this.CipherAlgorithm.Size = new System.Drawing.Size(516, 28);
            this.CipherAlgorithm.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.label1.Location = new System.Drawing.Point(88, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(227, 28);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cipher Algorithm:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // InputText
            // 
            this.InputText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.InputText.Location = new System.Drawing.Point(348, 122);
            this.InputText.Margin = new System.Windows.Forms.Padding(4);
            this.InputText.Name = "InputText";
            this.InputText.Size = new System.Drawing.Size(516, 73);
            this.InputText.TabIndex = 3;
            this.InputText.Text = "";
            // 
            // KeyText
            // 
            this.KeyText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.KeyText.Location = new System.Drawing.Point(348, 213);
            this.KeyText.Margin = new System.Windows.Forms.Padding(4);
            this.KeyText.Name = "KeyText";
            this.KeyText.Size = new System.Drawing.Size(516, 73);
            this.KeyText.TabIndex = 4;
            this.KeyText.Text = "";
            // 
            // OutputText
            // 
            this.OutputText.Location = new System.Drawing.Point(348, 386);
            this.OutputText.Margin = new System.Windows.Forms.Padding(4);
            this.OutputText.Name = "OutputText";
            this.OutputText.Size = new System.Drawing.Size(516, 130);
            this.OutputText.TabIndex = 5;
            this.OutputText.Text = "";
            // 
            // comboBox2
            // 
            this.comboBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(348, 75);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(516, 28);
            this.comboBox2.TabIndex = 6;
            // 
            // Calculate
            // 
            this.Calculate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.Calculate.Location = new System.Drawing.Point(705, 305);
            this.Calculate.Margin = new System.Windows.Forms.Padding(4);
            this.Calculate.Name = "Calculate";
            this.Calculate.Size = new System.Drawing.Size(160, 41);
            this.Calculate.TabIndex = 7;
            this.Calculate.Text = "Cacluate";
            this.Calculate.UseVisualStyleBackColor = true;
            // 
            // CipherMode
            // 
            this.CipherMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.CipherMode.Location = new System.Drawing.Point(348, 305);
            this.CipherMode.Margin = new System.Windows.Forms.Padding(4);
            this.CipherMode.Name = "CipherMode";
            this.CipherMode.Size = new System.Drawing.Size(323, 41);
            this.CipherMode.TabIndex = 8;
            this.CipherMode.Text = "Mode: Encrypt";
            this.CipherMode.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.label2.Location = new System.Drawing.Point(88, 75);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(227, 28);
            this.label2.TabIndex = 9;
            this.label2.Text = "Text Type:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.label3.Location = new System.Drawing.Point(88, 122);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(227, 74);
            this.label3.TabIndex = 10;
            this.label3.Text = "Plain Text:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.label4.Location = new System.Drawing.Point(88, 213);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(227, 74);
            this.label4.TabIndex = 11;
            this.label4.Text = "Key:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.label5.Location = new System.Drawing.Point(88, 386);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(227, 130);
            this.label5.TabIndex = 12;
            this.label5.Text = "Cipher Text:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CryptoApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CipherMode);
            this.Controls.Add(this.Calculate);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.OutputText);
            this.Controls.Add(this.KeyText);
            this.Controls.Add(this.InputText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CipherAlgorithm);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CryptoApp";
            this.Text = "CryptoApp";
            this.Load += new System.EventHandler(this.CryptoApp_Load);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button Calculate;
        private System.Windows.Forms.ComboBox CipherAlgorithm;
        private System.Windows.Forms.Button CipherMode;
        private System.Windows.Forms.RichTextBox InputText;
        private System.Windows.Forms.RichTextBox KeyText;
        private System.Windows.Forms.RichTextBox OutputText;

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;

        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.ComboBox comboBox2;

        private System.Windows.Forms.Label label1;

        #endregion
    }
}