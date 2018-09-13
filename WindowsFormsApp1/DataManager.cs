using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Text.RegularExpressions;

namespace MotoRev
{
    static class DataManager
    {
        public static List<User> users;
        public static Dictionary<string, HashSet<string>> makeModel;
        private static Dictionary<int, RO> rosIndexed = new Dictionary<int, RO>();
        private static Dictionary<int, Customer> customersIndexed = new Dictionary<int, Customer>();
        private static int currentRoID = 0;
        private static int currentRoFileID = 0;
        private static int currentCustomerID = 0;
        public static string resourcesPath;
        public static string usernamePath;
        public static string customerPath;
        public static string configPath;
        public static string partPath;
        public static string roPath;
        public static Image logo;
        private static bool writeMode = false;

 
        public static int getNextROFileID()
        {
            return ++currentRoFileID;
        }
        public static List<Customer> getCustomersList()
        {
            return customersIndexed.Values.ToList();
        }
        public static List<RO> getRosList()
        {
            return rosIndexed.Values.ToList();
        }
        public static int  getCurrentRoId()
        {
            return currentRoID; 
        }
        public static int getCurrentCustomerId()
        {
            return currentCustomerID;
        }
        public static void removeRoAtID(int id)
        {
           if(rosIndexed.ContainsKey(id))
            {
                rosIndexed.Remove(id);
            }
        }
        public static void addRO(RO ro)
        {

            if (rosIndexed.ContainsKey(ro.getId()))
            {
                rosIndexed[ro.getId()] = ro;
            } else
            {
                rosIndexed.Add(ro.getId(), ro);
                if (ro.getId() > currentRoID)
                {
                    currentRoID = ro.getId();
                }

            }
            
            ro.improtROData();
            if (writeMode)
            {
                ro.writeToFile();
            }
        }

        public static void addCustomer(Customer customer)
        {
            if (customer.id == -1)
            {
                customer.id = ++currentCustomerID;
            }
            if (customersIndexed.ContainsKey(customer.id))
            {
                customersIndexed[customer.id] = customer;
            }
            else
            {
                customersIndexed.Add(customer.id, customer);
            }
            if (writeMode)
            {
                customer.writeToFile();
            }
           
        }

