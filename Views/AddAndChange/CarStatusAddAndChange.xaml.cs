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
    public partial class CarStatusAddAndChange : Window
    {
        bool _changeMode;

        int id;
        public CarStatusAddAndChange()
        {
            InitializeComponent();
            id = -1;
            _changeMode = false;
        }

        public CarStatusAddAndChange(CarStatus carStatus)
        {
            InitializeComponent();
            id = carStatus.IdCarStatus;
            carStatusNameTextBox.Text = carStatus.CarStatusName;

            _changeMode = true;
        }

        private bool Validation()
        {
            if (carStatusNameTextBox.Text.Length == 0)
            {
                CustomMessageBox.Show("Поля не заполнены");
                return false;
            }
            if (DbUtils.db.CarStatuses.ToList().Any(ce => ce.CarStatusName == carStatusNameTextBox.Text && ce.IdCarStatus!= id))
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
                CarStatus carStatus;
                if (_changeMode)
                    carStatus = DbUtils.db.CarStatuses.FirstOrDefault(c => c.IdCarStatus == id);
                else
                    carStatus = new CarStatus();

                carStatus.CarStatusName = carStatusNameTextBox.Text;

                if (!_changeMode)
                    DbUtils.db.CarStatuses.Add(carStatus);

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
