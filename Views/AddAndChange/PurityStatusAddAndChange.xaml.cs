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
    public partial class PurityStatusAddAndChange : Window
    {
        bool _changeMode;

        int id;
        public PurityStatusAddAndChange()
        {
            InitializeComponent();
            id = -1;
            _changeMode = false;
        }

        public PurityStatusAddAndChange(PurityStatus purityStatus)
        {
            InitializeComponent();
            id = purityStatus.IdPurityStatus;
            PurityStatusNameTextBox.Text = purityStatus.PurityStatusName;

            _changeMode = true;
        }

        private bool Validation()
        {
            if (PurityStatusNameTextBox.Text.Length == 0)
            {
                CustomMessageBox.Show("Поля не заполнены");
                return false;
            }
            if (DbUtils.db.PurityStatuses.ToList().Any(ce => ce.PurityStatusName == PurityStatusNameTextBox.Text && ce.IdPurityStatus != id))
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
                PurityStatus purityStatus;
                if (_changeMode)
                    purityStatus = DbUtils.db.PurityStatuses.FirstOrDefault(c => c.IdPurityStatus == id);
                else
                    purityStatus = new PurityStatus();

                purityStatus.PurityStatusName = PurityStatusNameTextBox.Text;

                if (!_changeMode)
                    DbUtils.db.PurityStatuses.Add(purityStatus);

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
