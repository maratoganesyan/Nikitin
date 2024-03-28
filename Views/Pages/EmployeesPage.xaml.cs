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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nikitin.Views.Pages
{
    /// <summary>
    /// Interaction logic for EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : Page
    {
        public EmployeesPage()
        {
            InitializeComponent();
            if (Global.CurrentEmployee.IdRole == DbUtils.Roles.Director)
                EmployeesDataGrid.ItemsSource = DbUtils.db.Employees.ToList().Where(p => p.IdRole != DbUtils.Roles.Admin && p.IdRole != DbUtils.Roles.Director);
            else
                EmployeesDataGrid.ItemsSource = DbUtils.db.Employees.ToList();
        }

        private void SearchTextBox_QuerySubmitted(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (Global.CurrentEmployee.IdRole == DbUtils.Roles.Director)
                EmployeesDataGrid.ItemsSource = DbUtils.db.Employees.ToList()
                    .Where(p => p.IdRole != DbUtils.Roles.Admin && p.IdRole != DbUtils.Roles.Director && p.ToString().Contains(SearchTextBox.Text));
            else
                EmployeesDataGrid.ItemsSource = DbUtils.db.Employees.ToList().Where(p => p.ToString().Contains(SearchTextBox.Text));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            if (Global.CurrentEmployee.IdRole == DbUtils.Roles.Director)
                EmployeesDataGrid.ItemsSource = DbUtils.db.Employees.ToList().Where(p => p.IdRole != DbUtils.Roles.Admin && p.IdRole != DbUtils.Roles.Director);
            else
                EmployeesDataGrid.ItemsSource = DbUtils.db.Employees.ToList();
        }
    }
}
