using Cashier.ModelView;
using Cashier.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cashier.View
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private readonly NewTypeVM _viewModel;
        public Window1()
        {
            InitializeComponent();
            _viewModel = new NewTypeVM();
            DataContext = _viewModel;
            //
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //_viewModel.GetTypes();
            //_viewModel.AddNewType();
            //this.Close();
            _viewModel.SaveTypes();
        }
        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            //MessageBox.Show("Closing called");z

            // If data is dirty, notify user and ask for a response
            if (true)
            {
                string msg = "Save existing data?";
                MessageBoxResult result =
                  MessageBox.Show(
                    msg,
                    "Data App",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    // If user doesn't want to close, cancel closure
                    e.Cancel = true;
                }
                else if (result == MessageBoxResult.Yes)
                {
                    _viewModel.SaveTypes();
                }
            }
        }
    }
}
