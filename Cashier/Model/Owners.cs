

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Cashier.Model
{
    class Owners                     // Defines groups of WarehouseItem s
    {
        private string _Owner;
        public string Owner
        {
            get
            {
                //if (_Owner == null) { _Owner = "0"; return "0"; }
                return _Owner;
            }
            set
            {
                //if (value == null) _Owner = "0";
                //else
                    _Owner = value;
            }
        }

        ObservableCollection<Types> _Types;

        public ObservableCollection<Types> Types
        {
            get
            {
                //if (_Types == null)
                //{
                    //ObservableCollection<Types> list = new ObservableCollection<Types>();

                    //list.Add(new Types() { Type = "0" });
                    //_Types = list;
                  //  return _Types;
                //}
                //else
                    return _Types;
            }
            set
            {
                //ObservableCollection<Types> list = new ObservableCollection<Types>();

                //if (value == null)
                //{
                    //list.Add(new Types() { Type = "0"});
                  //  _Types = list;
                //}
                //else
                    _Types = value;
            }
        }



        ObservableCollection<Providers> _TypesProviders;

        public ObservableCollection<Providers> TypesProviders
        {
            get
            {
                    return _TypesProviders;
            }
            set
            {
                    _TypesProviders = value;
            }
        }





        public override string ToString()
        {
            return _Owner;
        }

        public Owners()
        {
            //_Owner = _Owner == null ? "0" : _Owner;
            //_Types = _Types == null ? new ObservableCollection<Types>() { new Types() { Type = "0" } } : _Types;
            //_TypesMaterial = _TypesMaterial == null ? new ObservableCollection<Materials>() { new Materials() { Material = "0" } } : _TypesMaterial;
            //_TypesProviders = _TypesProviders == null ? new ObservableCollection<Providers>() { new Providers() { Provider = "0" } } : _TypesProviders;
            //_TypesShape = _TypesShape == null ? new ObservableCollection<Shapes>() { new Shapes() { Shape = "0" } } : _TypesShape;
            //_Types = new ObservableCollection<Types>() { new Types() { Type = "0" } };
            //_TypesMaterial = new ObservableCollection<Materials>() { new Materials() { Material = "0" } };
            //_TypesProviders = new ObservableCollection<Providers>() { new Providers() { Provider = "0" } };
            //_TypesShape = new ObservableCollection<Shapes>() { new Shapes() { Shape = "0" } };
        }

        
    }
}
