namespace MassImageRescaler
{
    partial class Form1
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            button1 = new Button();
            widthTextBox2 = new TextBox();
            heightTextBox3 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(88, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(615, 23);
            textBox1.TabIndex = 0;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // button1
            // 
            button1.Location = new Point(7, 41);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "Resize Files";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // widthTextBox2
            // 
            widthTextBox2.Location = new Point(133, 41);
            widthTextBox2.Name = "widthTextBox2";
            widthTextBox2.Size = new Size(100, 23);
            widthTextBox2.TabIndex = 2;
            widthTextBox2.TextChanged += widthTextBox2_TextChanged;
            widthTextBox2.KeyPress += widthTextBox2_KeyPress;
            // 
            // heightTextBox3
            // 
            heightTextBox3.Location = new Point(288, 41);
            heightTextBox3.Name = "heightTextBox3";
            heightTextBox3.Size = new Size(100, 23);
            heightTextBox3.TabIndex = 3;
            heightTextBox3.TextChanged += heightTextBox3_TextChanged;
            heightTextBox3.KeyPress += heightTextBox3_KeyPress;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(70, 15);
            label1.TabIndex = 4;
            label1.Text = "Folder Path:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(88, 45);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 5;
            label2.Text = "Width";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(239, 44);
            label3.Name = "label3";
            label3.Size = new Size(43, 15);
            label3.TabIndex = 6;
            label3.Text = "Height";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(722, 102);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(heightTextBox3);
            Controls.Add(widthTextBox2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Name = "Form1";
            Text = "Mass File Resizer";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button button1;
        private TextBox widthTextBox2;
        private TextBox heightTextBox3;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}
