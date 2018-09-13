namespace MotoRev
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.labelUsername = new System.Windows.Forms.Label();
            this.btnNewRo = new System.Windows.Forms.Button();
            this.btCustomer = new System.Windows.Forms.Button();
            this.btHistory = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(254, 9);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(77, 13);
            this.labelUsername.TabIndex = 0;
            this.labelUsername.Text = "labelUsername";
            this.labelUsername.DoubleClick += new System.EventHandler(this.labelUsername_DoubleClick);
            // 
            // btnNewRo
            // 
            this.btnNewRo.Location = new System.Drawing.Point(12, 4);
            this.btnNewRo.Name = "btnNewRo";
            this.btnNewRo.Size = new System.Drawing.Size(75, 23);
            this.btnNewRo.TabIndex = 1;
            this.btnNewRo.Text = "New RO";
            this.btnNewRo.UseVisualStyleBackColor = true;
            this.btnNewRo.Click += new System.EventHandler(this.btnNewRo_Click);
            // 
            // btCustomer
            // 
            this.btCustomer.Location = new System.Drawing.Point(12, 73);
            this.btCustomer.Name = "btCustomer";
            this.btCustomer.Size = new System.Drawing.Size(75, 23);
            this.btCustomer.TabIndex = 3;
            this.btCustomer.Text = "Customers";
            this.btCustomer.UseVisualStyleBackColor = true;
            this.btCustomer.Click += new System.EventHandler(this.btCustomer_Click);
            // 
            // btHistory
            // 
            this.btHistory.Location = new System.Drawing.Point(12, 38);
            this.btHistory.Name = "btHistory";
            this.btHistory.Size = new System.Drawing.Size(75, 23);
            this.btHistory.TabIndex = 2;
            this.btHistory.Text = "History";
            this.btHistory.UseVisualStyleBackColor = true;
            this.btHistory.Click += new System.EventHandler(this.btHistory_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(343, 108);
            this.Controls.Add(this.btHistory);
            this.Controls.Add(this.btCustomer);
            this.Controls.Add(this.btnNewRo);
            this.Controls.Add(this.labelUsername);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Revolution Motors";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Button btnNewRo;
        private System.Windows.Forms.Button btCustomer;
        private System.Windows.Forms.Button btHistory;
    }
}

