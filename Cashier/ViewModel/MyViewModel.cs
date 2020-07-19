using Cashier.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace Cashier.ModelView
{
    // INotifyPropertyChanged notifies the View of property changes, so that Bindings are updated.
    sealed class MyViewModel : INotifyPropertyChanged
    {
        private WarehouseItem item;

        private DataManager dataManager = new DataManager();




        //public List<Item> listItems { get; set; }


        private ObservableCollection<OperationItem> _operationCollection;

        public ObservableCollection<OperationItem> operationCollection
        {
            get { return _operationCollection; }
            set { _operationCollection = value; OnPropertyChanged("operationCollection"); }
        }


        private ObservableCollection<object> _historyCollection;

        public ObservableCollection<object> historyCollection
        {
            get { return _historyCollection; }
            set { _historyCollection = value; OnPropertyChanged("historyCollection"); }
        }

        private ObservableCollection<WarehouseItem> _warehouseCollection;

        public ObservableCollection<WarehouseItem> warehouseCollection
        {
            get { return _warehouseCollection; }
            set { _warehouseCollection = value; OnPropertyChanged("warehouseCollection"); SaveWarehouse(); }
        }




        public MyViewModel()
        {
            warehouseCollection = dataManager.GetWarehouseItems();
            operationCollection = new ObservableCollection<OperationItem>();
            //typesListVM = dataManager.GetItemTypes();
            //_typesOC = new ObservableCollection<string>(_typesListVM);
            //item = new WarehouseItem()
            //{
            //    ItemCode = 111,
            //    ItemType = "Hat",
            //    ItemName = "Purple Hat",
            //    ItemAmount = 10,
            //    ItemPrice = 14.25
            //};

            //item.AddNewType("Fur Hat");
            //item.AddNewType("Paper Hat");
            //item.AddNewType("Gloves");
            //item.AddNewType("Scarf");
            //item.AddNewType("Hoodie");

            //listItems.Add(item);


            if (warehouseCollection == null) warehouseCollection = itemsList;
            //operationCollection = operationCollection.Add(item);
        }


        public ObservableCollection<WarehouseItem> itemsList = new ObservableCollection<WarehouseItem>()
        {
            //new Hat(){ItemName = "Purple Hat", ItemAmount = 2, ItemPrice = 10.1, Material = "Paper", Size = "8"},
           // new Hat(){ItemName = "Red Hat", ItemAmount = 1, ItemPrice = 14.25, Material = "Wool", Size = "6"},
            //new Gloves(){ItemName = "Cow Skin glowes big", ItemAmount = 1, ItemPrice = 25.25, Material = "Cow Skin", Size = "12"},
            //new Gloves(){ItemName = "Deer Skin glowes small", ItemAmount = 1, ItemPrice = 54.25, Material = "Deer Skin", Size = "8"},
           // new Item(){ItemName = "Breloque", ItemAmount = 1, ItemPrice = 1}
        };

        public event PropertyChangedEventHandler PropertyChanged;

        //public event RowEditEnding

        private void OnPropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
                //SaveJSON();
            }
        }

        public void UpdateTable()
        {
            ObservableCollection<WarehouseItem> newCol = new ObservableCollection<WarehouseItem>();
            newCol = _warehouseCollection;
            newCol.Add(item);
            //operationCollection.Add(item);
            warehouseCollection = newCol;
        }

        public void SaveWarehouse()
        {
            dataManager.SaveWarehouse(this._warehouseCollection);
        }

        public void GrantNewID()
        {
            int lastID = warehouseCollection.Count();
            int lastCode = warehouseCollection[lastID-1].ItemCode;
            if (lastCode == 0) warehouseCollection[lastID-1].UpdateCode();
            OnPropertyChanged("warehouseCollection");
        }

        private int _OperationItemCode;

        public string OperationItemCode
        {
            get
            {
                if (_OperationItemCode == 0) return "";
                return _OperationItemCode.ToString();
            }
            set
            {
                try
                {
                    if (value != "") _OperationItemCode = Int32.Parse(value);
                    else _OperationItemCode = 0;
                    OnPropertyChanged("OperationItemCode");
                }
                catch (FormatException e)
                {
                    MessageBox.Show("Only Numeric characters!");
                }
            }
        }

        private int _OperationDiscount;

        public int OperationDiscount
        {
            get
            {
                return _OperationDiscount;
            }
            set
            {
                _OperationDiscount = value;
                OnPropertyChanged("OperationDiscount");
            }
        }

        public void AddNewOperationItem()
        {
            OperationItem operationItem = new OperationItem();
            WarehouseItem warehouseItem1 = new WarehouseItem();
            if (_OperationItemCode != null)
            {
                warehouseItem1 = warehouseCollection.FirstOrDefault(x => x.ItemCode == _OperationItemCode);
                try
                {
                    operationItem = warehouseItem1.ToOperationItem(warehouseItem1);
                    //bool alreadyInList = false;
                    if (_operationCollection.FirstOrDefault(x => x.ItemCode == _OperationItemCode) != null)
                    {
                        OperationItem item = _operationCollection.FirstOrDefault(x => x.ItemCode == _OperationItemCode);
                        if (item.ItemAmount < warehouseItem1.ItemAmount)
                        {
                            item.ItemAmount++;
                            item.UpdateTotalPrice();
                        }
                        else MessageBox.Show("No more items in stock!");
                    }
                    else
                    {
                        _operationCollection.Add(operationItem);
                    }

                    OnPropertyChanged("operationCollection");
                }
                catch (NullReferenceException e)
                {
                    MessageBox.Show("No item found with this ID!");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Exception:\n'" + e.ToString());
                }
            }
            else MessageBox.Show("Enter Item ID First!");
        }

        public void OperationDone()
        {
            OperationItem operationItem = new OperationItem();
            WarehouseItem warehouseItem = new WarehouseItem();
            foreach(OperationItem currentItem in _operationCollection)
            {
                warehouseItem = _warehouseCollection.FirstOrDefault(x => x.ItemCode == currentItem.ItemCode);
                warehouseItem.ItemAmount -= currentItem.ItemAmount;
            }
            OperationClear();
            OnPropertyChanged("warehouseCollection");
        }
        public void OperationClear()
        {
            _operationCollection.Clear();
            OperationItemCode = "";
            CaltulateSum();
            OnPropertyChanged("operationCollection");
        }


        public void CaltulateSum()
        {
            double sum = 0;
            if (_operationCollection.Count >= 1)
            {
                foreach(OperationItem operationItem in _operationCollection)
                {
                    sum += operationItem.ItemTotalPrice;
                }
            }
            operationSum = sum.ToString();
            //OnPropertyChanged("operationSum");
        }

        private double _operationSum;

        public string operationSum
        {
            get
            {
                //CaltulateSum();
                return _operationSum + " €";
            }
            set
            {
                _operationSum = Double.Parse(value);
                OnPropertyChanged("operationSum");
            }
        }
    }
}
