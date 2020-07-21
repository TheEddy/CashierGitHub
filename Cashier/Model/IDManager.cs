using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cashier.Model
{
    abstract class IDManagerBody
    {
        abstract public int GetNewItemID();

    }

    class IDManager : IDManagerBody    //Responsible for ID management of Warehouse items and History items.
    {
        protected static int _id;
        protected static int _lastID;
        protected List<int> _listID;
        protected DataManagerV2 dataManager = new DataManagerV2();
        public IDManager()  // On initialize get last ID.
        {
            _listID = dataManager.GetID();     //Receive from List of int from saved file
            _lastID = _listID.Max();           //Check biggest number in it and set it as last used ID
        }

        public override int GetNewItemID()               //Public method, what creates new ID for warehouse Item object and returns it.
        {
            int tmp = 0;
            try
            {
                tmp = _listID.Max();            //Get last used is
                _listID.Add(++tmp);             //Increment it and add to list of integers
                _lastID = tmp;                  //Set Last used ID as this newly created one
                dataManager.SaveID(_listID);    //Save new list of integers to .json file
                return _lastID;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e);
            }
            return tmp;
        }
    }
    class HistoryIDManager : IDManager        //Does exaclty the same, but operates with another .json file
    {
        public HistoryIDManager()
        {
            _listID = dataManager.GetHistoryListID();
            _lastID = _listID.Max();
        }

        public override int GetNewItemID()
        {
            int tmp = 0;
            try
            {
                tmp = _listID.Max();
                _listID.Add(++tmp);
                _lastID = tmp;
                dataManager.SaveHistoryListID(_listID);
                return _lastID;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e);
            }
            return tmp;
        }
    }
}
