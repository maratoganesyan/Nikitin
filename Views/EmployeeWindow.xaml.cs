using ModernWpf.Controls;
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

namespace Nikitin.Views
{
    /// <summary>
    /// Interaction logic for EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        public EmployeeWindow(Employee employee)
        {
            InitializeComponent();
            Global.CurrentEmployee = employee;
            EmployeeInformation.Text = $"{employee.Surname} {employee.Name} ({employee.IdRoleNavigation.RoleName})";
            SetAccess(employee);
        }

        private void SetAccess(Employee employee)
        {
            if(employee.IdRole == DbUtils.Roles.Operator)
            {
                SprNVI.Visibility = Visibility.Collapsed;
                EmployeeNVI.Visibility = Visibility.Collapsed;
                ReportsNVI.Visibility = Visibility.Collapsed;
            }
            if(employee.IdRole == DbUtils.Roles.Director)
            {
                SprNVI.Visibility = Visibility.Collapsed;
                RequestsNVI.Visibility = Visibility.Collapsed;
            }
            if(employee.IdRole == DbUtils.Roles.Operator) 
            {
                SprNVI.Visibility = Visibility.Collapsed;
                EmployeeNVI.Visibility = Visibility.Collapsed;
                ReportsNVI.Visibility = Visibility.Collapsed;
                CarsNVI.Visibility= Visibility.Collapsed;
            }
            if (employee.IdRole == DbUtils.Roles.Driver)
            {
                SprNVI.Visibility = Visibility.Collapsed;
                EmployeeNVI.Visibility = Visibility.Collapsed;
                ReportsNVI.Visibility = Visibility.Collapsed;
                CarsNVI.Visibility = Visibility.Collapsed;
            }
        }

        private void NavigationView_SelectionChanged(ModernWpf.Controls.NavigationView sender, ModernWpf.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            NavigationViewItem item = args.SelectedItem as NavigationViewItem;
            if (item.Tag is Type pageType && typeof(System.Windows.Controls.Page).IsAssignableFrom(pageType))
            {
                MainContent.Content = (System.Windows.Controls.Page)Activator.CreateInstance(pageType);
            }
            else if (item.Tag != null)
            {
                AuthWindow window = new AuthWindow();
                window.Show();
                this.Close();
            }
        }
    }
}
