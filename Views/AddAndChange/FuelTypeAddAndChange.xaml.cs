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
    public partial class FuelTypeAddAndChange : Window
    {
        bool _changeMode;

        int id;
        public FuelTypeAddAndChange()
        {
            InitializeComponent();
            id = -1;
            _changeMode = false;
        }

        public FuelTypeAddAndChange(FuelType fuelType)
        {
            InitializeComponent();
            id = fuelType.IdFuelType;
            FuelTypeNameTextBox.Text = fuelType.FuelTypeName;

            _changeMode = true;
        }

        private bool Validation()
        {
            if (FuelTypeNameTextBox.Text.Length == 0)
            {
                CustomMessageBox.Show("Поля не заполнены");
                return false;
            }
            if (DbUtils.db.FuelTypes.ToList().Any(ce => ce.FuelTypeName == FuelTypeNameTextBox.Text && ce.IdFuelType != id))
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
                FuelType fuelType;
                if (_changeMode)
                    fuelType = DbUtils.db.FuelTypes.FirstOrDefault(c => c.IdFuelType == id);
                else
                    fuelType = new FuelType();

                fuelType.FuelTypeName = FuelTypeNameTextBox.Text;

                if (!_changeMode)
                    DbUtils.db.FuelTypes.Add(fuelType);

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
