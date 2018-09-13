using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoRev
{
    class Config
    {
        private Config()
        {

        }
        private decimal horlyRate = 95;
        private static Config instance;

        private static string resourcesPath;
        private static string configPath;
        
        public decimal HorlyRate
        {
            get => horlyRate;
            set
            {
                horlyRate = value;
                if (instance != null)
                     SaveToFile();
            }
        }

        public static Config getInstance()
        {
            if (instance == null)
            {
                initFromFile();
            }
            return instance;

        }

        public static void initFromFile()
        {
            resourcesPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            resourcesPath = System.IO.Path.Combine(resourcesPath, "MotoRev");
             configPath = Path.Combine(resourcesPath, "config.dat");
            if (File.Exists(configPath))
            {
                StreamReader sr = new StreamReader(configPath);
                instance = JsonConvert.DeserializeObject<Config>(sr.ReadToEnd());
                sr.Close();
            }  else
            {
                instance = new Config();
            }
  
        }

        public void SaveToFile()
        {
            StreamWriter sw = new StreamWriter(configPath);
            sw.Write(JsonConvert.SerializeObject(this, Formatting.Indented));
            sw.Close();
           

        }

        

    }
}
