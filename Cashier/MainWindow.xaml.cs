﻿using Cashier.ModelView;
using Cashier.View;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Cashier
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MyViewModel _viewModel;

        private Window1 NewTypeWindow;


        public MainWindow()
        {
            //OperationEnable();
            InitializeComponent();
            _viewModel = new MyViewModel();
            DataContext = _viewModel;
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.UpdateTable();
        }

        private void dataGrid1_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                _viewModel.GrantNewID();
                //MessageBox.Show("Changes Saved!");
            }
        }

        //private void dataGrid1_AddNewItem(object sender, AddingNewItemEventArgs e)
        //{
        //    MessageBox.Show("New Item added!");
        //}

        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            //MessageBox.Show("Closing called");

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
                    _viewModel.SaveWarehouse();
                }
            }
        }

        private void OperationEnable()
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

        private void HistoryEnable()
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

        private void WarehouseEnable()
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

        private void TextBlock_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OperationEnable();

        }

        private void TextBlock_PreviewMouseDown_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            HistoryEnable();
        }

        private void TextBlock_PreviewMouseDown_2(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            WarehouseEnable();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SaveWarehouse();
            MessageBox.Show("Changes Saved!");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewTypeWindow = new Window1();
            //NewTypeWindow.Activate();
            NewTypeWindow.Show();
        }

        private void TextBlock_PreviewMouseDown_3(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // not used
        }

        private void WarehouseGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            MessageBox.Show("New Item");
            _viewModel.GrantNewID();
        }
    }
}