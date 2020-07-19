using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cashier.Model
{
    class IDManager
    {
        private static int _id;
        private static int _lastID;
        List<int> _listID;
        DataManager dataManager = new DataManager();
        public IDManager()
        {
            _listID = dataManager.GetListID();
            _lastID = _listID.Max();
            //_listID = new List<int>();
        }

        public int GetLastItemID()
        {
            int tmp = 0;
            
            try
            {
                tmp = _listID.Max();
                _lastID = tmp;
                return tmp;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e);
            }
            _lastID = tmp;
            return tmp;
        }
        public int GetNewItemID()
        {
            int tmp = 0;
            try
            {
                tmp = _listID.Max();
                _listID.Add(++tmp);
                _lastID = tmp;
                dataManager.SaveListID(_listID);
                return _lastID;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e);
            }
            return tmp;
        }
        public static int GenerateID()
        {
            return _lastID += 1;
        }
    }
}
