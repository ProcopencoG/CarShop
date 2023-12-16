using CarShop.Commands;
using CarShop.Data;
using CarShop.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace CarShop.ViewModel
{
    public class CarViewModel : INotifyPropertyChanged
    {
        private readonly CarShopContext _carShopContext = new CarShopContext();

        public Car NewCar { get; set; }
        public Car EditedCar { get; set; }

        public CarViewModel()
        {
            GetCars();
        }

        private ObservableCollection<Car> cars;
        public ObservableCollection<Car> Cars
        {
            get { return cars; }
            set
            {
                if (cars != value)
                {
                    cars = value;
                    OnPropertyChanged(nameof(Cars));
                }
            }
        }

        private Car _selectedCar;
        public Car SelectedCar
        {
            get { return _selectedCar; }
            set
            {
                if (_selectedCar != value)
                {
                    _selectedCar = value;
                    OnPropertyChanged(nameof(SelectedCar));
                }
            }
        }

        public ICommand AddCommand => new RelayCommand(AddCar);
        public ICommand UpdateCommand => new RelayCommand(UpdateCar, CanUpdateCar);
        public ICommand DeleteCommand => new RelayCommand(DeleteCar, CanDeleteCar);

        private void GetCars()
        {

            var carsWithClientNames = from car in _carShopContext.Cars
                                      join client in _carShopContext.Clients
                                      on car.ClientId equals client.Id into joinedData
                                      from client in joinedData.DefaultIfEmpty()
                                      select new
                                      {
                                          car.Id,
                                          car.Name,
                                          client
                                      };

            Cars = new ObservableCollection<Car>(carsWithClientNames.Select(c => new Car
            {
                Id = c.Id,
                Name = c.Name,
                Client = c.client
            }));


        }

        private void AddCar()
        {
            _carShopContext.Cars.Add(NewCar);
            _carShopContext.SaveChanges();
            GetCars();
        }

        private void UpdateCar()
        {

            _carShopContext.ChangeTracker.Clear();

            _carShopContext.Cars.Update(EditedCar);
            _carShopContext.SaveChanges();
            GetCars();
        }

        private void DeleteCar()
        {
            _carShopContext.ChangeTracker.Clear();

            if (SelectedCar != null)
            {

                _carShopContext.Cars.Remove(SelectedCar);
                _carShopContext.SaveChanges();
                GetCars();
            }
        }

        private bool CanUpdateCar() => SelectedCar != null;
        private bool CanDeleteCar() => SelectedCar != null;



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }

}
