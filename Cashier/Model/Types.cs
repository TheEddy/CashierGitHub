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
    }
}
