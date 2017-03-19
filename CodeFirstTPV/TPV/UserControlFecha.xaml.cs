﻿using System;
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
using System.Windows.Threading;

namespace TPV.UserControls
{
    /// <summary>
    /// Lógica de interacción para Fecha.xaml
    /// </summary>
    public partial class Fecha : UserControl
    {
        public Fecha()
        {
            InitializeComponent();
            //DispatcherTimer dt = new DispatcherTimer();

            //dt.Interval = new TimeSpan(1, 0, 0, 0);
            //dt.Tick += Dt_Tick;
            lblDate.Content = DateTime.Today.ToShortDateString();
        }

        //private void Dt_Tick(object sender, EventArgs e)
        //{
        //    lblDate.Content = DateTime.Today.ToShortDateString();
        //}
    }
}
