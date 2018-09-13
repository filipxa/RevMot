namespace MotoRev
{
    partial class HistoryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HistoryForm));
            this.dgvRo = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btPrintSelected = new System.Windows.Forms.Button();
            this.tbRo = new System.Windows.Forms.TextBox();
            this.tbCustomer = new System.Windows.Forms.TextBox();
            this.tbMakeModel = new System.Windows.Forms.TextBox();
            this.dtpFirst = new System.Windows.Forms.DateTimePicker();
            this.dtpLast = new System.Windows.Forms.DateTimePicker();
            this.btPrintAll = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.tbTotal = new System.Windows.Forms.TextBox();
            this.cbSignPage = new System.Windows.Forms.CheckBox();
            this.tbSubTotal = new System.Windows.Forms.TextBox();
            this.cbBikesIn = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRo)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvRo
            // 
            this.dgvRo.AllowUserToResizeColumns = false;
            this.dgvRo.AllowUserToResizeRows = false;
            this.dgvRo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7});
            this.dgvRo.Location = new System.Drawing.Point(12, 37);
            this.dgvRo.MultiSelect = false;
            this.dgvRo.Name = "dgvRo";
            this.dgvRo.ReadOnly = true;
            this.dgvRo.RowHeadersVisible = false;
            this.dgvRo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvRo.Size = new System.Drawing.Size(581, 404);
            this.dgvRo.TabIndex = 0;
            this.dgvRo.TabStop = false;
            this.dgvRo.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvRo_CellMouseDoubleClick);
            this.dgvRo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvRo_KeyPress);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Ro#";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 40;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Customer";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 130;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Make/Model";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 130;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "In";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 70;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Out";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 70;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Subtotal";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 70;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Total";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 70;
            // 
            // btPrintSelected
            // 
            this.btPrintSelected.Location = new System.Drawing.Point(469, 473);
            this.btPrintSelected.Name = "btPrintSelected";
            this.btPrintSelected.Size = new System.Drawing.Size(124, 23);
            this.btPrintSelected.TabIndex = 2;
            this.btPrintSelected.TabStop = false;
            this.btPrintSelected.Text = "Print Selected";
            this.btPrintSelected.UseVisualStyleBackColor = true;
            this.btPrintSelected.Click += new System.EventHandler(this.btPrintSelected_Click);
            // 
            // tbRo
            // 
            this.tbRo.Location = new System.Drawing.Point(12, 11);
            this.tbRo.Name = "tbRo";
            this.tbRo.Size = new System.Drawing.Size(43, 20);
            this.tbRo.TabIndex = 1;
            // 
            // tbCustomer
            // 
            this.tbCustomer.Location = new System.Drawing.Point(61, 11);
            this.tbCustomer.Name = "tbCustomer";
            this.tbCustomer.Size = new System.Drawing.Size(119, 20);
            this.tbCustomer.TabIndex = 2;
            // 
            // tbMakeModel
            // 
            this.tbMakeModel.Location = new System.Drawing.Point(186, 11);
            this.tbMakeModel.Name = "tbMakeModel";
            this.tbMakeModel.Size = new System.Drawing.Size(127, 20);
            this.tbMakeModel.TabIndex = 3;
            // 
            // dtpFirst
            // 
            this.dtpFirst.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFirst.Location = new System.Drawing.Point(319, 11);
            this.dtpFirst.Name = "dtpFirst";
            this.dtpFirst.Size = new System.Drawing.Size(101, 20);
            this.dtpFirst.TabIndex = 4;
            this.dtpFirst.Value = new System.DateTime(2017, 8, 17, 12, 45, 12, 0);
            // 
            // dtpLast
            // 
            this.dtpLast.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpLast.Location = new System.Drawing.Point(494, 8);
            this.dtpLast.Name = "dtpLast";
            this.dtpLast.Size = new System.Drawing.Size(99, 20);
            this.dtpLast.TabIndex = 5;
            this.dtpLast.Value = new System.DateTime(2017, 8, 17, 12, 45, 12, 0);
            // 
            // btPrintAll
            // 
            this.btPrintAll.Location = new System.Drawing.Point(255, 473);
            this.btPrintAll.Name = "btPrintAll";
            this.btPrintAll.Size = new System.Drawing.Size(124, 23);
            this.btPrintAll.TabIndex = 2;
            this.btPrintAll.TabStop = false;
            this.btPrintAll.Text = "Print All";
            this.btPrintAll.UseVisualStyleBackColor = true;
            this.btPrintAll.Click += new System.EventHandler(this.btPrintAll_Click);
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(12, 473);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(124, 23);
            this.btClose.TabIndex = 2;
            this.btClose.TabStop = false;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // tbTotal
            // 
            this.tbTotal.Location = new System.Drawing.Point(524, 447);
            this.tbTotal.Name = "tbTotal";
            this.tbTotal.ReadOnly = true;
            this.tbTotal.Size = new System.Drawing.Size(69, 20);
            this.tbTotal.TabIndex = 6;
            // 
            // cbSignPage
            // 
            this.cbSignPage.AutoSize = true;
            this.cbSignPage.Location = new System.Drawing.Point(12, 447);
            this.cbSignPage.Name = "cbSignPage";
            this.cbSignPage.Size = new System.Drawing.Size(110, 17);
            this.cbSignPage.TabIndex = 7;
            this.cbSignPage.Text = "Print signing page";
            this.cbSignPage.UseVisualStyleBackColor = true;
            // 
            // tbSubTotal
            // 
            this.tbSubTotal.Location = new System.Drawing.Point(443, 447);
            this.tbSubTotal.Name = "tbSubTotal";
            this.tbSubTotal.ReadOnly = true;
            this.tbSubTotal.Size = new System.Drawing.Size(72, 20);
            this.tbSubTotal.TabIndex = 6;
            // 
            // cbBikesIn
            // 
            this.cbBikesIn.AutoSize = true;
            this.cbBikesIn.Checked = true;
            this.cbBikesIn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbBikesIn.Location = new System.Drawing.Point(128, 447);
            this.cbBikesIn.Name = "cbBikesIn";
            this.cbBikesIn.Size = new System.Drawing.Size(118, 17);
            this.cbBikesIn.TabIndex = 8;
            this.cbBikesIn.Text = "Show bikes in shop";
            this.cbBikesIn.UseVisualStyleBackColor = true;
            // 
            // HistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 508);
            this.Controls.Add(this.cbBikesIn);
            this.Controls.Add(this.cbSignPage);
            this.Controls.Add(this.tbSubTotal);
            this.Controls.Add(this.tbTotal);
            this.Controls.Add(this.dtpLast);
            this.Controls.Add(this.dtpFirst);
            this.Controls.Add(this.tbMakeModel);
            this.Controls.Add(this.tbCustomer);
            this.Controls.Add(this.tbRo);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btPrintAll);
            this.Controls.Add(this.btPrintSelected);
            this.Controls.Add(this.dgvRo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HistoryForm";
            this.Text = "History";
            this.Load += new System.EventHandler(this.HistoryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvRo;
        private System.Windows.Forms.Button btPrintSelected;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.TextBox tbRo;
        private System.Windows.Forms.TextBox tbCustomer;
        private System.Windows.Forms.TextBox tbMakeModel;
        private System.Windows.Forms.DateTimePicker dtpFirst;
        private System.Windows.Forms.DateTimePicker dtpLast;
        private System.Windows.Forms.Button btPrintAll;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.TextBox tbTotal;
        private System.Windows.Forms.CheckBox cbSignPage;
        private System.Windows.Forms.TextBox tbSubTotal;
        private System.Windows.Forms.CheckBox cbBikesIn;
    }
}