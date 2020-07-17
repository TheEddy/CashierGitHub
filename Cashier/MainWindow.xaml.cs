using Cashier.ModelView;
using System.ComponentModel;
using System.Windows;

namespace Cashier
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MyViewModel _viewModel;


        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MyViewModel();
            DoneButton.Click += Done_Click;
            // The DataContext serves as the starting point of Binding Paths
            DataContext = _viewModel;
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
           int i =_viewModel.itemsList.Count;
            _viewModel.UpdateTable();
        }

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
                    _viewModel.SaveJSON();
                }
            }
        }
    }
}
