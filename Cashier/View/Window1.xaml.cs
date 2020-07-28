using Cashier.ViewModel;
using System.ComponentModel;
using System.Windows;

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
            _viewModel.AddNewOwner();
            //_viewModel.SaveOwners();
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
                _viewModel.SaveOwners();
            }
        }

        private void DataGrid_AddingNewItem(object sender, System.Windows.Controls.AddingNewItemEventArgs e)        // Not used
        {
            //
        }

        private void DataGrid_AddingNewItem_1(object sender, System.Windows.Controls.AddingNewItemEventArgs e)
        {
            //_viewModel.AddNewType();
        }

        private void AddShapeButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddNewShape();
        }

        private void AddMaterialButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddNewMaterial();
        }
    }
}
