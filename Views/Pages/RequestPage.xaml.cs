
using Microsoft.Maps.MapControl.WPF;
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
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GMap.NET.WindowsPresentation;
using System.Net;
using Nikitin.Models;

namespace Nikitin.Views.Pages
{
    /// <summary>
    /// Interaction logic for RequestPage.xaml
    /// </summary>
    public partial class RequestPage : Page
    {
        Dictionary<int, SolidColorBrush> colors = new Dictionary<int, SolidColorBrush>();
        
        LocationCollection locations = new LocationCollection();

        Point currentPoint;

        int id;

        public RequestPage()
        {
            InitializeComponent();
            colors.Add(1, Brushes.Blue);
            colors.Add(2, Brushes.Green);
            colors.Add(3, Brushes.Red);
            PurityStatusComboBox.ItemsSource = DbUtils.GetTableAllValues<PurityStatus>();
            RequestStatusComboBox.ItemsSource = DbUtils.GetTableAllValues<RequestStatus>();
            CarsComboBox.ItemsSource = DbUtils.GetTableAllValues<Car>().Where(c => c.IdCarStatus != DbUtils.CarStatuses.Remont);
            if(Global.CurrentEmployee.IdRole == DbUtils.Roles.Driver)
            {
                DateTimePicker.IsEnabled = false;
                CarsComboBox.SelectedItem = Global.CurrentEmployee;
                CarsComboBox.IsEnabled = false;
            }
        }

        private void AddRouts()
        {
            myMap.Children.Clear();
            var requests = DbUtils.db.Requests.ToList();
            if (Global.CurrentEmployee.IdRole == DbUtils.Roles.Driver)
                requests = DbUtils.db.Requests.Where(p => p.IdCarNavigation.IdDriverNavigation == Global.CurrentEmployee).ToList();
            foreach(var r in requests)
            {
                MapPolyline polyline = new MapPolyline();
                polyline.Stroke = colors[r.IdRequestStatus];
                polyline.StrokeThickness = 5;
                polyline.Opacity = 0.7;
                polyline.Locations = new LocationCollection() {
                    new Location(Convert.ToDouble(r.FirstPointLng), Convert.ToDouble(r.FirstPointLat)),
                    new Location(Convert.ToDouble(r.SecondPointLng),Convert.ToDouble(r.SecondPintLat)) };
                polyline.ToolTip = $"{r.IdRequestStatusNavigation.RequestStatusName}\n{r.IdPurityStatusNavigation.PurityStatusName}\n{r.RequestDate:dd.MM.yy}";
                polyline.Tag = r.IdRequest;
                polyline.MouseDown += Polyline_MouseDown;

                myMap.Children.Add(polyline);
            }

           
        }

        private void Polyline_MouseDown(object sender, MouseButtonEventArgs e)
        {
            id = (int)((sender as MapPolyline).Tag);
            Request r = DbUtils.db.Requests.FirstOrDefault(c => c.IdRequest == id);
            locations.Clear();
            locations.Add(new Location(Convert.ToDouble(r.FirstPointLng), Convert.ToDouble(r.FirstPointLat)));
            locations.Add(new Location(Convert.ToDouble(r.SecondPointLng), Convert.ToDouble(r.SecondPintLat)));
            AddAndChange.ShowAsync();
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (myMap.TryViewportPointToLocation(currentPoint, out Location location))
            {
                locations.Add(location);
                if (locations.Count == 2)
                {
                    MapPolyline polyline = new MapPolyline();
                    polyline.Stroke = Brushes.Gray;
                    polyline.StrokeThickness = 5;
                    polyline.Opacity = 0.7;
                    polyline.Locations = locations;
                    myMap.Children.Add(polyline);
                    //locations.Clear();
                    id = -1;
                    DateTimePicker.Value = DateTime.Now;
                    PurityStatusComboBox.SelectedItem = null;
                    RequestStatusComboBox.SelectedItem = null;
                    CarsComboBox.SelectedItem = null;
                    AddAndChange.ShowAsync();
                }
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void myMap_Loaded(object sender, RoutedEventArgs e)
        {
            AddRouts();
            if (Global.CurrentEmployee.IdRole == DbUtils.Roles.Driver)
                Context.Visibility = Visibility.Collapsed;
        }

        private void myMap_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            currentPoint = e.GetPosition(myMap);
           
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            AddAndChange.Hide();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Request request;
                if(id != -1)
                    request = DbUtils.db.Requests.FirstOrDefault(p => p.IdRequest == id);
                else
                    request = new Request();

                request.RequestDate = DateTimePicker.Value.Value;
                request.IdCar = (CarsComboBox.SelectedItem as Car).IdCar;
                request.IdPurityStatus = (PurityStatusComboBox.SelectedItem as PurityStatus).IdPurityStatus;
                request.IdRequestStatus = (RequestStatusComboBox.SelectedItem as RequestStatus).IdRequestStatus;
                request.FirstPointLat = Math.Round(Convert.ToDecimal(locations[0].Longitude), 6);
                request.FirstPointLng = Math.Round(Convert.ToDecimal(locations[0].Latitude), 6);
                request.SecondPintLat= Math.Round(Convert.ToDecimal(locations[1].Longitude), 6);
                request.SecondPointLng = Math.Round(Convert.ToDecimal(locations[1].Latitude), 6);
                setCarStatus(request.IdCar, request.IdRequestStatus);

                if(id == -1)
                    DbUtils.db.Requests.Add(request);
                DbUtils.SaveChanges();
                AddAndChange.Hide();
            }
            catch(Exception ex)
            {
                CustomMessageBox.Show("Ошибка подключения к базе данных");
            }
        }

        private void setCarStatus(int IdCar, int IdRequestStatus)
        {
            Car car = DbUtils.db.Cars.FirstOrDefault(c => c.IdCar == IdCar);
            if (IdRequestStatus == DbUtils.RequestStatuses.InPlan)
                car.IdCarStatus = DbUtils.CarStatuses.Free;
            else if (IdRequestStatus == DbUtils.RequestStatuses.InProcess)
                car.IdCarStatus = DbUtils.RequestStatuses.InProcess;
            else if (IdRequestStatus == DbUtils.RequestStatuses.Done)
                car.IdCarStatus = DbUtils.CarStatuses.Free;

        }

        private void AddAndChange_Opened(ModernWpf.Controls.ContentDialog sender, ModernWpf.Controls.ContentDialogOpenedEventArgs args)
        {
            if(id != -1)
            {
                DateTimePicker.Value = DbUtils.db.Requests.First(r => r.IdRequest == id).RequestDate;
                PurityStatusComboBox.SelectedItem = DbUtils.db.Requests.First(r => r.IdRequest == id).IdPurityStatusNavigation;
                RequestStatusComboBox.SelectedItem = DbUtils.db.Requests.First(r => r.IdRequest == id).IdRequestStatusNavigation;
                CarsComboBox.SelectedItem = DbUtils.db.Requests.First(r => r.IdRequest == id).IdCarNavigation;
            }
        }

        private void AddAndChange_Closed(ModernWpf.Controls.ContentDialog sender, ModernWpf.Controls.ContentDialogClosedEventArgs args)
        {
            locations.Clear();
            AddRouts();
        }
    }
}
