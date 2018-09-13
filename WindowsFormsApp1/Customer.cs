using Newtonsoft.Json;
using System.IO;

namespace MotoRev
{
    public class Customer
    {
        public int id = -1;
        [JsonProperty]
        public string name;
        [JsonProperty]
        public string cellPhone;
        [JsonProperty]
        public string phone;
        [JsonProperty]
        public string Adress;
        [JsonProperty]
        public string email;
        [JsonProperty]
        public string cityStateZip;

        public void writeToFile()
        {
            System.IO.StreamWriter writer = new StreamWriter(Path.Combine(DataManager.customerPath,id.ToString() +".dat"));
            writer.Write(JsonConvert.SerializeObject(this, Formatting.Indented));
            writer.Close();
        }
        public string getInfo()
        {
            string rets = name;
            if (Adress != "")
            {
                rets += ", " + Adress;
            }
            if (cityStateZip != "")
            {
                rets += "," + cityStateZip;
            }
            return rets;
        }
        public string getContact()
        {
            string rets = cellPhone;
            if (phone != "")
            {
                rets += ", " + phone;
            }
            if (email != "")
            {
                rets += ", " + email;
            }
            return rets;
        }
        public override string ToString()
        {
           
            return name;

        }


    }

}
