using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier.Model
{
    class Providers
    {
        private string _Provider;
        public string Provider
        {
            get
            {
                return _Provider;
            }
            set
            {
                if (value == null) value = "0";
                _Provider = value;
            }
        }
    }
}
