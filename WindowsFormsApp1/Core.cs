
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoRev
{
    public static class Core
    {
        public static User logedIn;
        public static CustomersForm customersForm;
        public static bool LogIn(string username, string password)
        {
            foreach(User user in DataManager.users){
                if (user.logIn(username, password)){
                    logedIn = user;
                    return true;
                }
            }
            return false;
        }

        public static List<Customer> getCustomersByName(string search)
        {
            List<Customer> rets = new List<Customer>();
          
            foreach (Customer customer in DataManager.getCustomersList())
            {
                if (customer.name.ToLower().Contains(search.ToLower()))
                    rets.Add(customer);
            }
            return rets;
        }

        public static void showCustomerForm()
        {
            if (customersForm == null)
            {
                customersForm = new CustomersForm(false);

            } else
            {
                customersForm.Close();
                customersForm = new CustomersForm(false);
            }
            
            customersForm.Show();
        }

        public static Customer selectCustomerFromCustomerDialog()
        {
            CustomersForm customerDialog = new CustomersForm(true);
            customerDialog.ShowDialog();
            return customerDialog.getSelectedCustomer();
        }
    }
}
