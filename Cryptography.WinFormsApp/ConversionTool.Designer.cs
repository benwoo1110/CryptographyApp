using System.ComponentModel;

namespace Cryptography.WinFormsApp
{
    partial class ConversionTool
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
            this.BackBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.fromTypeBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.toTypeBox = new System.Windows.Forms.ComboBox();
            this.InputText = new System.Windows.Forms.RichTextBox();
            this.OutputText = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ConvertBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BackBtn
            // 
            this.BackBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BackBtn.Location = new System.Drawing.Point(16, 41);
            this.BackBtn.Margin = new System.Windows.Forms.Padding(4);
            this.BackBtn.Name = "BackBtn";
            this.BackBtn.Size = new System.Drawing.Size(92, 48);
            this.BackBtn.TabIndex = 17;
            this.BackBtn.Text = "< Back";
            this.BackBtn.UseVisualStyleBackColor = true;
            this.BackBtn.Click += new System.EventHandler(this.BackBtn_Click);
            // 
            // label1
            // 
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(380, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 38);
            this.label1.TabIndex = 18;
            this.label1.Text = "Convert from";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fromTypeBox
            // 
            this.fromTypeBox.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.fromTypeBox.FormattingEnabled = true;
            this.fromTypeBox.Location = new System.Drawing.Point(548, 108);
            this.fromTypeBox.Name = "fromTypeBox";
            this.fromTypeBox.Size = new System.Drawing.Size(121, 38);
            this.fromTypeBox.TabIndex = 19;
            this.fromTypeBox.SelectedIndexChanged += new System.EventHandler(this.fromTypeBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(675, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 38);
            this.label2.TabIndex = 18;
            this.label2.Text = "to";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // toTypeBox
            // 
            this.toTypeBox.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.toTypeBox.FormattingEnabled = true;
            this.toTypeBox.Location = new System.Drawing.Point(729, 107);
            this.toTypeBox.Name = "toTypeBox";
            this.toTypeBox.Size = new System.Drawing.Size(121, 38);
            this.toTypeBox.TabIndex = 19;
            this.toTypeBox.SelectedIndexChanged += new System.EventHandler(this.toTypeBox_SelectedIndexChanged);
            // 
            // InputText
            // 
            this.InputText.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.InputText.Location = new System.Drawing.Point(392, 170);
            this.InputText.Name = "InputText";
            this.InputText.Size = new System.Drawing.Size(458, 118);
            this.InputText.TabIndex = 20;
            this.InputText.Text = "";
            this.InputText.TextChanged += new System.EventHandler(this.InputText_TextChanged);
            // 
            // OutputText
            // 
            this.OutputText.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.OutputText.Location = new System.Drawing.Point(392, 415);
            this.OutputText.Name = "OutputText";
            this.OutputText.Size = new System.Drawing.Size(458, 118);
            this.OutputText.TabIndex = 20;
            this.OutputText.Text = "";
            this.OutputText.TextChanged += new System.EventHandler(this.OutputText_TextChanged);
            // 
            // label3
            // 
            this.label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(187, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(179, 118);
            this.label3.TabIndex = 18;
            this.label3.Text = "Input Text:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Cursor = System.Windows.Forms.Cursors.Default;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(187, 415);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(179, 118);
            this.label4.TabIndex = 18;
            this.label4.Text = "Output Text:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ConvertBtn
            // 
            this.ConvertBtn.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ConvertBtn.Location = new System.Drawing.Point(572, 304);
            this.ConvertBtn.Name = "ConvertBtn";
            this.ConvertBtn.Size = new System.Drawing.Size(278, 45);
            this.ConvertBtn.TabIndex = 21;
            this.ConvertBtn.Text = "Let\'s Convert!";
            this.ConvertBtn.UseVisualStyleBackColor = true;
            this.ConvertBtn.Click += new System.EventHandler(this.ConvertBtn_Click);
            // 
            // ConversionTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 658);
            this.Controls.Add(this.ConvertBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.OutputText);
            this.Controls.Add(this.InputText);
            this.Controls.Add(this.toTypeBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.fromTypeBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BackBtn);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ConversionTool";
            this.Text = "ConversionTool";
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button BackBtn;

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox fromTypeBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox toTypeBox;
        private System.Windows.Forms.RichTextBox InputText;
        private System.Windows.Forms.RichTextBox OutputText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button ConvertBtn;
    }
}