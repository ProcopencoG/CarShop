using CarShop.Models;
using CarShop.ViewModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CarShop.Views
{
    /// <summary>
    /// Interaction logic for CarAddEdit.xaml
    /// </summary>
    public partial class CarAddEdit : Window
    {
        ObservableCollection<string> ClientNames { get; set; } = new ObservableCollection<string>();

        public Car editCar { get; set; }
        public Client? SelectedClient { get; set; }

        ObservableCollection<Client> clients;
        private CarViewModel _carViewModel { get; set; }
        public CarAddEdit(CarViewModel carViewModel, Car carToEdit = null)
        {
            InitializeComponent();

            _carViewModel = carViewModel;
            DataContext = this;
            ClientViewModel clientViewModel = new ClientViewModel();
            clients = clientViewModel.Clients;
            foreach (Client client in clients)
            {
                ClientNames.Add(client.Name);
            }
            clientsComboBox.ItemsSource = ClientNames;

            if (carToEdit != null)
            {
                editCar = carToEdit;
                button.Content = "Edit";
                nameTextBox.Text = carToEdit.Name;

                if(carToEdit.Client != null)
                {
                    clientsComboBox.SelectedIndex = clientsComboBox.Items.IndexOf(carToEdit.Client.Name);
                }
                
            }

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (editCar != null)
            {
                if (SelectedClient != null)
                {
                    
                    editCar.Client = SelectedClient;
                    editCar.Name = nameTextBox.Text;

                    _carViewModel.EditedCar = editCar;

                }
                else
                {
                    editCar.Name = nameTextBox.Text;
                    editCar.Client = null;
                    _carViewModel.EditedCar = editCar;
                }
                _carViewModel.UpdateCommand.Execute(_carViewModel);
            }
            else 
            {
                if (SelectedClient != null)
                {
                    _carViewModel.NewCar = new Car
                    {
                        Name = nameTextBox.Text,
                        ClientId = SelectedClient.Id
                    };
                }
                else
                {
                    _carViewModel.NewCar = new Car
                    {
                        Name = nameTextBox.Text
                    };
                }
                _carViewModel.AddCommand.Execute(_carViewModel);
            }

            this.Close();
        }

        private void clientsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(clientsComboBox.SelectedItem != null)
            {
                SelectedClient = clients.FirstOrDefault(c => c.Name == clientsComboBox.SelectedValue.ToString());
            }
            else
            {
                SelectedClient = null;
            }

            
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            clientsComboBox.SelectedIndex = -1;
        }
    }
}
