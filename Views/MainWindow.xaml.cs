using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CarShop.ViewModel;

namespace CarShop.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CarView carView { get; set; }
        public CarViewModel carViewModel { get; set; }

        public ClientView clientView { get; set; }
        public ClientViewModel clientViewModel { get; set; }

        public bool carViewVisible { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            carView = new CarView();
            carViewModel = carView.carViewModel;

            clientView = new ClientView();
            clientViewModel = clientView.clientViewModel;
        }

        private void btnClients_Click(object sender, RoutedEventArgs e)
        {
            changeView(sender);
        }

        private void btnCars_Click(object sender, RoutedEventArgs e)
        {
            changeView(sender);
        }

        private void changeView(object sender)
        {
            if(sender == btnCars)
            {
                carView = new CarView();
                carViewModel = carView.carViewModel;

                carViewVisible = true;
                btnClients.FontWeight = FontWeights.Regular;
                btnClients.Background = new SolidColorBrush(Color.FromRgb(26, 31, 53));
                btnClients.Foreground = new SolidColorBrush(Colors.White);
                userControlView.Content = carView;
            }
            else
            {

                clientView = new ClientView();
                clientViewModel = clientView.clientViewModel;

                carViewVisible = false;
                btnCars.FontWeight = FontWeights.Regular;
                btnCars.Background = new SolidColorBrush(Color.FromRgb(26, 31, 53));
                btnCars.Foreground = new SolidColorBrush(Colors.White);
                userControlView.Content = clientView;
            }

            ButtonsPanel.Visibility = Visibility.Visible;


            (sender as Button).FontWeight = FontWeights.Bold;
            (sender as Button).Background = new SolidColorBrush(Colors.White);
            (sender as Button).Foreground = new SolidColorBrush(Color.FromRgb(26, 31, 53));
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(carViewVisible)
            {
                CarAddEdit carAddEdit = new CarAddEdit(carViewModel);
                carAddEdit.Owner = this;
                carAddEdit.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                carAddEdit.ShowDialog();
            }
            else
            {
                ClientAddEdit clientAddEdit = new ClientAddEdit(clientViewModel);
                clientAddEdit.Owner = this;
                clientAddEdit.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                clientAddEdit.ShowDialog();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (carViewVisible)
            {
                CarAddEdit carAddEdit = new CarAddEdit(carViewModel, carViewModel.SelectedCar);
                carAddEdit.Owner = this;
                carAddEdit.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                carAddEdit.ShowDialog();
            }
            else
            {
                ClientAddEdit clientAddEdit = new ClientAddEdit(clientViewModel, clientViewModel.SelectedClient);
                clientAddEdit.Owner = this;
                clientAddEdit.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                clientAddEdit.ShowDialog();
            }
        }
    

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            if (carViewVisible) carViewModel.DeleteCommand.Execute(carViewModel);
            else clientViewModel.DeleteCommand.Execute(clientViewModel);
        }
    }
}
