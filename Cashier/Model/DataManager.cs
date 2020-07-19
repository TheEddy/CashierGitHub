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
        public ObservableCollection<WarehouseItem> GetWarehouseItems()
        {
            try
            {
                ObservableCollection<WarehouseItem> itemsList = JsonConvert.DeserializeObject<ObservableCollection<WarehouseItem>>(File.ReadAllText(@"C:\Users\Administrator\source\repos\TheEddy\CashierGitHub\items.json"));
                return itemsList;
            }
            catch (Exception e)
            {
                MessageBox.Show("Warehouse items file not exists");
                File.WriteAllText(@"C:\Users\Administrator\source\repos\TheEddy\CashierGitHub\types.json", "Empty file creation");
                return new ObservableCollection<WarehouseItem>();
            }
            return new ObservableCollection<WarehouseItem>();
            
        }

        public void SaveWarehouse(ObservableCollection<WarehouseItem> itemsList)
        {
            string json = JsonConvert.SerializeObject(itemsList, Formatting.Indented);
           // MessageBox.Show("Collection serialized" + "\n" + json);a

            File.WriteAllText(@"C:\Users\Administrator\source\repos\TheEddy\CashierGitHub\items.json", JsonConvert.SerializeObject(itemsList, Formatting.Indented));
        }

        public ObservableCollection<Types> GetItemTypes()
        {
            try
            {
                ObservableCollection<Types> types = JsonConvert.DeserializeObject<ObservableCollection<Types>>(File.ReadAllText(@"C:\Users\Administrator\source\repos\TheEddy\CashierGitHub\types.json"));
                return types;
            }
            catch (Exception e)
            {
                MessageBox.Show("Items types file not exists");
                File.WriteAllText(@"C:\Users\Administrator\source\repos\TheEddy\CashierGitHub\types.json", "Empty file creation");
                return new ObservableCollection<Types>();
            }
            return new ObservableCollection<Types>();
            
        }

        public void SaveItemTypes(ObservableCollection<Types> itemsTypesList)
        {
            string json = JsonConvert.SerializeObject(itemsTypesList, Formatting.Indented);
            // MessageBox.Show("Collection serialized" + "\n" + json);a

            File.WriteAllText(@"C:\Users\Administrator\source\repos\TheEddy\CashierGitHub\types.json", JsonConvert.SerializeObject(itemsTypesList, Formatting.Indented));
        }

        public List<int> GetListID()
        {
            try
            {
                List<int> types = JsonConvert.DeserializeObject<List<int>>(File.ReadAllText(@"C:\Users\Administrator\source\repos\TheEddy\CashierGitHub\itemIDs.json"));
                return types;
            }
            catch (Exception e)
            {
                MessageBox.Show("Item IDs file not exists");
                File.WriteAllText(@"C:\Users\Administrator\source\repos\TheEddy\CashierGitHub\itemIDs.json", @"[0]");
                return new List<int>(0);
            }
            return new List<int>();

        }

        public void SaveListID(List<int> itemsTypesList)
        {
            string json = JsonConvert.SerializeObject(itemsTypesList, Formatting.Indented);
            //MessageBox.Show("Collection serialized" + "\n" + json);a

            File.WriteAllText(@"C:\Users\Administrator\source\repos\TheEddy\CashierGitHub\itemIDs.json", JsonConvert.SerializeObject(itemsTypesList, Formatting.Indented));
        }
    }
}
