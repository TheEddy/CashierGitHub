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
    /// This window is being opened by "Button_Click" on main window
    /// Separated window for "Types" collection editing
    /// </summary>
    public partial class Window1 : Window
    {
        private readonly NewTypeVM _viewModel;
        public Window1()
        {
            InitializeComponent();
            _viewModel = new NewTypeVM();
            DataContext = _viewModel;           //Link view objects to VM
        }

        private void Button_Click(object sender, RoutedEventArgs e)     // Click on button "Save" Saves existing types list
        {
            _viewModel.SaveTypes();
        }

        void DataWindow_Closing(object sender, CancelEventArgs e)       // Describes actions to be performed on window close
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
