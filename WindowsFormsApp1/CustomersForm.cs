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
    public partial class CustomersForm : Form
    {
        private Customer selectedCustomer;
        private bool selectMode = false;
        public Customer getSelectedCustomer()
        {
            return selectedCustomer;
        }
        public CustomersForm(bool isSelectMode)
        {
            selectMode = isSelectMode;
            InitializeComponent();
            tbName.TextChanged += dataChanged;
        }
        bool triger = true;
        List<Customer> activeCustomers = new List<Customer>();
        private void CustomerForm_Load(object sender, EventArgs e)
        {
            btSelect.Visible = selectMode;
            btSelect.Enabled = false;
            fillDgv();
            tbName.KeyPress += keyPressInData;
        }

        private void fillDgv()
        {
            dgv.Rows.Clear();
            activeCustomers = Core.getCustomersByName(tbName.Text);
            foreach (Customer currentCustomer in activeCustomers)
            {
                addCustomerToDgv(currentCustomer);
            }

        }

        private void keyPressInData(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals((char)Keys.Escape))
            {
                clearFileds();
            }
        }
        private void dataChanged(object sender, EventArgs e)
        {
            if (triger)
                fillDgv();
        }

        private void clearFileds()
        {
            triger = false;
            tbName.Text = "";
            triger = true;
        }

        private void addCustomerToDgv(Customer customer)
        {
            object[] data = new object[4];
            data[0] = customer.id;
            data[1] = customer.name;
            data[2] = customer.cellPhone;
            data[3] = customer.email;
            dgv.Rows.Add(data);
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (dgv.Rows[e.RowIndex].IsNewRow)
            {
                return;
            }
            int customerID = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[0].Value);
            if (customerID < 1)
                return;
            Customer customer = DataManager.getCustomerByID(customerID);
            if (customer == null)
            {
                MessageBox.Show("Customer is null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            openCustomerEdit(customer);


        }
        private DataGridViewRow getRowOfSelectedCell()
        {
            DataGridViewRow rets = null;
            if (dgv.SelectedCells.Count > 0)
            {
                rets = dgv.SelectedCells[0].OwningRow;

            }
            return rets;
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = getRowOfSelectedCell();
            if (selectedRow == null)
            {
                MessageBox.Show("Selected row is null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int customerID = Convert.ToInt32(selectedRow.Cells[0].Value);
            Customer customer = DataManager.getCustomerByID(customerID);
            if (customer == null)
            {
                MessageBox.Show("Customer is null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            selectedCustomer = customer;
            Close();

        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (getRowOfSelectedCell() == null || getRowOfSelectedCell().IsNewRow)
            {
                btSelect.Enabled = false;
            } else
            {
                btSelect.Enabled = true;
            }
        }
        private void openCustomerEdit(Customer customer)
        {
            CustomerEditDialog customerDialog = new CustomerEditDialog(customer);
            customerDialog.ShowDialog();
            if(customer == null)
            {
                customer = customerDialog.getCustomer();
            }

            if (customerDialog.result)
            {

                DataManager.addCustomer(customerDialog.getCustomer());

                clearFileds();
                fillDgv();
                foreach(DataGridViewRow row in dgv.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        int id = Convert.ToInt32(row.Cells[0].Value);
                        if (id == customer.id)
                        {
                            row.Cells[1].Selected = true;
                            break;
                        }
                    }
                }
            }
        }
        private void openCustomerEdit()
        {
            openCustomerEdit(null);
        }

        private void btAddNew_Click(object sender, EventArgs e)
        {
            openCustomerEdit();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
                Close();

        }
    }
}
