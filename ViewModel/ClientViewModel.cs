using CarShop.Commands;
using CarShop.Data;
using CarShop.Models;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace CarShop.ViewModel
{
    public class ClientViewModel : INotifyPropertyChanged
    {
        private readonly CarShopContext _carShopContext = new CarShopContext();

        public Client NewClient { get; set; }
        public Client EditedClient { get; set; }

        public ClientViewModel()
        {
            GetClients();
        }

        private ObservableCollection<Client> clients;
        public ObservableCollection<Client> Clients
        {
            get { return clients; }
            set
            {
                if (clients != value)
                {
                    clients = value;
                    OnPropertyChanged(nameof(Clients));
                }
            }
        }

        private Client _selectedClient;
        public Client SelectedClient
        {
            get { return _selectedClient; }
            set
            {
                if (_selectedClient != value)
                {
                    _selectedClient = value;
                    OnPropertyChanged(nameof(SelectedClient));
                }
            }
        }

        public ICommand AddCommand => new RelayCommand(AddClient);
        public ICommand UpdateCommand => new RelayCommand(UpdateClient, CanUpdateClient);
        public ICommand DeleteCommand => new RelayCommand(DeleteClient, CanDeleteClient);

        
        private void GetClients()
        {
            Clients = new ObservableCollection<Client>();

            using (SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")) 
            {
                connection.Open();

                string sql = "SELECT Id, Name FROM Clients";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int clientId = reader.GetInt32(0);
                            string clientName = reader.GetString(1);

                            Client client = new Client
                            {
                                Id = clientId,
                                Name = clientName
                            };

                            Clients.Add(client);
                        }
                    }
                }
            }
        }


        private void AddClient()
        {
            _carShopContext.Clients.Add(NewClient);
            _carShopContext.SaveChanges();
            GetClients();
        }

        private void UpdateClient()
        {

            _carShopContext.ChangeTracker.Clear();

            _carShopContext.Clients.Update(EditedClient);
            _carShopContext.SaveChanges();
            GetClients();
        }

        private void DeleteClient()
        {
            _carShopContext.ChangeTracker.Clear();

            if (SelectedClient != null)
            {

                _carShopContext.Clients.Remove(SelectedClient);
                _carShopContext.SaveChanges();
                GetClients();
            }
        }

        private bool CanUpdateClient() => SelectedClient != null;
        private bool CanDeleteClient() => SelectedClient != null;



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }

}
