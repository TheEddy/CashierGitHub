using Cashier.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Cashier.ModelView
{
    // INotifyPropertyChanged notifies the View of property changes, so that Bindings are updated.
    sealed class MyViewModel : INotifyPropertyChanged
    {
        private WarehouseItem item;

        private DataManagerV2 dataManager = new DataManagerV2();                //Used for data read/write to .json files

        private PrintManager printManager = new PrintManager();                 //Used for print operations.

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _AdminMode;
        public bool AdminMode {
            get { return _AdminMode; }
            set { _AdminMode = value; OnPropertyChanged("WarehouseButton"); }
        }

        private string _Password;

        public string Password { get { if (_Password == null) return ""; else return _Password;
            } set { _Password = value; TryAuthorize();
            } }

        public Visibility WarehouseButton
        {
            get
            {
                if (_AdminMode) { Password = ""; return Visibility.Visible; }
                else return Visibility.Collapsed;
            }
        }

        private ObservableCollection<OperationItem> _operationCollection;       //Keeps current "OperationItem" in collection
        public ObservableCollection<OperationItem> operationCollection          //Allows to read/write/delete "OperationItem" from datagrid
        {
            get { return _operationCollection; }
            set { _operationCollection = value; OnPropertyChanged("operationCollection"); }
        }


        private ObservableCollection<HistoryItem> _historyCollection;           //Keeps current "HistoryItem" in collection
        public ObservableCollection<HistoryItem> historyCollection              //Allows to read/write/delete "HistoryItem" from datagrid
        {
            get { return _historyCollection; }
            set {
                _historyCollection = value;
                OnPropertyChanged("historyCollection"); SaveHistory();            }
        }
        private HistoryItem _SelectedHistoryItem;                               //Keeps here currently selected item from "HistoryItem" datagrid
        public HistoryItem SelectedHistoryItem                                  //Allows View to modify selected item from "HisroryItem" datagrid
        {
            get{ return _SelectedHistoryItem; }
            set { _SelectedHistoryItem = value; }
        }


        private ObservableCollection<WarehouseItem> _warehouseCollection;       //Keeps current "WarehouseItem" in collection
        public ObservableCollection<WarehouseItem> warehouseCollection
        {
            get {
                return _warehouseCollection;
            }
            set {
                _warehouseCollection = value;
                OnPropertyChanged("warehouseCollection");
                SaveWarehouse(); }
        }       //Allows to read/write/delete "WarehouseItem" from datagrid
        private WarehouseItem _selectedWarehouseItem;                           // Keeps current selected value from HistoryCollection
        public WarehouseItem selectedWareHouseItem                              // Allows View to set this propery
        {
            get
            {
                return _selectedWarehouseItem;
            }
            set
            {
                _selectedWarehouseItem = value;
                OnPropertyChanged("selectedWareHouseItem");
            }
        }

        private Types _selectedType;

        public Types selectedType
        {
            get { return _selectedType; }
            set
            {
                _selectedType = value;
                OnPropertyChanged("ItemType");
            }
        }




        private ObservableCollection<Owners> _expanderOwnersCollection;

        public ObservableCollection<Owners> expanderOwnersCollection
        {
            get { return _expanderOwnersCollection;
            }
            set
            {
                _expanderOwnersCollection = value;
                OnPropertyChanged("expanderOwnersCollection");
            }
        }



        private ObservableCollection<WarehouseItem> _expanderWarehouseCollection;       //Keeps current "WarehouseItem" in collection
        public ObservableCollection<WarehouseItem> expanderWarehouseCollection
        {
            get
            {
                return _expanderWarehouseCollection;
            }
            set
            {
                _expanderWarehouseCollection = value;
                OnPropertyChanged("expanderWarehouseCollection");
            }
        }       //Provides an ability to lookup for objects in datagrid

        private Owners _expanderSelectedOwner;

        public Owners expanderSelectedOwner
        {
            get { return _expanderSelectedOwner;
            }
            set
            {
                _expanderSelectedOwner = value;
                OnPropertyChanged("expanderSelectedOwner");
            }
        }

        private Providers _expanderSelectedProvider;

        public Providers expanderSelectedProvider
        {
            get
            {
                return _expanderSelectedProvider;
            }
            set
            {
                _expanderSelectedProvider = value;
                OnPropertyChanged("expanderSelectedProvider");
            }
        }

        private Types _expanderSelectedType;

        public Types expanderSelectedType
        {
            get
            {
                return _expanderSelectedType;
            }
            set
            {
                _expanderSelectedType = value;
                OnPropertyChanged("expanderSelectedType");
            }
        }

        private Materials _expanderSelectedMaterial;

        public Materials expanderSelectedMaterial
        {
            get
            {
                return _expanderSelectedMaterial;
            }
            set
            {
                _expanderSelectedMaterial = value;
                OnPropertyChanged("expanderSelectedMaterial");
            }
        }

        private Shapes _expanderSelectedShape;

        public Shapes expanderSelectedShape
        {
            get
            {
                return _expanderSelectedShape;
            }
            set
            {
                _expanderSelectedShape = value;
                OnPropertyChanged("expanderSelectedShape");
            }
        }

        private int? _expanderSearch;

        public string expanderSearch
        {
            get
            {
                if (_expanderSearch == 0) return "";                         // to make scanTextBox empty
                return _expanderSearch.ToString();
            }
            set
            {
                try
                {
                    if (value != "") _expanderSearch = Int32.Parse(value);
                    else _expanderSearch = 0;                                // if scanTextBox text is empty, then it's = 0

                    OnPropertyChanged("expanderSearch");
                }
                catch (FormatException e)                                       // ID can be only numeric char.
                {
                    MessageBox.Show("Only Numeric characters!");
                }
            }
        }

        private bool _expanderIncrement;

        public bool expanderIncrement
        {
            get { return _expanderIncrement; }
            set { _expanderIncrement = value; }
        }


        private bool _expanderDecrement;
        public bool expanderDecrement
        {
            get { return _expanderDecrement; }
            set { _expanderDecrement = value; }
        }


        private bool _expanderFind;
        public bool expanderFind
        {
            get { return _expanderFind; }
            set { _expanderFind = value; }
        }


        private int? _OperationItemCode;                                        // Keeps value from scanTextBox  on Operation Tab
        public string OperationItemCode                                         // Allows view to modify Text in a scanTextBox
        {
            get
            {
                if (_OperationItemCode == 0) return "";                         // to make scanTextBox empty
                return _OperationItemCode.ToString();
            }
            set
            {
                try
                {
                    if (value != "") _OperationItemCode = Int32.Parse(value);
                    else _OperationItemCode = 0;                                // if scanTextBox text is empty, then it's = 0

                    OnPropertyChanged("OperationItemCode");
                }
                catch (FormatException e)                                       // ID can be only numeric char.
                {
                    MessageBox.Show("Only Numeric characters!");
                }
            }
        }

        private double _operationSum;                                           // Keeps current value of Total Sum 
        public string operationSum                                              // Used for display of value in textbox
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






        public MyViewModel()                                                    //On initialization, create empty OperationCollection 
                                                                                //and get previous values for WarehouseCollection and HistoryCollection from .json files
        {
            warehouseCollection = dataManager.GetItems(warehouseCollection);
            operationCollection = new ObservableCollection<OperationItem>();
            historyCollection = dataManager.GetItems(historyCollection);
            _expanderOwnersCollection = dataManager.GetItems(_expanderOwnersCollection);
            expanderFind = true;
        }

        
        private void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public void SaveWarehouse()                                             //Method used for "Save" button on Warehouse Tab
                                                                                //And for saving WarehouseCollection to .json file
        {
            dataManager.SaveItems(this._warehouseCollection);
        }
        public void GrantNewID()                                                // Method used for refreshing "0" ID value on newly created WarehouseItem in WarehouseCollection
        {
            int lastID = warehouseCollection.Count();
            WarehouseItem warehouseItem = warehouseCollection[lastID - 1];
            int lastCode;

            if (lastID != 0) lastCode = warehouseItem.ItemCode;
            else lastCode = 1;

            if (lastCode == 0)
            {
                warehouseItem.UpdateCode();                  // Call an internal method of WarehouseItem to update ID value from "0" to new ID.
                string newCode = warehouseItem.ItemCode.ToString();
                string Annotation = warehouseItem.ItemName + " " + warehouseItem.ItemMaterial.Material + " " + warehouseItem.ItemShape.Shape;

                printManager.PrintNewLabel(newCode, Annotation);                           // Print a label with new WarehouseItem ID.
                OnPropertyChanged("warehouseCollection");
            }
        }

        public void SaveHistory()                                               //Method used for save HistoryCollection to .json file
        {
            dataManager.SaveItems(_historyCollection);
        }
        public void GrantNewHistoryID()                                         // Method used for refreshing "0" ID value on newly created HistoryItem in HistoryCollection
        {
            int lastID = historyCollection.Count();
            int lastCode = historyCollection[lastID - 1].ItemCode;

            if (lastCode == 0) historyCollection[lastID - 1].UpdateCode();      // Call an internal method of HistoryItem to update ID value from "0" to new ID.

            OnPropertyChanged("historyCollection");
        }



        public void AddNewOperationItem()                                       // Method what adds new OperationItem to OperationCollection based on value from scanTextBox
                                                                                // Called by "Enter" button click while inside of scanTextBox (MainWindow "Done_Click")
        {
            OperationItem operationItem = new OperationItem();
            WarehouseItem warehouseItem1 = new WarehouseItem();

            if (_OperationItemCode != null)
            {
                warehouseItem1 = warehouseCollection.FirstOrDefault(x => x.ItemCode == _OperationItemCode);         // Find a WarehouseItem with same ItemCode.
                try
                {
                    operationItem = warehouseItem1.ToOperationItem(warehouseItem1);                                 // Will cause NullReferenceException if there is no Object in WarehouseCollection with same ItemCode
                    //bool alreadyInList = false;
                    if (warehouseItem1.ItemAmount != 0)                                                             // Cannot be added if there is 0 items on warehouse
                    {
                        if (_operationCollection.FirstOrDefault(x => x.ItemCode == _OperationItemCode) != null)     // Checks is this item already exists in current OperationCollection (!=null - exists)
                        {
                            OperationItem item = _operationCollection.FirstOrDefault(x => x.ItemCode == _OperationItemCode);    //If exists, get this OperationItem and increment "Amount"

                            if (item.ItemAmount < warehouseItem1.ItemAmount)                                        // If there is "Amount" in OperationCollection less than in WarehouseCollection then we will increment ItemAmount
                            {
                                item.ItemAmount++;
                            }
                            else MessageBox.Show("No more items in stock!");                                        
                        }
                        else                                                                                        // Means, that there no this item in current OperationCollection and can add it.
                        {
                            _operationCollection.Add(operationItem);
                        }

                        OnPropertyChanged("operationCollection");
                    }
                    else MessageBox.Show("Item Code: " + warehouseItem1.ItemCode + "\nItem: " + warehouseItem1.ItemName + " cannot be added! \n Amount on warehouse = " + warehouseItem1.ItemAmount);
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
            OperationItemCode = "";
            OnPropertyChanged("OperationItemCode");
        }


        public void RemoveOperationItem()                                       // Method what adds new OperationItem to OperationCollection based on value from scanTextBox
                                                                                // Called by "Enter" button click while inside of scanTextBox (MainWindow "Done_Click")
        {
            OperationItem operationItem = new OperationItem();
            WarehouseItem warehouseItem1 = new WarehouseItem();

            if (_OperationItemCode != null)
            {
                if (_operationCollection.FirstOrDefault(x => x.ItemCode == _OperationItemCode) != null)     // Checks is this item already exists in current OperationCollection (!=null - exists)
                {
                    OperationItem item = _operationCollection.FirstOrDefault(x => x.ItemCode == _OperationItemCode);    //If exists, get this OperationItem and increment "Amount"
                    if (item.ItemAmount > 1)
                    {
                        item.ItemAmount--;
                    }
                    else
                    {
                        _operationCollection.Remove(item);
                    }
                    
                }
                else                                                                                        // Means, that there no this item in current OperationCollection and can add it.
                {
                    MessageBox.Show("This item not in a list");
                }

                OnPropertyChanged("operationCollection");
            }
            else MessageBox.Show("Enter Item ID First!");

            OperationItemCode = "";
            OnPropertyChanged("OperationItemCode");
        }

        public bool OperationDone()                                             // Method what registers current OperationCollection in HistoryCollection
                                                                                // And subtracts OperationItems from WarehouseItems     
                                                                                // Returns "true" when first receipt is printed out, to ask do you wish a second receipt
        {
            OperationItem operationItem = new OperationItem();
            WarehouseItem warehouseItem = new WarehouseItem();

            ObservableCollection<OperationItem> opCollection = new ObservableCollection<OperationItem>();
            opCollection = operationCollection;

            if (_operationCollection.Count != 0)                                // Check for emptyness of OperationCollection. If Empty, we will not register this operation in HistoryCollection
            {
                try
                {
                    foreach (OperationItem currentItem in _operationCollection) // Subtracts items from warehouse.
                    {

                        warehouseItem = _warehouseCollection.FirstOrDefault(x => x.ItemCode == currentItem.ItemCode);
                        warehouseItem.ItemAmount -= currentItem.ItemAmount;
                    }

                    HistoryItem historyItem = new HistoryItem();
                    historyItem = historyItem.GenerateNewHistoryItem(opCollection);     // Create new HistoryItem out of this OperationCollection

                    historyCollection.Add(historyItem);                                 // Add new HistoryItem to HistoryCollection
                    GrantNewHistoryID();                                                // Update HistoryItem ID for new item

                    printManager.PrintNewReceipt(historyItem.ItemCode.ToString(), historyItem.DateTime,                     // Print a receipt for this operation
                                                    historyItem.OperationHistory, historyItem.TotalSum.ToString());

                    OperationClear();                                                   // Clear OpecationCollection for next operation
                    SaveWarehouse();                                                    // Save new amount of items in WarehouseCollection to .json file

                    OnPropertyChanged("warehouseCollection");
                    return true;
                }
                catch (NullReferenceException e)
                {
                    MessageBox.Show("Some value is empty!\n Item Code cannot be \"0\"!");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("List is Empty!");
                return false;
            }
            
        }

        public void OperationClear()                                            // Method cleares OperationCollection. Called by method OperationDone()
                                                                                // And by ClearAllButton in MainWindow ("ClearAllButton_Click") Operation Tab
        {
            _operationCollection.Clear();

            OperationItemCode = "";

            SaveHistory();

            CaltulateSum();                                                     // Re-calculate total sum for operationSum property

            OnPropertyChanged("operationCollection");
        }


        public void PrintNewLabel()                                             // Method used to print out barcode label for selected WarehouseItem in WarehouseCollection
        {
            if (selectedWareHouseItem != null)
            {
                string itemCode = selectedWareHouseItem.ItemCode.ToString();
                string Annotation = selectedWareHouseItem.ItemName + " " + selectedWareHouseItem.ItemMaterial.Material + " " + selectedWareHouseItem.ItemShape.Shape;

                printManager.PrintNewLabel(itemCode, Annotation);
            }
            else MessageBox.Show("Choose Item from list first!");
        }

        public void PrintNewReceipt()                                           //  Method used to print out receipt for selected HistoryItem in HistoryCollection
        {
            if (_SelectedHistoryItem != null)
            {
                printManager.PrintNewReceipt(_SelectedHistoryItem.ItemCode.ToString(), _SelectedHistoryItem.DateTime, 
                            _SelectedHistoryItem.OperationHistory, _SelectedHistoryItem.TotalSum.ToString());
            }
        }

        public void PrintSecondReceipt()                                        // Method prints a receipt for last HistoryItem in HistoryCollection
                                                                                // Called by MainWindow "PrintRecipeButton_Click" in Operation Tab
                                                                                // Also called by positive answer in question messagebox in MainWindow "Done_Click"
        {
            HistoryItem historyItem = new HistoryItem();
            historyItem = _historyCollection.Last<HistoryItem>();               // Get last HistoryItem from HistoryCollection

            printManager.PrintNewReceipt(historyItem.ItemCode.ToString(), historyItem.DateTime,     // Print a receipt for last HistoryItem
                                                    historyItem.OperationHistory, historyItem.TotalSum.ToString());
        }


        public void CaltulateSum()                                              // Method calculates total sum for all items in OperationCollection. (Property operationSum)
        {
            double sum = 0;
            if (_operationCollection.Count >= 1)
            {
                foreach (OperationItem operationItem in _operationCollection)
                {
                    sum += operationItem.ItemTotalPrice;
                }
            }
            operationSum = sum.ToString();
        }


        public void WarehouseFiler()
        {
            if (expanderWarehouseCollection != null) expanderWarehouseCollection.Clear();
            else expanderWarehouseCollection = new ObservableCollection<WarehouseItem>();

            ObservableCollection<WarehouseItem> tempCollection = new ObservableCollection<WarehouseItem>();

            foreach (WarehouseItem warehouseItem in _warehouseCollection)
            {
                expanderWarehouseCollection.Add(warehouseItem);
                tempCollection.Add(warehouseItem);
            }
            foreach(WarehouseItem item in tempCollection)
            {
                if (item.ItemOwner != null)
                {
                    if (expanderSelectedOwner != null && expanderSelectedOwner.Owner != item.ItemOwner.Owner) _expanderWarehouseCollection.Remove(item);
                }
                else _expanderWarehouseCollection.Remove(item);

                if (item.ItemProvider != null)
                {
                    if (expanderSelectedProvider != null && expanderSelectedProvider.Provider != item.ItemProvider.Provider) _expanderWarehouseCollection.Remove(item);
                }
                else _expanderWarehouseCollection.Remove(item);

                if (item.ItemType != null)
                {
                    if (expanderSelectedType != null && expanderSelectedType.Type != item.ItemType.Type) _expanderWarehouseCollection.Remove(item);
                }
                else _expanderWarehouseCollection.Remove(item);

                if (item.ItemMaterial != null)
                {
                    if (expanderSelectedMaterial != null && expanderSelectedMaterial.Material != item.ItemMaterial.Material) _expanderWarehouseCollection.Remove(item);
                }
                else _expanderWarehouseCollection.Remove(item);

                if (item.ItemShape != null)
                {
                    if (expanderSelectedShape != null && expanderSelectedShape.Shape != item.ItemShape.Shape) _expanderWarehouseCollection.Remove(item);
                }
                else _expanderWarehouseCollection.Remove(item);
            }
            OnPropertyChanged("expanderWarehouseCollection");
        }



        public void WarehouseIncrementAction()
        {
            WarehouseItem warehouseItem1 = new WarehouseItem();

            if (_expanderSearch != null)
            {
                if (_warehouseCollection.FirstOrDefault(x => x.ItemCode == _expanderSearch) != null)     // Checks is this item already exists in current OperationCollection (!=null - exists)
                {
                    warehouseItem1 = _warehouseCollection.FirstOrDefault(x => x.ItemCode == _expanderSearch);    //If exists, get this OperationItem and increment "Amount"
                    if (_expanderIncrement)
                    {
                        warehouseItem1.ItemAmount++;
                    }
                    else if (_expanderDecrement)
                    {
                        if (warehouseItem1.ItemAmount >= 1) warehouseItem1.ItemAmount--;
                        else MessageBox.Show("Zero on stock!");

                    }
                    else if (_expanderFind)
                    {

                        selectedWareHouseItem = warehouseItem1;
                    }

                }
                else                                                                                        // Means, that there no this item in current OperationCollection and can add it.
                {
                    MessageBox.Show("This item not in a list");
                }

                OnPropertyChanged("warehouseCollection");
            }
            else MessageBox.Show("Enter Item ID First!");

            expanderSearch = "";
            OnPropertyChanged("expanderSearch");
        }

        public void TryAuthorize()
        {
            if (Password == "0000") AdminMode = true;
            else AdminMode = false;
        }
    }
}
