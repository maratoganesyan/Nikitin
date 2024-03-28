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
    public partial class BrandAddAndChange : Window
    {
        bool _changeMode;

        int id;
        public BrandAddAndChange()
        {
            InitializeComponent();
            id = -1;
            _changeMode = false;
        }

        public BrandAddAndChange(Brand brand)
        {
            InitializeComponent();
            id = brand.IdBrand;
            BrandNameTextBox.Text = brand.BrandName;

            _changeMode = true;
        }

        private bool Validation()
        {
            if (BrandNameTextBox.Text.Length == 0)
            {
                CustomMessageBox.Show("Поля не заполнены");
                return false;
            }
            if (DbUtils.db.Brands.ToList().Any(ce => ce.BrandName == BrandNameTextBox.Text && ce.IdBrand != id))
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
                Brand brand;
                if (_changeMode)
                    brand = DbUtils.db.Brands.FirstOrDefault(c => c.IdBrand == id);
                else
                    brand = new Brand();

                brand.BrandName = BrandNameTextBox.Text;

                if (!_changeMode)
                    DbUtils.db.Brands.Add(brand);

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
