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
    public partial class TransmissionAddAndChange : Window
    {
        bool _changeMode;

        int id;
        public TransmissionAddAndChange()
        {
            InitializeComponent();
            id = -1;
            _changeMode = false;
        }

        public TransmissionAddAndChange(Transmission transmission)
        {
            InitializeComponent();
            id = transmission.IdTransmission;
            TransmissionNameTextBox.Text = transmission.TransmissionName;

            _changeMode = true;
        }

        private bool Validation()
        {
            if (TransmissionNameTextBox.Text.Length == 0)
            {
                CustomMessageBox.Show("Поля не заполнены");
                return false;
            }
            if (DbUtils.db.Transmissions.ToList().Any(ce => ce.TransmissionName == TransmissionNameTextBox.Text && ce.IdTransmission != id))
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
                Transmission transmission;
                if (_changeMode)
                    transmission = DbUtils.db.Transmissions.FirstOrDefault(c => c.IdTransmission == id);
                else
                    transmission = new Transmission();

                transmission.TransmissionName = TransmissionNameTextBox.Text;

                if (!_changeMode)
                    DbUtils.db.Transmissions.Add(transmission);

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
