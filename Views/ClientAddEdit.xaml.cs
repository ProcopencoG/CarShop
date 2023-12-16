using CarShop.Models;
using CarShop.ViewModel;
using System.Windows;

namespace CarShop.Views
{
    /// <summary>
    /// Interaction logic for ClientAddEdit.xaml
    /// </summary>
    public partial class ClientAddEdit : Window
    {
        public Client editClient { get; set; }

        private ClientViewModel _clientViewModel { get; set; }
        public ClientAddEdit(ClientViewModel clientViewModel, Client clientToEdit = null)
        {
            InitializeComponent();
            _clientViewModel = clientViewModel;
            DataContext = this;

            if (clientToEdit != null )
            {
                editClient = clientToEdit;
                button.Content = "Edit";
                nameTextBox.Text = clientToEdit.Name;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if ( editClient != null )
            {
                editClient.Name = nameTextBox.Text;
                _clientViewModel.EditedClient = editClient;
                _clientViewModel.UpdateCommand.Execute(_clientViewModel);
            }
            else
            {
                _clientViewModel.NewClient = new Client()
                {
                    Name = nameTextBox.Text
                };
                _clientViewModel.AddCommand.Execute(_clientViewModel);
            }

            this.Close();
        }
    }
}
