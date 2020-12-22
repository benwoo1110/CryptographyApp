namespace Cryptography.WinFormsApp
{
    partial class CipherTool
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
            this.CipherAlgorithmBox = new System.Windows.Forms.ComboBox();
            this.CipherLabel = new System.Windows.Forms.Label();
            this.InputText = new System.Windows.Forms.RichTextBox();
            this.KeyText = new System.Windows.Forms.RichTextBox();
            this.OutputText = new System.Windows.Forms.RichTextBox();
            this.TextTypeBox = new System.Windows.Forms.ComboBox();
            this.CalculateBtn = new System.Windows.Forms.Button();
            this.CipherModeBtn = new System.Windows.Forms.Button();
            this.TextTypeLabel = new System.Windows.Forms.Label();
            this.InputLabel = new System.Windows.Forms.Label();
            this.KeyLabel = new System.Windows.Forms.Label();
            this.OutputLabel = new System.Windows.Forms.Label();
            this.KeyBits = new System.Windows.Forms.Label();
            this.InputBits = new System.Windows.Forms.Label();
            this.OutputBits = new System.Windows.Forms.Label();
            this.BackBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            this.CipherAlgorithmBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CipherAlgorithmBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.CipherAlgorithmBox.FormattingEnabled = true;
            this.CipherAlgorithmBox.Location = new System.Drawing.Point(272, 28);
            this.CipherAlgorithmBox.Name = "CipherAlgorithmBox";
            this.CipherAlgorithmBox.Size = new System.Drawing.Size(388, 28);
            this.CipherAlgorithmBox.TabIndex = 0;
            this.CipherAlgorithmBox.SelectedIndexChanged += new System.EventHandler(this.CipherAlgorithmBox_SelectedIndexChanged);
            this.CipherLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.CipherLabel.Location = new System.Drawing.Point(77, 27);
            this.CipherLabel.Name = "CipherLabel";
            this.CipherLabel.Size = new System.Drawing.Size(170, 28);
            this.CipherLabel.TabIndex = 1;
            this.CipherLabel.Text = "Cipher Algorithm:";
            this.CipherLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.InputText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InputText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.InputText.Location = new System.Drawing.Point(272, 102);
            this.InputText.Name = "InputText";
            this.InputText.Size = new System.Drawing.Size(388, 60);
            this.InputText.TabIndex = 3;
            this.InputText.Text = "";
            this.InputText.TextChanged += new System.EventHandler(this.InputText_TextChanged);
            this.KeyText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KeyText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.KeyText.Location = new System.Drawing.Point(272, 176);
            this.KeyText.Name = "KeyText";
            this.KeyText.Size = new System.Drawing.Size(388, 60);
            this.KeyText.TabIndex = 4;
            this.KeyText.Text = "";
            this.KeyText.TextChanged += new System.EventHandler(this.KeyText_TextChanged);
            this.OutputText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OutputText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.OutputText.Location = new System.Drawing.Point(272, 317);
            this.OutputText.Name = "OutputText";
            this.OutputText.Size = new System.Drawing.Size(388, 106);
            this.OutputText.TabIndex = 5;
            this.OutputText.Text = "";
            this.OutputText.TextChanged += new System.EventHandler(this.OutputText_TextChanged);
            this.TextTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TextTypeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.TextTypeBox.FormattingEnabled = true;
            this.TextTypeBox.Location = new System.Drawing.Point(272, 64);
            this.TextTypeBox.Name = "TextTypeBox";
            this.TextTypeBox.Size = new System.Drawing.Size(388, 28);
            this.TextTypeBox.TabIndex = 6;
            this.TextTypeBox.SelectedIndexChanged += new System.EventHandler(this.TextTypeBox_SelectedIndexChanged);
            this.CalculateBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.CalculateBtn.Location = new System.Drawing.Point(540, 251);
            this.CalculateBtn.Name = "CalculateBtn";
            this.CalculateBtn.Size = new System.Drawing.Size(120, 33);
            this.CalculateBtn.TabIndex = 7;
            this.CalculateBtn.Text = "Calculate";
            this.CalculateBtn.UseVisualStyleBackColor = true;
            this.CalculateBtn.Click += new System.EventHandler(this.CalculateBtn_Click);
            this.CipherModeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.CipherModeBtn.Location = new System.Drawing.Point(272, 251);
            this.CipherModeBtn.Name = "CipherModeBtn";
            this.CipherModeBtn.Size = new System.Drawing.Size(242, 33);
            this.CipherModeBtn.TabIndex = 8;
            this.CipherModeBtn.Text = "Mode: Encrypt";
            this.CipherModeBtn.UseVisualStyleBackColor = true;
            this.CipherModeBtn.Click += new System.EventHandler(this.CipherMode_Click);
            this.TextTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.TextTypeLabel.Location = new System.Drawing.Point(77, 63);
            this.TextTypeLabel.Name = "TextTypeLabel";
            this.TextTypeLabel.Size = new System.Drawing.Size(170, 28);
            this.TextTypeLabel.TabIndex = 9;
            this.TextTypeLabel.Text = "Text Type:";
            this.TextTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.InputLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.InputLabel.Location = new System.Drawing.Point(77, 102);
            this.InputLabel.Name = "InputLabel";
            this.InputLabel.Size = new System.Drawing.Size(170, 60);
            this.InputLabel.TabIndex = 10;
            this.InputLabel.Text = "Plain Text:";
            this.InputLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.KeyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.KeyLabel.Location = new System.Drawing.Point(77, 176);
            this.KeyLabel.Name = "KeyLabel";
            this.KeyLabel.Size = new System.Drawing.Size(170, 60);
            this.KeyLabel.TabIndex = 11;
            this.KeyLabel.Text = "Key:";
            this.KeyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.OutputLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.OutputLabel.Location = new System.Drawing.Point(77, 317);
            this.OutputLabel.Name = "OutputLabel";
            this.OutputLabel.Size = new System.Drawing.Size(170, 106);
            this.OutputLabel.TabIndex = 12;
            this.OutputLabel.Text = "Cipher Text:";
            this.OutputLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.KeyBits.BackColor = System.Drawing.SystemColors.Control;
            this.KeyBits.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.KeyBits.Location = new System.Drawing.Point(614, 217);
            this.KeyBits.Name = "KeyBits";
            this.KeyBits.Size = new System.Drawing.Size(45, 17);
            this.KeyBits.TabIndex = 13;
            this.KeyBits.Text = "-";
            this.KeyBits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.InputBits.BackColor = System.Drawing.SystemColors.Control;
            this.InputBits.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.InputBits.Location = new System.Drawing.Point(614, 143);
            this.InputBits.Name = "InputBits";
            this.InputBits.Size = new System.Drawing.Size(45, 17);
            this.InputBits.TabIndex = 14;
            this.InputBits.Text = "-";
            this.InputBits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OutputBits.BackColor = System.Drawing.SystemColors.Control;
            this.OutputBits.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.OutputBits.Location = new System.Drawing.Point(614, 404);
            this.OutputBits.Name = "OutputBits";
            this.OutputBits.Size = new System.Drawing.Size(45, 17);
            this.OutputBits.TabIndex = 15;
            this.OutputBits.Text = "-";
            this.OutputBits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BackBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.BackBtn.Location = new System.Drawing.Point(12, 28);
            this.BackBtn.Name = "BackBtn";
            this.BackBtn.Size = new System.Drawing.Size(69, 33);
            this.BackBtn.TabIndex = 16;
            this.BackBtn.Text = "< Back";
            this.BackBtn.UseVisualStyleBackColor = true;
            this.BackBtn.Click += new System.EventHandler(this.BackBtn_Click);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BackBtn);
            this.Controls.Add(this.OutputBits);
            this.Controls.Add(this.InputBits);
            this.Controls.Add(this.KeyBits);
            this.Controls.Add(this.OutputLabel);
            this.Controls.Add(this.KeyLabel);
            this.Controls.Add(this.InputLabel);
            this.Controls.Add(this.TextTypeLabel);
            this.Controls.Add(this.CipherModeBtn);
            this.Controls.Add(this.CalculateBtn);
            this.Controls.Add(this.TextTypeBox);
            this.Controls.Add(this.OutputText);
            this.Controls.Add(this.KeyText);
            this.Controls.Add(this.InputText);
            this.Controls.Add(this.CipherLabel);
            this.Controls.Add(this.CipherAlgorithmBox);
            this.Name = "CipherTool";
            this.Text = "CryptoApp";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button BackBtn;

        private System.Windows.Forms.Label CipherLabel;
        private System.Windows.Forms.Label OutputBits;

        private System.Windows.Forms.Label InputBits;
        private System.Windows.Forms.Label KeyBits;

        private System.Windows.Forms.ComboBox CipherAlgorithmBox;

        private System.Windows.Forms.Button CalculateBtn;
        private System.Windows.Forms.Button CipherModeBtn;

        private System.Windows.Forms.Label OutputLabel;
        private System.Windows.Forms.Label TextTypeLabel;

        private System.Windows.Forms.Label InputLabel;
        private System.Windows.Forms.Label KeyLabel;
        private System.Windows.Forms.ComboBox TextTypeBox;

        private System.Windows.Forms.RichTextBox InputText;
        private System.Windows.Forms.RichTextBox KeyText;
        private System.Windows.Forms.RichTextBox OutputText;

        #endregion
    }
}