using Nikitin.GenerationTools;
using Nikitin.GenerationTools.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nikitin.Views.Pages
{
    /// <summary>
    /// Interaction logic for ReportsPage.xaml
    /// </summary>
    public partial class ReportsPage : Page
    {
        public ReportsPage()
        {
            InitializeComponent();
            DriverComboBox.ItemsSource = DbUtils.db.Employees.ToList().Where(p => p.IdRole == DbUtils.Roles.Driver).ToList();
        }

        private async void GenerateDriverButton_Click(object sender, RoutedEventArgs e)
        {
            if(StartDateDriver.SelectedDate == null)
            {
                CustomMessageBox.Show("Начальная дата не выбрана");
                return;
            }
            if (EndDateDriver.SelectedDate == null)
            {
                CustomMessageBox.Show("Конечная дата не выбрана");
                return;
            }
            if (DriverComboBox.SelectedItem == null)
            {
                CustomMessageBox.Show("Водитель не выбрана");
                return;
            }
            var driver = DriverComboBox.SelectedItem as Employee;
            List<Request> requests = new List<Request>();
            foreach(var car in driver.Cars)
            {
                requests.AddRange(car.Requests.Where(p => p.RequestDate >= StartDateDriver.SelectedDate.Value &&
                                                          p.RequestDate <= EndDateDriver.SelectedDate.Value));
            }
            if (requests.Count() == 0)
            {
                CustomMessageBox.Show("В этот период у водителя не было заказов");
                return;
            }
            Driver driver1 = new Driver(driver, requests, StartDateDriver.SelectedDate.Value, EndDateDriver.SelectedDate.Value);
            Load.ShowAsync();
            await ReportGeneration.DoADriverReportAsync(driver1);
            Load.Hide();
        }

        private async void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            if (StartDate.SelectedDate == null)
            {
                CustomMessageBox.Show("Начальная дата не выбрана");
                return;
            }
            if (EndDate.SelectedDate == null)
            {
                CustomMessageBox.Show("Конечная дата не выбрана");
                return;
            }

            List<Request> requests = DbUtils.db.Requests.ToList().Where(p => p.RequestDate >= StartDate.SelectedDate.Value &&
                                                          p.RequestDate <= EndDate.SelectedDate.Value).ToList();
            if (requests.Count() == 0)
            {
                CustomMessageBox.Show("В этот период не было заказов");
                return;
            }

            RequestsModel model = new RequestsModel(requests, StartDate.SelectedDate.Value, EndDate.SelectedDate.Value);
            Load.ShowAsync();
            await ReportGeneration.DoARequestReportAsync(model);
            Load.Hide();
        }
    }
}
