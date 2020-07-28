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

        private ObservableCollection<Owners> _ownersOC;           //Stores data of datagrid

        public ObservableCollection<Owners> ownersOC              //Allows to read/write/delete "Types" from datagrid
        {
            get { return _ownersOC; }
            set { _ownersOC = value; OnPropertyChanged("ownersOC"); dataManager.SaveItems(ownersOC); }
        }

        private Owners _selectedOwner;

        public Owners selectedOwner
        {
            get
            {
                return _selectedOwner;
            }
            set
            {
                _selectedOwner = value;
                OnPropertyChanged("selectedOwner");
                OnPropertyChanged("Types");
                OnPropertyChanged("TypesProvider");
            }
        }




        private Types _selectedType;

        public Types selectedType
        {
            get { return _selectedType; }
            set
            {
                _selectedType = value;
                OnPropertyChanged("selectedType");
                OnPropertyChanged("TypesShape");
                OnPropertyChanged("TypesMaterial");
            }
        }

        public void SaveOwners()                                 //Used for "Save" Button. Method saves all "Types" to .json file
        {
            dataManager.SaveItems(ownersOC);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(String propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public NewTypeVM()                                      // On each initialization get collection of existing "Types" from .json file
        {
            ownersOC = dataManager.GetItems(ownersOC);
            selectedOwner = ownersOC[0];
            selectedType = ownersOC[0].Types[0];
            
            OnPropertyChanged("ownersOC");
            OnPropertyChanged("selectedOwner");
            OnPropertyChanged("selectedType");
        }

        public void AddNewOwner()
        {
            int lastIndex = ownersOC.Count - 1;
            _ownersOC.Add
                (
                new Owners()
                {
                    Owner = "0",
                    Types = new ObservableCollection<Types>()
                    {
                        new Types()
                        {
                            Type = "0",
                            TypesMaterials = new ObservableCollection<Materials>() { new Materials() { Material = "0" } },
                            TypesShape = new ObservableCollection<Shapes>() { new Shapes() { Shape = "0" } }
                        }
                    },
                    TypesProviders = new ObservableCollection<Providers>() { new Providers() { Provider = "0" } },
                    //TypesShape = new ObservableCollection<Shapes>() { new Shapes() { Shape = "0" } }
                }
                );
            //_ownersOC[lastIndex].Types = new ObservableCollection<Types>(){new Types() { Type = "0" }};
            //_ownersOC[lastIndex].TypesMaterial = new ObservableCollection<Materials>() { new Materials() { Material = "0" } };
            //_ownersOC[lastIndex].TypesProviders = new ObservableCollection<Providers>() { new Providers() { Provider = "0" } };
            //_ownersOC[lastIndex].TypesShape = new ObservableCollection<Shapes>() { new Shapes() { Shape = "0" } };

        }

        public void AddNewMaterial()
        {
            selectedType.AddNewMaterial();
            OnPropertyChanged("TypesMaterial");
        }

        public void AddNewShape()
        {
            selectedType.AddNewShape();
            OnPropertyChanged("TypesShape");
        }
    }
}
