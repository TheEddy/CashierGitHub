using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cashier.Model
{
    class DataManager
    {

        public ObservableCollection<Item> GetItems()
        {
            ObservableCollection<Item> itemsList = JsonConvert.DeserializeObject<ObservableCollection<Item>>(File.ReadAllText(@"C:\Users\Administrator\source\repos\Cashier\items.json"));
            return itemsList;
        }

        public void JsonSerialize(ObservableCollection<Item> itemsList)
        {
            string json = JsonConvert.SerializeObject(itemsList, Formatting.Indented);
           // MessageBox.Show("Collection serialized" + "\n" + json);a

            File.WriteAllText(@"C:\Users\Administrator\source\repos\Cashier\items.json", JsonConvert.SerializeObject(itemsList, Formatting.Indented));
        }
    }
}
