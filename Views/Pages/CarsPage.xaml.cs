using Nikitin.Models;
using Nikitin.Tools;
using Nikitin.Views.AddAndChange;
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
    /// Interaction logic for CarsPage.xaml
    /// </summary>
    public partial class CarsPage : Page
    {
        public CarsPage()
        {
            InitializeComponent();
            CarsDataGrid.ItemsSource = DbUtils.GetTableAllValues<Car>();
        }

        private void SearchTextBox_QuerySubmitted(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            CarsDataGrid.ItemsSource = DbUtils.GetSearchingValues<Car>(SearchTextBox.Text);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new CarsAddAndChange().ShowDialog();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            CarsDataGrid.ItemsSource = DbUtils.GetTableAllValues<Car>();
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            var car = (sender as Button).DataContext as Car;
            new CarsAddAndChange(car).ShowDialog();
        }
    }
}
