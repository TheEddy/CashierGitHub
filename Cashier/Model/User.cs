using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier.Model
{
    class Item : INotifyPropertyChanged
    {
        private string _ItemName;
        private double _ItemPrice;
        private double _ItemAmount;

        public string ItemName
        {
            get
            {
                return _ItemName;
            }
            set
            {
                _ItemName = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ItemName"));
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
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ItemPrice"));
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
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ItemAmount"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

     class Hat : Item
    {
        private string _Material;
        private string _Size;
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string Material
        {
            
            get
            {
                return _Material;
            }
            set
            {
                _Material = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Material"));
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
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Size"));
            }
        }
    }

    class Gloves : Item
    {
        private string _Material;
        private string _Size;
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string Material
        {
            get
            {
                return _Material;
            }
            set
            {
                _Material = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Material"));
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
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Size"));
            }
        }
    }
}
