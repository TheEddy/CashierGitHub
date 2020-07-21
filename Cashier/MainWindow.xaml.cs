using Cashier.ModelView;
using Cashier.View;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Cashier
{
    /// <summary>
    /// Main window of application.
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MyViewModel _viewModel;

        private Window1 NewTypeWindow;

        private Window2 OperationHistoryWindow;


        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MyViewModel();
            DataContext = _viewModel;
        }

        private void Done_Click(object sender, RoutedEventArgs e)                                               // Method what reacts on "Done" button click in Operation Tab
                                                                                                                // Finishing of operation and adding it to history
        {
            if (_viewModel.OperationDone())
            {
                string msg = "Do you wish a second receipt?";
                MessageBoxResult result =
                  MessageBox.Show(
                    msg,
                    "Data App",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _viewModel.PrintSecondReceipt();
                }
            }
        }

        private void dataGrid1_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)                     //Method what tracks row updates in Operation datagrid
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                _viewModel.GrantNewID();
                _viewModel.CaltulateSum();
            }
        }

        void DataWindow_Closing(object sender, CancelEventArgs e)                                                 //Method what On windows close offers to save data
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
                _viewModel.SaveWarehouse();
            }
        }

        private void OperationEnable()                              //Enables Operation Tab
        {
            operationGrid.Visibility = Visibility.Visible;
            scanTextBox.Visibility = Visibility.Visible;
            DoneButton.Visibility = Visibility.Visible;
            historyGrid.Visibility = Visibility.Hidden;
            warehouseGrid.Visibility = Visibility.Hidden;
            SaveButton.Visibility = Visibility.Hidden;

            OperationStackPanel.Visibility = Visibility.Visible;
            HistoryStackPanel.Visibility = Visibility.Hidden;
            WarehouseStackPanel.Visibility = Visibility.Hidden;

        }
        private void HistoryEnable()                                //Enables History Tab
        {
            operationGrid.Visibility = Visibility.Hidden;
            scanTextBox.Visibility = Visibility.Hidden;
            DoneButton.Visibility = Visibility.Hidden;
            historyGrid.Visibility = Visibility.Visible;
            warehouseGrid.Visibility = Visibility.Hidden;
            SaveButton.Visibility = Visibility.Hidden;

            OperationStackPanel.Visibility = Visibility.Hidden;
            HistoryStackPanel.Visibility = Visibility.Visible;
            WarehouseStackPanel.Visibility = Visibility.Hidden;
        }
        private void WarehouseEnable()                              //Enables Warehouse Tab
        {
            operationGrid.Visibility = Visibility.Hidden;
            scanTextBox.Visibility = Visibility.Hidden;
            DoneButton.Visibility = Visibility.Hidden;
            historyGrid.Visibility = Visibility.Hidden;
            warehouseGrid.Visibility = Visibility.Visible;
            SaveButton.Visibility = Visibility.Visible;

            OperationStackPanel.Visibility = Visibility.Hidden;
            HistoryStackPanel.Visibility = Visibility.Hidden;
            WarehouseStackPanel.Visibility = Visibility.Visible;
        }

        private void TextBlock_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)         // Method what responses to click on "Operation" Tab
        {
            OperationEnable();
        }
        private void TextBlock_PreviewMouseDown_1(object sender, System.Windows.Input.MouseButtonEventArgs e)       // Method what responses to click on "History" Tab
        {
            HistoryEnable();
        }
        private void TextBlock_PreviewMouseDown_2(object sender, System.Windows.Input.MouseButtonEventArgs e)       // Method what responses to click on "Warehouse" Tab
        {
            WarehouseEnable();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)                                             // Method what responses to click on "Save" button in "Warehouse" tab
        {
            _viewModel.SaveWarehouse();
            MessageBox.Show("Changes Saved!");
        }

        private void Button_Click(object sender, RoutedEventArgs e)                                                 // Method what responses to click on "Add" button in "Warehouse" tab
        {
            NewTypeWindow = new Window1();
            NewTypeWindow.Show();
        }

        private void WarehouseGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)                           // Method what updates WarehouseItem ID on creation of new row in "Warehouse" tab
        {
            _viewModel.GrantNewID();
        }           

        private void ScanTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)                          // Method what adds new OperationItem in "Operation" Tab on "Enter" button click
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;
            e.Handled = true;
            _viewModel.AddNewOperationItem();
            _viewModel.CaltulateSum();
        }

        private void ClearAllButton_Click(object sender, RoutedEventArgs e)                                         // Method what clear current Operation datagrid in "Operation" Tab
        {
            _viewModel.OperationClear();
            _viewModel.CaltulateSum();
            MessageBox.Show("Current list has been cleared!");
        }

        private void OperationHistoryButton_Click(object sender, RoutedEventArgs e)                                 // Method what opens window with OperationCollection in "History" tab
        {
            OperationHistoryWindow = new Window2();
            OperationHistoryWindow.SetDataContext(_viewModel);
            OperationHistoryWindow.Show();

        }


        private void PrintLabelButton_Click(object sender, RoutedEventArgs e)                                       // Method what print-out a barcode label for selected item in Warehouse datagrid on button "Print" click
        {
            _viewModel.PrintNewLabel();
        }

        private void PrintRecipeButton_Click(object sender, RoutedEventArgs e)                                      // Method what print-out a receipt for last operation from History in Operation Tab on "Print" button click
        {
            _viewModel.PrintSecondReceipt();
        }

        private void PrintHistoryButton_Click(object sender, RoutedEventArgs e)                                     // Method what print-out a receipt for selected operation in History list on "Print" button click
        {
            _viewModel.PrintNewReceipt();
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)                                               // Not used
        {
            // not used
        }
        private void TextBlock_PreviewMouseDown_3(object sender, System.Windows.Input.MouseButtonEventArgs e)       // Not used
        {
            // not used
        }
    }
}
