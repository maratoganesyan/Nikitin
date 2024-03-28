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
    /// Interaction logic for LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {
        public LoadingWindow()
        {
            InitializeComponent();
        }

        private async void LoadDb()
        {
            try
            {
                var employees = await Task.Run(() => DbUtils.db.Employees.ToList());
                new AuthWindow().Show();
                this.Close();

            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Ошибка подключения к базе данных. Попробуйте перезапустить приложение");
                Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDb();
        }
    }
}
