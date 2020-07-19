using Cashier.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cashier.ViewModel
{
    class NewTypeVM : INotifyPropertyChanged
    {
        private DataManager dataManager = new DataManager();

        private List<Types> _typesListVM;

        public List<Types> typesListVM
        {
            get { return _typesListVM; }
            set { _typesListVM = value; OnPropertyChanged("typesList"); dataManager.SaveItemTypes(typesOC); }
        }
        public void GetTypes()
        {
            typesOC = dataManager.GetItemTypes();
            OnPropertyChanged("typesOC");
            //OnPropertyChanged("NewTypeVM.typesOC");
        }
        private ObservableCollection<Types> _typesOC;

        //public ObservableCollection<string> typesOC
        //{
        //    get
        //    {
        //        //_typesOC = new ObservableCollection<string>(_typesListVM);
        //        return _typesOC;
        //    }
        //    set
        //    {
        //        _typesOC = value;
        //    }
        //}

        public ObservableCollection<Types> typesOC
        {
            get { return _typesOC; }
            set { _typesOC = value; OnPropertyChanged("typesOC"); dataManager.SaveItemTypes(typesOC); }
        }

        private Types _newTypeName;
        public Types newTypeName
        {
            get
            {
                return _newTypeName;
            }
            set
            {
                _newTypeName = value;
            }
        }

        public void SaveTypes()
        {
            dataManager.SaveItemTypes(typesOC);
        }

        public void AddNewType()
        {
            List<Types> vs = _typesListVM;
            vs.Add(_newTypeName);

            typesListVM = vs;
            OnPropertyChanged("typesList");
            //MessageBox.Show("New Type Succesfully added!");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(String propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
                //SaveJSON();
            }
        }
        Types types = new Types();
        public NewTypeVM()
        {
            typesOC = dataManager.GetItemTypes();
            

            //types.Type = "Fur Hat";
            //typesOC.Add(types);
            //types = new Types();
            //types.Type = "Paper Hat";
            //typesOC.Add(types);
            //types = new Types();
            //types.Type = "Gloves";
            //typesOC.Add(types);
            //types = new Types();
            //types.Type = "Scarf";
            //typesOC.Add(types);
            //types = new Types();
            //types.Type = "Hoodie";
            //typesOC.Add(types);

            OnPropertyChanged("typesOC");
            //typesOC = new ObservableCollection<Types>(_typesListVM);
        }
    }
}