        public static Customer getCustomerByID(int customerID)
        {
            try
            {
                Customer rets = customersIndexed[customerID];
                return rets;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public static void init()
        {

            resourcesPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            resourcesPath = System.IO.Path.Combine(resourcesPath, "MotoRev");
            usernamePath = System.IO.Path.Combine(resourcesPath, "username.dat");
            customerPath = Path.Combine(resourcesPath, "customers");
            partPath = Path.Combine(resourcesPath, "parts");
            roPath = Path.Combine(resourcesPath, "ro");
            makeModel = new Dictionary<string, HashSet<string>>();
            string logoPath = Path.Combine(resourcesPath, "resources", "logo.jpg");
            logo = Image.FromFile(logoPath);
            
            if (!System.IO.File.Exists(resourcesPath))
            {
                System.IO.Directory.CreateDirectory(resourcesPath);
            }
            if (!System.IO.File.Exists(usernamePath))
            {
                List<User> users = new List<User>
                {
                    new User("revMoto", "admin", "Jerry Sustek", true)
                };
                System.IO.StreamWriter writer = new System.IO.StreamWriter(usernamePath);
                writer.Write(JsonConvert.SerializeObject(users, Formatting.Indented));
                writer.Close();
            }
            if (!System.IO.File.Exists(customerPath))
            {
                System.IO.Directory.CreateDirectory(customerPath);
            }
            if (!System.IO.File.Exists(partPath))
            {
                System.IO.Directory.CreateDirectory(partPath);
            }
            if (!System.IO.File.Exists(roPath))
            {
                System.IO.Directory.CreateDirectory(roPath);
            }
            loadUsers();
            loadData();

        }

        public static RO getRoById(int roId)
        {
            try
            {
                RO rets = rosIndexed[roId];
                return rets;
            }
            catch (Exception)
            {

                return null;
            }      
        }

        static void loadUsers()
        {
            StreamReader sr = new StreamReader(usernamePath);
            users = JsonConvert.DeserializeObject<List<User>>(sr.ReadToEnd());
            sr.Close();
        }
        static void loadROs()
        {
            string[] roFiles = Directory.GetFiles(roPath, "*.dat");
            Array.Sort(roFiles);
            foreach (string path in roFiles)
            {
                
                currentRoID = Convert.ToInt32(Path.GetFileNameWithoutExtension(path));
                StreamReader sr = new StreamReader(path);
                RO newRO;
                newRO = JsonConvert.DeserializeObject<RO>(sr.ReadToEnd());
                
                addRO(newRO);
                sr.Close();
            }

        }

        private static readonly Regex rxNonDigits = new Regex(@"[^\d]+");

        static private string CleanStringOfNonDigits_V1(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            string cleaned = rxNonDigits.Replace(s, "");
            return cleaned;
        }

        public static void extractCustomersFromRO()
        {

            List<Customer> addedCustomers = new List<Customer>(customersIndexed.Values);
            List<Customer> customerMatches;
            Customer current;
            foreach(RO ro in rosIndexed.Values)
            {
                current = ro.customer;
                if (current != null)
                {

                    customerMatches = addedCustomers.Where(x =>
                     {

                         string phone = CleanStringOfNonDigits_V1(x.cellPhone);
                         string cphone = CleanStringOfNonDigits_V1(current.cellPhone);
                         
                         return x.name.Equals(current.name) || phone.Equals(cphone);

                     }).ToList();
                    if (customerMatches.Count == 0)
                    {
                        current.id = -1;
                        addedCustomers.Add(current);
                        addCustomer(current);
                    }

                }
            }
        }



        static void loadCustomers()
        {
           
            string[] customerFiles = Directory.GetFiles(customerPath, "*.dat");
            Array.Sort(customerFiles);
            int maxId = 0;
            foreach (string path in customerFiles)
            {
                currentCustomerID = Convert.ToInt32(Path.GetFileNameWithoutExtension(path));
                if (currentCustomerID > maxId)
                {
                    maxId = currentCustomerID;
                }
                StreamReader sr = new StreamReader(path);
                Customer newCustomer;
                newCustomer = JsonConvert.DeserializeObject<Customer>(sr.ReadToEnd());
                newCustomer.id = currentCustomerID;
                addCustomer(newCustomer);
                sr.Close();
            }
                currentCustomerID = maxId;
        }

        static void loadData()
        {
            writeMode = false;
            loadROs();
            loadCustomers();
            writeMode = true;
        }

    }

    public class User
    {
        [JsonProperty]
        string username;
        [JsonProperty]
        string password;
        [JsonProperty]
        string name;
        [JsonProperty]
        bool isAdmin = false;
        public User(string username, string password, string name, bool admin)
        {
            this.username = username;
            this.password = password;
            this.name = name;
            this.isAdmin = admin;
        }

        public bool logIn(string username, string password)
        {
            return username.Equals(this.username) && password.Equals(this.password);
        }

        internal string getName()
        {
            return name;
        }
    } 

    public class Service
    {
        [JsonProperty]
        public string description;
        [JsonProperty]
        public double hour;
    }
    public class Bike
    {
        [JsonProperty]
        public string color;
        [JsonProperty]
        public  int year;
        [JsonProperty]
        public string make;
        [JsonProperty]
        public string model;
        [JsonProperty]
        public string licNo;
        [JsonProperty]
        public string odo;
        [JsonProperty]
        public string engineNo;
        [JsonProperty]
        public string frameNo;
        [JsonProperty]
        public string keyNo;
        public string getMakeModel()
        {
            return make + " " + model;
        }
    }
    public class Part
    {
        [JsonProperty]
        public string partNo;
        [JsonProperty]
        public string description;
        [JsonProperty]
        public double price;
    }

    public class PartQty
    {
        public enum State
        {
            Initial, Ordered, Recieved
        }
        [JsonProperty]
        public Part part;
        [JsonProperty]
        public int qunatity = 1;
        [JsonProperty]
        public State state = State.Initial;
        public void setState(Color color)
        {
            if (color == new Color())
            {
                state = State.Initial;
            }
            else if (color == Color.PaleVioletRed)
            {
                state = State.Ordered;
            }
            else
            {
                state = State.Recieved;
            }
        }
        public Color getColorFromState()
        {
            if (state == State.Initial)
            {
                return new Color();
            }
            else if (state == State.Ordered)
            {
                return Color.PaleVioletRed;
            }
            return Color.LightGreen;
        }

        public PartQty(Part part, int qunatity)
        {
            this.part = part;
            this.qunatity = qunatity;
        }
    }

}
