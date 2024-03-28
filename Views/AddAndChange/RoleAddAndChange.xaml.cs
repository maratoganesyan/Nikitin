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
    public partial class RoleAddAndChange : Window
    {
        bool _changeMode;

        int id;
        public RoleAddAndChange()
        {
            InitializeComponent();
            id = -1;
            _changeMode = false;
        }

        public RoleAddAndChange(Role role)
        {
            InitializeComponent();
            id = role.IdRole;
            RoleNameTextBox.Text = role.RoleName;

            _changeMode = true;
        }

        private bool Validation()
        {
            if (RoleNameTextBox.Text.Length == 0)
            {
                CustomMessageBox.Show("Поля не заполнены");
                return false;
            }
            if (DbUtils.db.Roles.ToList().Any(ce => ce.RoleName == RoleNameTextBox.Text && ce.IdRole != id))
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
                Role role;
                if (_changeMode)
                    role = DbUtils.db.Roles.FirstOrDefault(c => c.IdRole == id);
                else
                    role = new Role();

                role.RoleName = RoleNameTextBox.Text;

                if (!_changeMode)
                    DbUtils.db.Roles.Add(role);

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
