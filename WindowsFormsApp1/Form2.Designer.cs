
namespace WindowsFormsApp1
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.search_txtbox = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.srch_btn = new System.Windows.Forms.Button();
            this.results_list = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // search_txtbox
            // 
            this.search_txtbox.Location = new System.Drawing.Point(167, 181);
            this.search_txtbox.Name = "search_txtbox";
            this.search_txtbox.Size = new System.Drawing.Size(393, 22);
            this.search_txtbox.TabIndex = 1;
            this.search_txtbox.UseWaitCursor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(259, 218);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(40, 35);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlText;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Image = global::WindowsFormsApp1.Properties.Resources.se;
            this.pictureBox1.Location = new System.Drawing.Point(167, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(393, 143);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // srch_btn
            // 
            this.srch_btn.BackColor = System.Drawing.SystemColors.Window;
            this.srch_btn.Location = new System.Drawing.Point(305, 209);
            this.srch_btn.Name = "srch_btn";
            this.srch_btn.Size = new System.Drawing.Size(123, 56);
            this.srch_btn.TabIndex = 2;
            this.srch_btn.Text = "Search";
            this.srch_btn.UseVisualStyleBackColor = false;
            this.srch_btn.Click += new System.EventHandler(this.srch_btn_Click);
            // 
            // results_list
            // 
            this.results_list.HideSelection = false;
            this.results_list.Location = new System.Drawing.Point(167, 296);
            this.results_list.Name = "results_list";
            this.results_list.Size = new System.Drawing.Size(393, 135);
            this.results_list.TabIndex = 4;
            this.results_list.UseCompatibleStateImageBehavior = false;
            this.results_list.View = System.Windows.Forms.View.List;
            this.results_list.Visible = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 514);
            this.Controls.Add(this.results_list);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.srch_btn);
            this.Controls.Add(this.search_txtbox);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox search_txtbox;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button srch_btn;
        private System.Windows.Forms.ListView results_list;
    }
}