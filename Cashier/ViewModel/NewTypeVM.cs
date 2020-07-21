using Cashier.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Cashier.ViewModel
{
    class NewTypeVM : INotifyPropertyChanged        //Used for logic description of "Window1"(Types window)
    {
        private DataManagerV2 dataManager = new DataManagerV2();        // Used for data reading/saving to .json files
                                                                        // As all data will be written/read to/from .json file by each VMs, data will be consistaint.

        private ObservableCollection<Types> _typesOC;           //Stores data of datagrid

        public ObservableCollection<Types> typesOC              //Allows to read/write/delete "Types" from datagrid
        {
            get { return _typesOC; }
            set { _typesOC = value; OnPropertyChanged("typesOC"); dataManager.SaveItems(typesOC); }
        }

        public void SaveTypes()                                 //Used for "Save" Button. Method saves all "Types" to .json file
        {
            dataManager.SaveItems(typesOC);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(String propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public NewTypeVM()                                      // On each initialization get collection of existing "Types" from .json file
        {
            typesOC = dataManager.GetItems(typesOC);
            
            OnPropertyChanged("typesOC");
        }
    }
}
