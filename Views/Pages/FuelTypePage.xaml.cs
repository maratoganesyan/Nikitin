﻿using Nikitin.Models;
using Nikitin.ViewModels;
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
    /// Interaction logic for BrandPage.xaml
    /// </summary>
    public partial class FuelTypePage : Page
    {
        public FuelTypePage()
        {
            InitializeComponent();
            this.DataContext = new SomePagesViewModel<FuelType>();
        }
    }
}
