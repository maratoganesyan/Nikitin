using Nikitin.Models;
using Nikitin.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Nikitin.Views.AddAndChange
{
    /// <summary>
    /// Interaction logic for CarsAddAndChange.xaml
    /// </summary>
    public partial class CarsAddAndChange : Window
    {
        bool _changeMode;

        int id;

        public CarsAddAndChange()
        {
            InitializeComponent();
            Init();
            _changeMode = false;
            id = -1;
        }

        public CarsAddAndChange(Car car)
        {
            InitializeComponent();
            Init();
            _changeMode = true;
            id = car.IdCar;
            DriverComboBox.SelectedItem = car.IdDriverNavigation;
            ModelComboBox.SelectedItem = car.IdCarModelNavigation;
            CarStatusComboBox.SelectedItem = car.IdCarStatusNavigation;
            TransmissionComboBox.SelectedItem = car.IdTransmissionNavigation;
            FuelTypeComboBox.SelectedItem = car.IdFuelTypeNavigation;
            DriveTypeComboBox.SelectedItem = car.IdDriveTypeNavigation;
        }

        private void Init()
        {
            DriverComboBox.ItemsSource = DbUtils.db.Employees.ToList().Where(p => p.IdRole == DbUtils.Roles.Driver);
            ModelComboBox.ItemsSource = DbUtils.db.CarModels.ToList();
            CarStatusComboBox.ItemsSource = DbUtils.db.CarStatuses.ToList();
            TransmissionComboBox.ItemsSource = DbUtils.GetTableAllValues<Transmission>();
            FuelTypeComboBox.ItemsSource = DbUtils.GetTableAllValues<FuelType>();
            DriveTypeComboBox.ItemsSource = DbUtils.GetTableAllValues<DriveType>();
        }

        private bool Validation()
        {
            if(StateNumber.Text.Length == 0)
            {
                CustomMessageBox.Show("Номер автомобиля не введен");
                return false;
            }
            if (DriverComboBox.SelectedItem == null)
            {
                CustomMessageBox.Show("Водитель не выбран");
                return false;
            }
            if (ModelComboBox.SelectedItem == null)
            {
                CustomMessageBox.Show("Модель не выбрана");
                return false;
            }
            if (CarStatusComboBox.SelectedItem == null)
            {
                CustomMessageBox.Show("Статус не выбран");
                return false;
            }
            if (TransmissionComboBox.SelectedItem == null)
            {
                CustomMessageBox.Show("коробка передач не выбрана");
                return false;
            }
            if (FuelTypeComboBox.SelectedItem == null)
            {
                CustomMessageBox.Show("Тип топлива не выбран");
                return false;
            }
            if (DriveTypeComboBox.SelectedItem == null)
            {
                CustomMessageBox.Show("Привод не выбран");
                return false;
            }
            return true;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Validation())
                    return;
                Car car;
                if (_changeMode)
                    car = DbUtils.db.Cars.FirstOrDefault(c => c.IdCar == id);
                else
                    car = new Car();

                car.StateNumber = StateNumber.Text;
                car.IdDriver = (DriverComboBox.SelectedItem as Employee).IdEmployee;
                car.IdCarModel = (ModelComboBox.SelectedItem as CarModel).IdCarModel;
                car.IdCarStatus = (CarStatusComboBox.SelectedItem as CarStatus).IdCarStatus;
                car.IdDriveType = (DriveTypeComboBox.SelectedItem as DriveType).IdDriveType;
                car.IdTransmission = (TransmissionComboBox.SelectedItem as Transmission).IdTransmission;
                car.IdFuelType = (FuelTypeComboBox.SelectedItem as FuelType).IdFuelType;

                if (!_changeMode)
                    DbUtils.db.Cars.Add(car);

                DbUtils.db.SaveChanges();
                Close();

            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Ошибка подключения к базе данных");
            }
        }
    }
}
