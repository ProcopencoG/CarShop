using CarShop.Models;
using CarShop.ViewModel;
using System.Windows.Controls;

namespace CarShop.Views
{
    /// <summary>
    /// Interaction logic for CarView.xaml
    /// </summary>
    public partial class CarView : UserControl
    {
        public CarViewModel carViewModel { get; set; }


        public CarView()
        {
            InitializeComponent();

            carViewModel = new CarViewModel();
            DataContext = carViewModel;

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            carViewModel.SelectedCar = (Car)(sender as DataGrid).SelectedItem;
        }
    }
}
