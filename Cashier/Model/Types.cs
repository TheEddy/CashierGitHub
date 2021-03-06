﻿

namespace Cashier.Model
{
    class Types                     // Defines groups of WarehouseItem s
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
