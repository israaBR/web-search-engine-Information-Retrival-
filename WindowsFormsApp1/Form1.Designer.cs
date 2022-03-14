namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.url_text = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.crawel_button = new System.Windows.Forms.Button();
            this.pause_button = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.url2_text = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // url_text
            // 
            this.url_text.Location = new System.Drawing.Point(102, 28);
            this.url_text.Multiline = true;
            this.url_text.Name = "url_text";
            this.url_text.Size = new System.Drawing.Size(475, 39);
            this.url_text.TabIndex = 0;
            this.url_text.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Start Url";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // crawel_button
            // 
            this.crawel_button.Location = new System.Drawing.Point(591, 28);
            this.crawel_button.Name = "crawel_button";
            this.crawel_button.Size = new System.Drawing.Size(84, 39);
            this.crawel_button.TabIndex = 2;
            this.crawel_button.Text = "crawel!";
            this.crawel_button.UseVisualStyleBackColor = true;
            this.crawel_button.Click += new System.EventHandler(this.crawel_button_Click);
            // 
            // pause_button
            // 
            this.pause_button.Location = new System.Drawing.Point(690, 28);
            this.pause_button.Name = "pause_button";
            this.pause_button.Size = new System.Drawing.Size(98, 39);
            this.pause_button.TabIndex = 3;
            this.pause_button.Text = "pause";
            this.pause_button.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 135);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(206, 23);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Prefer different domains";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(734, 89);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(40, 27);
            this.textBox2.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label2.Location = new System.Drawing.Point(587, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 24);
            this.label2.TabIndex = 6;
            this.label2.Text = "Page found";
            // 
            // url2_text
            // 
            this.url2_text.Location = new System.Drawing.Point(102, 73);
            this.url2_text.Multiline = true;
            this.url2_text.Name = "url2_text";
            this.url2_text.Size = new System.Drawing.Size(475, 39);
            this.url2_text.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label3.Location = new System.Drawing.Point(1, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 24);
            this.label3.TabIndex = 8;
            this.label3.Text = "Start Url2";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(16, 177);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(772, 261);
            this.textBox3.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.url2_text);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.pause_button);
            this.Controls.Add(this.crawel_button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.url_text);
            this.Name = "Form1";
            this.Text = "te";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox url_text;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button crawel_button;
        private System.Windows.Forms.Button pause_button;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox url2_text;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
    }
}

