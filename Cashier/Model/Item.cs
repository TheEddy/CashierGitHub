using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Cashier.Model
{
    class Item : INotifyPropertyChanged                                                             //Basis object for OperationItem and WarehouseItem
    {
        private int _ItemCode = 0;                                                                  //Default code value, what will be overwritten by VM logic
        private string _ItemName;   
        private double _ItemPrice;
        private double _ItemAmount;
        private IDManager IDManager;                                                                //ID Manager for creating unique warehouse IDs and keeping them.
        public event PropertyChangedEventHandler PropertyChanged;                                   //Event for a VM and V, that value is updated


        public string ItemName                                                                      //Fields for read/write via Data Grid in UI
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
                if (value == 0) _ItemCode = IDManager.GetNewItemID();   //If ItemCode = 0, then receive new ID for it on change
                else _ItemCode = value;
                OnPropertyChanged("ItemCode");
            }
        }


        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public Item()                                                                               //Initialize ID Manager
        {
            IDManager = new IDManager();
        }


        public void UpdateCode()                                                                    //Generate new warehouse ID for item.
        {
            ItemCode = IDManager.GetNewItemID();                                                    
        }
    }


    class OperationItem : Item                                                                      //Object, what used in OperationCollection for read/write in OperationDataGrid
    {
        private double _ItemDiscount = 0;                                                           //Unique for OperationItem.
        public double ItemDiscount
        {
            get
            {
                return _ItemDiscount;
            }
            set
            {
                if (value > 15)                                                                     //Maximum discount could be only 15%
                {
                    MessageBox.Show("Discount can't be more than 15%!");
                    _ItemDiscount = 15;
                }
                else _ItemDiscount = value;

                OnPropertyChanged("ItemDiscount");                                                  //Update cell in data grid
                OnPropertyChanged("ItemTotalPrice");                                                //Calculate total price with applied discount
                OnPropertyChanged("operationSum");                                                  //Calculate full sum of items in a OperationCollection(Whole datagrid)
            }
        }

        private double _ItemTotalPrice = 0;                                                         //Unique property for OperationItem = (Amount*price)-discount

        private double _ItemAmount = 0;
        public double ItemAmount
        {
            get
            {
                return _ItemAmount;
            }
            set
            {
                _ItemAmount = value;
                UpdateTotalPrice();
                OnPropertyChanged("ItemAmount");
                OnPropertyChanged("ItemTotalPrice");
            }
        }

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

        public void UpdateTotalPrice()                                                              //Updating totalprice of line.
        {
            _ItemTotalPrice = ItemPrice * ItemAmount;
            if (_ItemDiscount != null)
            {
                _ItemTotalPrice = _ItemTotalPrice - ((_ItemTotalPrice / 100) * _ItemDiscount);
            }
            OnPropertyChanged("ItemTotalPrice");
        }
    }

    class WarehouseItem : Item                                                                      //Object, what used in WarehouseCollection for read/write in WarehouseDataGrid
    {   
        private Types _ItemType;                                                                   //Unique property for WarehouseItem. Describes group of object.
        private Owners _ItemOwner;
        private Providers _ItemProvider;
        private Materials _ItemMaterial;
        private Shapes _ItemShape;

        DataManagerV2 dataManager = new DataManagerV2();                                            //Used for gathering Types object (group property)

        private ObservableCollection<Owners> _OwnerList;

        public ObservableCollection<Owners> OwnerList                                                //Group property
        {
            get                                                                                     //On get, reads value from .json file and returns all possible types to be used.
            {
                _OwnerList = dataManager.GetItems(_OwnerList);
                return _OwnerList;
            }
            set                                                                                     //On set, saves new Types list to .json file. On new get, they will be gathered for each WarehouseItem object again
            {
                dataManager.SaveItems(_OwnerList);
                _OwnerList = value;

                OnPropertyChanged("OwnerList");
            }
        }

        public WarehouseItem()                                                                      //On initialization, get all possible types from .json 
                                                                                                        //and set "0" value as default for newly created objects
        {
            _OwnerList = dataManager.GetItems(_OwnerList);
            _OwnerList = new ObservableCollection<Owners>()
            {
                new Owners()
                {
                   Owner = "0",
                   //TypesShape = new ObservableCollection<Shapes>(){ new Shapes() { Shape ="0"} },
                   Types = new ObservableCollection<Types>(){new Types() { Type = "0"} },
                   //TypesMaterial = new ObservableCollection<Materials>() { new Materials() { Material = "0"}}
                }
            };
        }


        public Owners ItemOwner
        {
            get
            {
                return _ItemOwner;
            }
            set
            {
                _ItemOwner = value;
                OnPropertyChanged("Types");
                OnPropertyChanged("TypesProviders");

            }
        }

        public Types ItemType                                                                      //Selected Types property. Used for keeping existing value in DataGrid Cell
        {
            get
            {
                return _ItemType;
            }
            set
            {
                _ItemType = value;
                OnPropertyChanged("ItemType");
                OnPropertyChanged("Material");
                OnPropertyChanged("Shape");
            }
        }

        public Providers ItemProvider
        {
            get
            {
                return _ItemProvider;
            }
            set
            {
                _ItemProvider = value;
                OnPropertyChanged("ItemProvider");
                OnPropertyChanged("TypesProviders");
                OnPropertyChanged("Provider");
            }
        }

        public Materials ItemMaterial
        {
            get
            {
                return _ItemMaterial;
            }
            set
            {
                _ItemMaterial = value;
                OnPropertyChanged("ItemMaterial");
                OnPropertyChanged("Material");
            }
        }

        public Shapes ItemShape
        {
            get
            {
                return _ItemShape;
            }
            set
            {
                _ItemShape = value;
                OnPropertyChanged("ItemShape");
                OnPropertyChanged("Shape");
            }
        }

        public OperationItem ToOperationItem(WarehouseItem warehouseItem)                           //Method for creation of OperationItem out of WarehouseItem.
        {
            OperationItem operationItem = new OperationItem();
            operationItem.ItemCode = warehouseItem.ItemCode;
            operationItem.ItemName = warehouseItem.ItemName + " (" + warehouseItem.ItemType.Type + ' ' + warehouseItem.ItemMaterial.Material +' ' + warehouseItem.ItemShape.Shape + " )";
            operationItem.ItemPrice = warehouseItem.ItemPrice;
            operationItem.ItemAmount = 1;
            operationItem.ItemDiscount = 0;                                                         //Add unique value for operation item manually. Total Price Item will be calculated automatically

            return operationItem;
        }
    }
    
    class HistoryItem : INotifyPropertyChanged                          //No inheritance cause there is only 1 field is same.
    {
        private DateTime _DateTime;
        private HistoryIDManager IDManager;                             //Work with different ID Manager.

        public HistoryItem()
        {
            IDManager = new HistoryIDManager();
        }

        public DateTime DateTime                                        //DateTime of operation to store.
        {
            get
            {
                return _DateTime;
            }
            set
            {
                _DateTime = value;
            }
        }

        private ObservableCollection<OperationItem> _OperationHistory;
        public ObservableCollection<OperationItem> OperationHistory     //Collection of items, what was sold.
        {
            get
            {
                return _OperationHistory;
            }
            set
            {
                _OperationHistory = value;
            }
        }

        private string _ItemLine;

        public string ItemLine                                          // Short description of sold items for a preview
        {
            get
            {
                return _ItemLine;
            }
            set
            {
                _ItemLine = value;
            }
        }

        private int _ItemCode;

        public int ItemCode                                             //Unique code of operation in a history. Used for receipt and History DataGrid
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

        public void UpdateCode()
        {
            ItemCode = IDManager.GetNewItemID();
        }

        private double _TotalSum;

        public double TotalSum
        {
            get
            {
                return _TotalSum;
            }
            set
            {
                _TotalSum = value;
            }
        }

        public HistoryItem GenerateNewHistoryItem(ObservableCollection<OperationItem> operationItems)       //Used for creation of new HistoryItem out of OperationItem
        {
            var item = new HistoryItem();
            item.DateTime = DateTime.Now;
            item.ItemLine = GenerateNewLineName(operationItems);                                            //Generate short description
            item.TotalSum = GenerateTotalSum(operationItems);                                               //Get whole items sum for a record

            item.OperationHistory = new ObservableCollection<OperationItem>();                              //Create new collection of OperationItems what will be stored in file/history.
            foreach (OperationItem operationItem in operationItems)
            {
                item.OperationHistory.Add(operationItem);
                //Add them one by one in new collection, otherwise it will not be a copy of object,
                //But just a link to existing and if Current OperationCollection will change,
                //It will be changed in history as well
            }

            return item;
        }
        
        private string GenerateNewLineName(ObservableCollection<OperationItem> opItems)                     //Method for generation of short description for preview
        {
            string lineName = "Items: ";
            foreach (OperationItem opItem in opItems)
            {
                if (opItem.ItemName != null)
                {
                    lineName = lineName + opItem.ItemName + "; ";                                           //Gather all Item Names in collection and stack them in a string.
                }
            }
            return lineName;
        }

        private double GenerateTotalSum(ObservableCollection<OperationItem> opItems)                        //Method for generation of total operation sum.
        {
            double totalSum = 0;
            foreach (OperationItem opItem in opItems)
            {
                if (opItem.ItemTotalPrice != 0)
                {
                    totalSum += opItem.ItemTotalPrice;                                                      //Summarize all line's total price properties into one double.
                }
            }
            return totalSum;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
