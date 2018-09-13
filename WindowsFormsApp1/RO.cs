using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace MotoRev
{
    public class RO
    {
        [JsonProperty]
        private int id=-1;
        [JsonProperty]
        public Customer customer;
        [JsonProperty]
        public Bike bike;
        [JsonProperty]
        public DateTime dateIn;
        [JsonProperty]
        public DateTime dateOut;
        [JsonProperty]
        public List<Service> services;
        [JsonProperty]
        public bool saveOldParts;
        [JsonProperty]
        public List<PartQty> parts;
        [JsonProperty]
        public string takenBy;
        [JsonProperty]
        public decimal tires;
        [JsonProperty]
        public decimal hourlyRate;
        [JsonProperty]
        public double deposit;
        [JsonProperty]
        public string descriptionOfWork;
        [JsonProperty]
        public double gasOilGreas;

        public int getId()
        {
            return id;
        }
        public void changeID(int desiredID)
        {
            if (id == -1)
            {
                id = desiredID;
                return;
            }
            delete();
            this.id = desiredID;
            DataManager.addRO(this);
        }
        /// Deletes file, and removes this instance from DataManager
        public void delete()
        {
            string oldPath = Path.Combine(DataManager.roPath, this.id.ToString() + ".dat");
            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }
            DataManager.removeRoAtID(this.id);
        }



        private void importMakeModel()
        {
            if(bike.make=="" || bike.model == "")
            {
                return;
            }
            if (!DataManager.makeModel.ContainsKey(bike.make))
            {
                DataManager.makeModel.Add(bike.make, new HashSet<string>());
            }
            DataManager.makeModel[bike.make].Add(bike.model);
        }

        public void improtROData()
        {
            importMakeModel();
            
        }
       

        public void writeToFile()
        {
            System.IO.StreamWriter writer = new StreamWriter(Path.Combine(DataManager.roPath, id.ToString() + ".dat"));
            writer.Write(JsonConvert.SerializeObject(this, Formatting.Indented));
            writer.Close();
        }

        public double getTotalHours()
        {
            double hours = 0;
            foreach (Service service in services)
            {
                hours += service.hour;
            }
            return hours;
        }
        public double getTotalLabor()
        {
            return Math.Round(getTotalHours() * Convert.ToDouble(hourlyRate),2);
        }
        public double getTotalPartsPrice()
        {
            double totalParts = 0;
            foreach (PartQty partQty in parts)
            {
                totalParts += partQty.part.price * partQty.qunatity;
            }
            return Math.Round(totalParts,2);
        }
        public double getTotalWaste()
        {
            return Convert.ToDouble(tires) * 3;
        }
        public double getSubTotal()
        {
            return getTotalPartsPrice() + getTotalLabor() + getTotalWaste() + gasOilGreas;
        }
        public double getTax()
        {
            return Math.Round(getSubTotal() * 0.1,2);
        }
        public double getTotal()
        {
            return Math.Round(getSubTotal() * 1.1,2);
        }
        public double getDept()
        {
            return Math.Round(getTotal() - deposit,2);
        }


        public bool isCLosed()
        {
            if (dateOut.Equals(DateTime.MinValue))
            {
                return false;
            } else
            {
                return true;
            }
        }
        public bool filter(int id, DateTime dateFirst, DateTime dateLast, bool filteringDateOut, string makeModel, string customer)
        {
            if (id > 0)
            {
                if (this.id != id)
                      return false;
            }
            if (filteringDateOut)
            {
                //If it isnt closed we dont need to check date
                if (isCLosed())
                {
                    if (dateFirst > dateOut || dateLast < dateOut)
                    {
                        return false;
                    }
                } 
                    
            } else
            {
                if(dateFirst > dateIn || dateLast < dateIn)
                {
                    return false;
                }
            }
           
            
            if (makeModel != "")
            {
                if (!bike.getMakeModel().ToLower().Contains(makeModel.ToLower()))
                {
                    return false;
                }
            }
            if (customer != "")
            {
                if (!this.customer.name.ToLower().Contains(customer.ToLower()))
                {
                    return false;
                }
            }
            return true;
        }
    }

}
