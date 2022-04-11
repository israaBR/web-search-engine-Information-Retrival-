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
            this.crawel_button = new System.Windows.Forms.Button();
            this.pause_button = new System.Windows.Forms.Button();
            this.documentsNumber_txt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.crawledURLs_txt = new System.Windows.Forms.TextBox();
            this.indexedPages_txt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.indPgNumbers_txt = new System.Windows.Forms.TextBox();
            this.pauseIndexing = new System.Windows.Forms.Button();
            this.indexingButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // url_text
            // 
            this.url_text.Location = new System.Drawing.Point(11, 19);
            this.url_text.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.url_text.Multiline = true;
            this.url_text.Name = "url_text";
            this.url_text.Size = new System.Drawing.Size(213, 27);
            this.url_text.TabIndex = 0;
            this.url_text.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // crawel_button
            // 
            this.crawel_button.Location = new System.Drawing.Point(11, 59);
            this.crawel_button.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.crawel_button.Name = "crawel_button";
            this.crawel_button.Size = new System.Drawing.Size(102, 27);
            this.crawel_button.TabIndex = 2;
            this.crawel_button.Text = "crawel!";
            this.crawel_button.UseVisualStyleBackColor = true;
            this.crawel_button.Click += new System.EventHandler(this.crawel_button_Click);
            // 
            // pause_button
            // 
            this.pause_button.Location = new System.Drawing.Point(117, 59);
            this.pause_button.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pause_button.Name = "pause_button";
            this.pause_button.Size = new System.Drawing.Size(106, 27);
            this.pause_button.TabIndex = 3;
            this.pause_button.Text = "pause";
            this.pause_button.UseVisualStyleBackColor = true;
            // 
            // documentsNumber_txt
            // 
            this.documentsNumber_txt.Location = new System.Drawing.Point(117, 97);
            this.documentsNumber_txt.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.documentsNumber_txt.Name = "documentsNumber_txt";
            this.documentsNumber_txt.Size = new System.Drawing.Size(99, 20);
            this.documentsNumber_txt.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label2.Location = new System.Drawing.Point(18, 96);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "craweled pages";
            // 
            // crawledURLs_txt
            // 
            this.crawledURLs_txt.Location = new System.Drawing.Point(11, 121);
            this.crawledURLs_txt.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.crawledURLs_txt.Multiline = true;
            this.crawledURLs_txt.Name = "crawledURLs_txt";
            this.crawledURLs_txt.Size = new System.Drawing.Size(213, 179);
            this.crawledURLs_txt.TabIndex = 9;
            // 
            // indexedPages_txt
            // 
            this.indexedPages_txt.Location = new System.Drawing.Point(312, 120);
            this.indexedPages_txt.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.indexedPages_txt.Multiline = true;
            this.indexedPages_txt.Name = "indexedPages_txt";
            this.indexedPages_txt.Size = new System.Drawing.Size(213, 179);
            this.indexedPages_txt.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label1.Location = new System.Drawing.Point(318, 95);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 17);
            this.label1.TabIndex = 14;
            this.label1.Text = "Indexed pages";
            // 
            // indPgNumbers_txt
            // 
            this.indPgNumbers_txt.Location = new System.Drawing.Point(419, 96);
            this.indPgNumbers_txt.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.indPgNumbers_txt.Name = "indPgNumbers_txt";
            this.indPgNumbers_txt.Size = new System.Drawing.Size(99, 20);
            this.indPgNumbers_txt.TabIndex = 13;
            // 
            // pauseIndexing
            // 
            this.pauseIndexing.Location = new System.Drawing.Point(312, 57);
            this.pauseIndexing.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pauseIndexing.Name = "pauseIndexing";
            this.pauseIndexing.Size = new System.Drawing.Size(212, 27);
            this.pauseIndexing.TabIndex = 12;
            this.pauseIndexing.Text = "pause";
            this.pauseIndexing.UseVisualStyleBackColor = true;
            this.pauseIndexing.Click += new System.EventHandler(this.pauseIndexing_Click);
            // 
            // indexingButton
            // 
            this.indexingButton.Location = new System.Drawing.Point(312, 19);
            this.indexingButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.indexingButton.Name = "indexingButton";
            this.indexingButton.Size = new System.Drawing.Size(212, 27);
            this.indexingButton.TabIndex = 11;
            this.indexingButton.Text = "start Indexing";
            this.indexingButton.UseVisualStyleBackColor = true;
            this.indexingButton.Click += new System.EventHandler(this.indexingButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 308);
            this.Controls.Add(this.indexedPages_txt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.indPgNumbers_txt);
            this.Controls.Add(this.pauseIndexing);
            this.Controls.Add(this.indexingButton);
            this.Controls.Add(this.crawledURLs_txt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.documentsNumber_txt);
            this.Controls.Add(this.pause_button);
            this.Controls.Add(this.crawel_button);
            this.Controls.Add(this.url_text);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Form1";
            this.Text = "te";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox url_text;
        private System.Windows.Forms.Button crawel_button;
        private System.Windows.Forms.Button pause_button;
        private System.Windows.Forms.TextBox documentsNumber_txt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox crawledURLs_txt;
        private System.Windows.Forms.TextBox indexedPages_txt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox indPgNumbers_txt;
        private System.Windows.Forms.Button pauseIndexing;
        private System.Windows.Forms.Button indexingButton;
    }
}

