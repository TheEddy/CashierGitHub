using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier.Model
{
    class Types
    {
        private string _Type;
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        private ObservableCollection<Materials> _TypesMaterials;

        public ObservableCollection<Materials> TypesMaterials
        {
            get
            {
                return _TypesMaterials;
            }
            set
            {
                _TypesMaterials = value;
            }
        }

        ObservableCollection<Shapes> _TypesShape;

        public ObservableCollection<Shapes> TypesShape
        {
            get
            {
                return _TypesShape;
            }
            set
            {
                _TypesShape = value;
            }
        }

        public void AddNewMaterial()
        {
            var TempCollection = TypesMaterials;
            if (TempCollection == null)
            {
                TempCollection = new ObservableCollection<Materials>() { new Materials() { Material = "0" } };
            }
            else TempCollection.Add(new Materials() { Material = "0" });
            TypesMaterials = TempCollection;
        }

        public void AddNewShape()
        {
            var TempCollection = TypesShape;
            if (TempCollection == null)
            {
                TempCollection = new ObservableCollection<Shapes>() { new Shapes() { Shape = "0" } };
            }
            else TempCollection.Add(new Shapes() { Shape = "0" });
            TypesShape = TempCollection;
        }
    }
}
