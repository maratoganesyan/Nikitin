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
    public partial class RequestStatusAddAndChange : Window
    {
        bool _changeMode;

        int id;
        public RequestStatusAddAndChange()
        {
            InitializeComponent();
            id = -1;
            _changeMode = false;
        }

        public RequestStatusAddAndChange(RequestStatus requestStatus)
        {
            InitializeComponent();
            id = requestStatus.IdRequestStatus;
            RequestStatusNameTextBox.Text = requestStatus.RequestStatusName;

            _changeMode = true;
        }

        private bool Validation()
        {
            if (RequestStatusNameTextBox.Text.Length == 0)
            {
                CustomMessageBox.Show("Поля не заполнены");
                return false;
            }
            if (DbUtils.db.RequestStatuses.ToList().Any(ce => ce.RequestStatusName == RequestStatusNameTextBox.Text && ce.IdRequestStatus != id))
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
                RequestStatus requestStatus;
                if (_changeMode)
                    requestStatus = DbUtils.db.RequestStatuses.FirstOrDefault(c => c.IdRequestStatus == id);
                else
                    requestStatus = new RequestStatus();

                requestStatus.RequestStatusName = RequestStatusNameTextBox.Text;

                if (!_changeMode)
                    DbUtils.db.RequestStatuses.Add(requestStatus);

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
