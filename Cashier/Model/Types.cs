using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier.Model
{
    class Types
    {
        private string _type;
        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                if (value == null) value = "0";
                else _type = value;
            }
        }

        public override string ToString()
        {
            return _type;
        }

        public Types()
        {
            _type = "0";
        }
    }
}
