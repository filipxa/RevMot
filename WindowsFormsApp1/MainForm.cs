using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotoRev
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            init();
        }
        public void init()
        {
            labelUsername.Text = Core.logedIn.getName();
            System.Drawing.Point point = labelUsername.Location;
            point.X = this.Size.Width - labelUsername.Size.Width - 30;
            this.labelUsername.Location = point;
        }

        
        private void btnNewRo_Click(object sender, EventArgs e)
        {
            RoForm rf=new RoForm();
            rf.ShowDialog();
        }

        private void btHistory_Click(object sender, EventArgs e)
        {
            HistoryForm hf = new HistoryForm();
            hf.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void btCustomer_Click(object sender, EventArgs e)
        {
            Core.showCustomerForm();
        }

        private void labelUsername_DoubleClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to extract customer information from RO's?", "Warrning", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                DataManager.extractCustomersFromRO();
            }
        }
    }
}
