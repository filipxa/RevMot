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
    public partial class CustomerEditDialog : Form
    {
        private Customer customer;
        public bool result = false;
        public CustomerEditDialog(Customer customer)
        {
            this.customer = customer;
            InitializeComponent();
        }
        public CustomerEditDialog() : this(new Customer())
        {

        }

        private void CustomerEditDialog_Load(object sender, EventArgs e)
        {
            if (customer == null)
            {
                customer = new Customer();
            } else
            {
                autoFillCustomer(customer);
            }
        }
        private Customer getCustomerFromFields()
        {
            customer.Adress = tbCustomerAdress.Text;
            customer.cellPhone = tbCustomerPhone.Text;
            customer.cityStateZip = tbCustomerCSZ.Text;
            customer.email = tbCustomerEmail.Text;
            customer.name = tbName.Text;
            customer.phone = tbCustomerSecPh.Text;
            return customer;
        }
        private void autoFillCustomer(Customer customer)
        {
            tbName.Text = customer.name;
            tbCustomerAdress.Text = customer.Adress;
            tbCustomerCSZ.Text = customer.cityStateZip;
            tbCustomerEmail.Text = customer.email;
            tbCustomerPhone.Text = customer.cellPhone;
            tbCustomerSecPh.Text = customer.phone;
        }
        public Customer getCustomer()
        {
            return customer;
        }
        private bool validateCustomer()
        {
            bool rets = true;
            if (tbName.Text == "")
            {
                lbCustomerName.ForeColor = Color.Red;
                rets = false;
            }
            else
            {
                lbCustomerName.ForeColor = Color.Black;
            }

            if (tbCustomerPhone.Text == "")
            {
                lbCustomerPhone.ForeColor = Color.Red;
                rets = false;
            }
            else
            {
                lbCustomerPhone.ForeColor = Color.Black;
            }

            return rets;

        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if (validateCustomer())
            {
                customer = getCustomerFromFields();
                result = true;
                Close();
            }
            
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
