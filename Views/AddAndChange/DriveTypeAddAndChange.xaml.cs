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
    public partial class DriveTypeAddAndChange : Window
    {
        bool _changeMode;

        int id;
        public DriveTypeAddAndChange()
        {
            InitializeComponent();
            id = -1;
            _changeMode = false;
        }

        public DriveTypeAddAndChange(DriveType driveType)
        {
            InitializeComponent();
            id = driveType.IdDriveType;
            DriveTypeNameTextBox.Text = driveType.DriveTypeName;

            _changeMode = true;
        }

        private bool Validation()
        {
            if (DriveTypeNameTextBox.Text.Length == 0)
            {
                CustomMessageBox.Show("Поля не заполнены");
                return false;
            }
            if (DbUtils.db.DriveTypes.ToList().Any(ce => ce.DriveTypeName == DriveTypeNameTextBox.Text && ce.IdDriveType != id))
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
                DriveType driveType;
                if (_changeMode)
                    driveType = DbUtils.db.DriveTypes.FirstOrDefault(c => c.IdDriveType == id);
                else
                    driveType = new DriveType();

                driveType.DriveTypeName = DriveTypeNameTextBox.Text;

                if (!_changeMode)
                    DbUtils.db.DriveTypes.Add(driveType);

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
