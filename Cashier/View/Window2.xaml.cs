using System.Windows;

namespace Cashier.View
{
    /// <summary>
    /// This window is used for operation collection representation in history items.
    /// Opened by "OperationHistoryButton_Click" on MainWindow in History tab.
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();

        }
        public void SetDataContext(object DataContext)      //Method used to set DataContext from MainWindow
        {
            this.DataContext = DataContext;
        }
    }
}
