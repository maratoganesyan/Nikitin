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
    /// Interaction logic for EmployeeAddAndChange.xaml
    /// </summary>
    public partial class EmployeeAddAndChange : Window
    {
        bool _changeMode;

        int id;

        public EmployeeAddAndChange()
        {
            InitializeComponent();
            _changeMode = false;
            if (Global.CurrentEmployee.IdRole == DbUtils.Roles.Director)
                RoleComboBox.ItemsSource = DbUtils.db.Roles.ToList().Where(r => r.IdRole != DbUtils.Roles.Admin && r.IdRole != DbUtils.Roles.Director);
            else
                RoleComboBox.ItemsSource = DbUtils.db.Roles.ToList();
            id = -1;
        }

        public EmployeeAddAndChange(Employee employee)
        {
            InitializeComponent();
            _changeMode = true;
            id = employee.IdEmployee;
            SurnameTextBox.Text = employee.Surname;
            NameTextBox.Text = employee.Name;
            PatronymicTextBox.Text = employee.Patronymic;
            PhoneNumberTextBox.Text = employee.PhoneNumber;
            LoginTextBox.Text = employee.Login;
            PasswordTextBox.Text = employee.Password;
            if (Global.CurrentEmployee.IdRole == DbUtils.Roles.Director)
                RoleComboBox.ItemsSource = DbUtils.db.Roles.ToList().Where(r => r.IdRole != DbUtils.Roles.Admin && r.IdRole != DbUtils.Roles.Director);
            else
                RoleComboBox.ItemsSource = DbUtils.db.Roles.ToList();
            RoleComboBox.SelectedItem = employee.IdRoleNavigation;

        }

        private bool Validation()
        {
            if (SurnameTextBox.Text.Length == 0)
            {
                CustomMessageBox.Show("Фамилия не заполнена");
                return false;
            }
            if (NameTextBox.Text.Length == 0)
            {
                CustomMessageBox.Show("Имя не заполнена");
                return false;
            }
            if (PatronymicTextBox.Text.Length == 0)
            {
                CustomMessageBox.Show("Отчество не заполнено");
                return false;
            }
            if (PhoneNumberTextBox.Text.Contains('_'))
            {
                CustomMessageBox.Show("Номер телефона не заполнен или заполнен в неправильном формате");
                return false;
            }
            if (LoginTextBox.Text.Length < 8)
            {
                CustomMessageBox.Show("В логине должно быть от 8 символов");
                return false;
            }
            if (PasswordTextBox.Text.Length < 8)
            {
                CustomMessageBox.Show("В пароле должно быть от 8 символов");
                return false;
            }
            if (RoleComboBox.SelectedItem == null)
            {
                CustomMessageBox.Show("Роль не выбрана");
                return false;
            }
            if(DbUtils.db.Employees.Any(e => e.IdEmployee != id && LoginTextBox.Text == e.Login))
            {
                CustomMessageBox.Show("Пользователь с таким логином уже существует");
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
                Employee employee;
                if (_changeMode)
                    employee = DbUtils.db.Employees.FirstOrDefault(c => c.IdEmployee == id);
                else
                    employee = new Employee();

                employee.Surname = SurnameTextBox.Text;
                employee.Name = NameTextBox.Text;
                employee.Patronymic = PatronymicTextBox.Text;
                employee.PhoneNumber = _maskedPhone.Text;
                employee.Login = LoginTextBox.Text;
                employee.Password = PasswordTextBox.Text;
                employee.IdRole = (RoleComboBox.SelectedItem as Role).IdRole;

                if (!_changeMode)
                    DbUtils.db.Employees.Add(employee);

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
