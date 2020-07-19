using Cashier.ModelView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cashier.Model
{
    class Item : INotifyPropertyChanged
    {
        private int _ItemCode = 0;
        private string _ItemName;
        private double _ItemPrice;
        private double _ItemAmount;
        private IDManager IDManager;


        public string ItemName
        {
            get
            {
                return _ItemName;
            }
            set
            {
                _ItemName = value;
                OnPropertyChanged("ItemName");
            }
        }

        public double ItemPrice
        {
            get
            {
                return _ItemPrice;
            }
            set
            {
                _ItemPrice = value;
                OnPropertyChanged("ItemPrice");
            }
        }

        public double ItemAmount
        {
            get
            {
                return _ItemAmount;
            }
            set
            {
                _ItemAmount = value;
                OnPropertyChanged("ItemAmount");
            }
        }

        public int ItemCode
        {
            get
            {
                return _ItemCode;
            }
            set
            {
                if (value == 0) _ItemCode = IDManager.GetNewItemID();
                else _ItemCode = value;
                OnPropertyChanged("ItemCode");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Item()
        {
            IDManager = new IDManager();
            //_ItemName = "0";
            //if(_ItemName == null)_ItemCode = IDManager.GetNewItemID();
        }
        public void UpdateCode()
        {
            ItemCode = IDManager.GetNewItemID();
        }
    }


    class OperationItem : Item
    {
        private double _ItemDiscount = 0;
        public double ItemDiscount
        {
            get
            {
                return _ItemDiscount;
            }
            set
            {
                if (value > 15)
                {
                    MessageBox.Show("Discount can't be more than 15%!");
                    _ItemDiscount = 15;
                }
                else _ItemDiscount = value;
                OnPropertyChanged("ItemDiscount");
                double i = ItemTotalPrice;
                OnPropertyChanged("ItemTotalPrice");
                OnPropertyChanged("operationSum");
            }
        }

        private double _ItemTotalPrice = 0;

        public double ItemTotalPrice
        {
            get
            {
                _ItemTotalPrice = ItemPrice * ItemAmount;
                if (_ItemDiscount != null)
                {
                    _ItemTotalPrice = _ItemTotalPrice - ((_ItemTotalPrice / 100) * _ItemDiscount);
                }
                return _ItemTotalPrice;
            }
        }

        public void UpdateTotalPrice()
        {
            _ItemTotalPrice = ItemPrice * ItemAmount;
            if (_ItemDiscount != null)
            {
                _ItemTotalPrice = _ItemTotalPrice - ((_ItemTotalPrice / 100) * _ItemDiscount);
            }
            OnPropertyChanged("ItemTotalPrice");
        }
    }

    class WarehouseItem : Item
    {   
        private string _ItemType;

        DataManager dataManager = new DataManager();

        private ObservableCollection<Types> _typesList;

        public ObservableCollection<Types> typesList
        {
            get
            {
                _typesList = dataManager.GetItemTypes();
                return _typesList;
            }
            set
            {
                dataManager.SaveItemTypes(_typesList);
                _typesList = value;
                //_ItemType = value.ToString()
                OnPropertyChanged("typesList");
            }
        }

        public WarehouseItem()
        {
            _typesList = dataManager.GetItemTypes();
            _ItemType = "0";
            //ItemType = "test";
        }

        public string ItemType
        {
            get
            {
                return _ItemType;
            }
            set
            {
                _ItemType = value;
                OnPropertyChanged("ItemType");
            }
        }

        public OperationItem ToOperationItem(WarehouseItem warehouseItem)
        {
            OperationItem operationItem = new OperationItem();
            operationItem.ItemCode = warehouseItem.ItemCode;
            operationItem.ItemName = warehouseItem.ItemName;
            operationItem.ItemPrice = warehouseItem.ItemPrice;
            operationItem.ItemAmount = 1;
            operationItem.ItemDiscount = 0;

            return operationItem;
        }
    }
    
    class HistoryItem : Item
    {
        private ObservableCollection<OperationItem> _operationHistory;
        public ObservableCollection<OperationItem> operationHistory
        {
            get
            {
                return _operationHistory;
            }
            set
            {
                _operationHistory = value;
                OnPropertyChanged("operationHistory");
            }
        }

        private DateTime _dateTime;

        public DateTime dateTime
        {
            get
            {
                return _dateTime;
            }
            set
            {
                _dateTime = DateTime.Now;
            }
        }
    }
}
