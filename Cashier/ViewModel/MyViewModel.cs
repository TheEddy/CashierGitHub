using Cashier.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace Cashier.ModelView
{
    // INotifyPropertyChanged notifies the View of property changes, so that Bindings are updated.
    sealed class MyViewModel : INotifyPropertyChanged
    {
        private Gloves item;

        private DataManager dataManager = new DataManager();


        public List<Item> listItems { get; set; }


        private ObservableCollection<Item> _itemsCollection;

        public ObservableCollection<Item> itemsCollection
        {
            get { return _itemsCollection; }
            set { _itemsCollection = value; OnPropertyChanged("itemsCollection"); }
        }

        public MyViewModel()
        {
            itemsCollection = dataManager.GetItems();
            //MessageBox.Show("Object deserialized" + File.ReadAllText(@"C:\Users\Administrator\source\repos\Cashier\items.json"));
            item = new Gloves()
            {
                ItemName = "Purple Hat",
                ItemAmount = 10,
                ItemPrice = 14.25,
                Material = "Wool",
                Size = "8"
                //HatSize = "10"
            };

            //listItems.Add(item);


            if (itemsCollection == null) itemsCollection = itemsList;
            //itemsCollection = itemsCollection.Add(item);
        }


        public ObservableCollection<Item> itemsList = new ObservableCollection<Item>()
        {
            new Hat(){ItemName = "Purple Hat", ItemAmount = 2, ItemPrice = 10.1, Material = "Paper", Size = "8"},
            new Hat(){ItemName = "Red Hat", ItemAmount = 1, ItemPrice = 14.25, Material = "Wool", Size = "6"},
            new Gloves(){ItemName = "Cow Skin glowes big", ItemAmount = 1, ItemPrice = 25.25, Material = "Cow Skin", Size = "12"},
            new Gloves(){ItemName = "Deer Skin glowes small", ItemAmount = 1, ItemPrice = 54.25, Material = "Deer Skin", Size = "8"},
            new Item(){ItemName = "Breloque", ItemAmount = 1, ItemPrice = 1}
        };

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        public void UpdateTable()
        {
            itemsCollection.Add(item);
        }

        public void SaveJSON()
        {
            dataManager.JsonSerialize(this._itemsCollection);
        }
    }
}
