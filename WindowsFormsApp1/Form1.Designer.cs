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
            this.documentsNumber_txt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.url2_text = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.crawledURLs_txt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // url_text
            // 
            this.url_text.Location = new System.Drawing.Point(91, 24);
            this.url_text.Multiline = true;
            this.url_text.Name = "url_text";
            this.url_text.Size = new System.Drawing.Size(423, 33);
            this.url_text.TabIndex = 0;
            this.url_text.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label1.Location = new System.Drawing.Point(11, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Start Url";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // crawel_button
            // 
            this.crawel_button.Location = new System.Drawing.Point(525, 24);
            this.crawel_button.Name = "crawel_button";
            this.crawel_button.Size = new System.Drawing.Size(75, 33);
            this.crawel_button.TabIndex = 2;
            this.crawel_button.Text = "crawel!";
            this.crawel_button.UseVisualStyleBackColor = true;
            this.crawel_button.Click += new System.EventHandler(this.crawel_button_Click);
            // 
            // pause_button
            // 
            this.pause_button.Location = new System.Drawing.Point(613, 24);
            this.pause_button.Name = "pause_button";
            this.pause_button.Size = new System.Drawing.Size(87, 33);
            this.pause_button.TabIndex = 3;
            this.pause_button.Text = "pause";
            this.pause_button.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(11, 114);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(170, 20);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Prefer different domains";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // documentsNumber_txt
            // 
            this.documentsNumber_txt.Location = new System.Drawing.Point(652, 75);
            this.documentsNumber_txt.Name = "documentsNumber_txt";
            this.documentsNumber_txt.Size = new System.Drawing.Size(36, 22);
            this.documentsNumber_txt.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label2.Location = new System.Drawing.Point(522, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 21);
            this.label2.TabIndex = 6;
            this.label2.Text = "Page found";
            // 
            // url2_text
            // 
            this.url2_text.Location = new System.Drawing.Point(91, 61);
            this.url2_text.Multiline = true;
            this.url2_text.Name = "url2_text";
            this.url2_text.Size = new System.Drawing.Size(423, 33);
            this.url2_text.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label3.Location = new System.Drawing.Point(1, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 21);
            this.label3.TabIndex = 8;
            this.label3.Text = "Start Url2";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // crawledURLs_txt
            // 
            this.crawledURLs_txt.Location = new System.Drawing.Point(14, 149);
            this.crawledURLs_txt.Multiline = true;
            this.crawledURLs_txt.Name = "crawledURLs_txt";
            this.crawledURLs_txt.Size = new System.Drawing.Size(687, 220);
            this.crawledURLs_txt.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 379);
            this.Controls.Add(this.crawledURLs_txt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.url2_text);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.documentsNumber_txt);
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
        private System.Windows.Forms.TextBox documentsNumber_txt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox url2_text;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox crawledURLs_txt;
    }
}

