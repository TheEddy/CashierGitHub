using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier.Model
{
    class Materials
    {
        private string _Material;
        public string Material
        {
            get { return _Material; }
            set
            {
                if (value == null) value = "0";
                _Material = value;
            }
        }

        public Materials()
        {

        }
    }
}
