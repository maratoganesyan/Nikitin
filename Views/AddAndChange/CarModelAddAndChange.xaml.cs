using Nikitin.Models;
using Nikitin.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    public partial class CarModelAddAndChange : Window
    {
        bool _changeMode;

        int id;
        public CarModelAddAndChange()
        {
            InitializeComponent();
            id = -1;
            _changeMode = false;
            BrandsComboBox.ItemsSource = DbUtils.db.Brands.ToList();
        }

        public CarModelAddAndChange(CarModel model)
        {
            InitializeComponent();
            id = model.IdCarModel;
            ModelTextBox.Text = model.ModelName;
            BrandsComboBox.ItemsSource = DbUtils.db.Brands.ToList();
            BrandsComboBox.SelectedItem = model.IdBrandNavigation;
            _changeMode = true;
        }

        private bool Validation()
        {
            if (ModelTextBox.Text.Length == 0)
            {
                CustomMessageBox.Show("Поля не заполнены");
                return false;
            }
            if(BrandsComboBox.SelectedItem == null)
            {
                CustomMessageBox.Show("Марка не выбрана");
                return false;
            }
            if (DbUtils.db.CarModels.ToList().Any(ce => ce.ModelName == ModelTextBox.Text && BrandsComboBox.SelectedItem == ce.IdBrandNavigation && ce.IdCarModel != id))
            {
                CustomMessageBox.Show("Такая запись уже есть в базе");
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
                CarModel carModel;
                if (_changeMode)
                    carModel = DbUtils.db.CarModels.FirstOrDefault(c => c.IdCarModel == id);
                else
                    carModel = new CarModel();

                carModel.ModelName = ModelTextBox.Text;
                carModel.IdBrand = (BrandsComboBox.SelectedItem as Brand).IdBrand;

                if (!_changeMode)
                    DbUtils.db.CarModels.Add(carModel);

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
