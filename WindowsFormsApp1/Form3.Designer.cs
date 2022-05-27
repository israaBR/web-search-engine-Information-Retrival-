namespace WindowsFormsApp1
{
    partial class Form3
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
            this.calculatebtn = new System.Windows.Forms.Button();
            this.URLtxt = new System.Windows.Forms.TextBox();
            this.calculatedTermstxt = new System.Windows.Forms.TextBox();
            this.numberOfTermstxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numberOfCalTermstxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.labl = new System.Windows.Forms.Label();
            this.fileNametxt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // calculatebtn
            // 
            this.calculatebtn.Location = new System.Drawing.Point(89, 102);
            this.calculatebtn.Name = "calculatebtn";
            this.calculatebtn.Size = new System.Drawing.Size(620, 41);
            this.calculatebtn.TabIndex = 0;
            this.calculatebtn.Text = "Calculate TF-IDF";
            this.calculatebtn.UseVisualStyleBackColor = true;
            this.calculatebtn.Click += new System.EventHandler(this.calculatebtn_Click);
            // 
            // URLtxt
            // 
            this.URLtxt.Location = new System.Drawing.Point(12, 13);
            this.URLtxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.URLtxt.Multiline = true;
            this.URLtxt.Name = "URLtxt";
            this.URLtxt.Size = new System.Drawing.Size(776, 41);
            this.URLtxt.TabIndex = 17;
            // 
            // calculatedTermstxt
            // 
            this.calculatedTermstxt.Location = new System.Drawing.Point(12, 236);
            this.calculatedTermstxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.calculatedTermstxt.Multiline = true;
            this.calculatedTermstxt.Name = "calculatedTermstxt";
            this.calculatedTermstxt.Size = new System.Drawing.Size(776, 201);
            this.calculatedTermstxt.TabIndex = 18;
            // 
            // numberOfTermstxt
            // 
            this.numberOfTermstxt.Location = new System.Drawing.Point(574, 163);
            this.numberOfTermstxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numberOfTermstxt.Multiline = true;
            this.numberOfTermstxt.Name = "numberOfTermstxt";
            this.numberOfTermstxt.Size = new System.Drawing.Size(135, 23);
            this.numberOfTermstxt.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(409, 166);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 16);
            this.label1.TabIndex = 20;
            this.label1.Text = "Total Number of Terms:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(86, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 16);
            this.label2.TabIndex = 21;
            this.label2.Text = "Number of Calculated terms:";
            // 
            // numberOfCalTermstxt
            // 
            this.numberOfCalTermstxt.Location = new System.Drawing.Point(269, 159);
            this.numberOfCalTermstxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numberOfCalTermstxt.Multiline = true;
            this.numberOfCalTermstxt.Name = "numberOfCalTermstxt";
            this.numberOfCalTermstxt.Size = new System.Drawing.Size(135, 23);
            this.numberOfCalTermstxt.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 16);
            this.label3.TabIndex = 23;
            this.label3.Text = "Calculated Terms";
            // 
            // labl
            // 
            this.labl.AutoSize = true;
            this.labl.Location = new System.Drawing.Point(188, 65);
            this.labl.Name = "labl";
            this.labl.Size = new System.Drawing.Size(75, 16);
            this.labl.TabIndex = 24;
            this.labl.Text = "File Name: ";
            // 
            // fileNametxt
            // 
            this.fileNametxt.Location = new System.Drawing.Point(269, 62);
            this.fileNametxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fileNametxt.Multiline = true;
            this.fileNametxt.Name = "fileNametxt";
            this.fileNametxt.Size = new System.Drawing.Size(288, 23);
            this.fileNametxt.TabIndex = 25;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.fileNametxt);
            this.Controls.Add(this.labl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numberOfCalTermstxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numberOfTermstxt);
            this.Controls.Add(this.calculatedTermstxt);
            this.Controls.Add(this.URLtxt);
            this.Controls.Add(this.calculatebtn);
            this.Name = "Form3";
            this.Text = "Form3";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button calculatebtn;
        private System.Windows.Forms.TextBox URLtxt;
        private System.Windows.Forms.TextBox calculatedTermstxt;
        private System.Windows.Forms.TextBox numberOfTermstxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox numberOfCalTermstxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labl;
        private System.Windows.Forms.TextBox fileNametxt;
    }
}