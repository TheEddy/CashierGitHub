

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

        ObservableCollection<Materials> _TypesMaterial;

        public ObservableCollection<Materials> TypesMaterial
        {
            get
            {
                //if (_TypesMaterial == null)
                //{
                    //ObservableCollection<Materials> list = new ObservableCollection<Materials>();

                    //list.Add(new Materials() { Material = "0" });
                    //_TypesMaterial = list;
                    return _TypesMaterial;
                //}
                //else
                    return _TypesMaterial;
            }
            set
            {
                //ObservableCollection<Materials> list = new ObservableCollection<Materials>();

                //if (value == null)
                //{
                    //list.Add(new Materials() { Material = "0"});
                    //_TypesMaterial = list;
                //}
                //else
                    _TypesMaterial = value;
            }
        }

        ObservableCollection<Providers> _TypesProviders;

        public ObservableCollection<Providers> TypesProviders
        {
            get
            {
                //if (_TypesProviders == null)
                //{
                  //  ObservableCollection<Providers> list = new ObservableCollection<Providers>();

                    //list.Add(new Providers() { Provider = "0" });
                    //_TypesProviders = list;
                    return _TypesProviders;
                //}
                //else return _TypesProviders;
            }
            set
            {
                //ObservableCollection<Providers> list = new ObservableCollection<Providers>();
                //if (value == null)
                //{
                    //list.Add(new Providers() { Provider = "0"});
                    //_TypesProviders = list;
                //}
                //else 
                    _TypesProviders = value;
            }
        }


        ObservableCollection<Shapes> _TypesShape;

        public ObservableCollection<Shapes> TypesShape
        {
            get
            {
                //if (_TypesShape == null)
                //{
                  //  ObservableCollection<Shapes> list = new ObservableCollection<Shapes>();

                    //list.Add(new Shapes() { Shape = "0" });
                    //_TypesShape = list;
                    return _TypesShape;
                //}
                //else return _TypesShape;
            }
            set
            {
                //ObservableCollection<Shapes> list = new ObservableCollection<Shapes>();
                ///if (value == null)
                //{
                  //  list.Add(new Shapes() { Shape = "0" }) ;
                    //_TypesShape = list;
                //}
                //else 
                _TypesShape = value;
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
