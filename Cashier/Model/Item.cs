using Cashier.ModelView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

     class Hat : Item
    {
        private string _Material;
        private string _Size;
        public string Material
        {
            
            get
            {
                return _Material;
            }
            set
            {
                _Material = value;
                OnPropertyChanged("Material");
            }
        }
        public string Size
        {
            get
            {
                return _Size;
            }
            set
            {
                _Size = value;
                OnPropertyChanged("Size");
            }
        }
    }

    class Gloves : Item
    {
        private string _Material;
        private string _Size;
        public string Material
        {
            get
            {
                return _Material;
            }
            set
            {
                _Material = value;
                OnPropertyChanged("Material");
            }
        }
        public string Size
        {
            get
            {
                return _Size;
            }
            set
            {
                _Size = value;
                OnPropertyChanged("Size");
            }
        }
    }

    class OperationItem : Item
    {
        private double _ItemDiscount;
        public double ItemDiscount
        {
            get
            {
                return _ItemDiscount;
            }
            set
            {
                _ItemDiscount = value;
                OnPropertyChanged("ItemDiscount");
            }
        }

        private double _ItemTotalPrice;

        public double ItemTotalPrice
        {
            get
            {
                double _ItemTotalPrice = ItemPrice * ItemAmount;
                if (_ItemDiscount != null)
                {
                    _ItemTotalPrice = _ItemTotalPrice - ((_ItemTotalPrice / 100) * _ItemDiscount);
                }
                return _ItemTotalPrice;
            }
        }
    }

    class WarehouseItem : Item
    {
        //Types _Types12 = new Types();
        //public Types Types12
        //{
        //    get
        //    {
        //        return _Types12;
        //    }
        //    set
        //    {
        //        _Types12 = value;
        //        _ItemType = value.Type;
        //    }
        //}

        

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
    }
}
