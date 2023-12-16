using CarShop.Models;
using CarShop.ViewModel;
using System.Windows.Controls;

namespace CarShop.Views
{
    /// <summary>
    /// Interaction logic for ClientView.xaml
    /// </summary>
    public partial class ClientView : UserControl
    {
        public ClientViewModel clientViewModel { get; set; }
        public ClientView()
        {
            InitializeComponent();
            clientViewModel = new ClientViewModel();
            DataContext = clientViewModel;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clientViewModel.SelectedClient = (Client)(sender as DataGrid).SelectedItem;
        }
    }
}
