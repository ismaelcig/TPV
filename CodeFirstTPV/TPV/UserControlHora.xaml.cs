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
using System.Windows.Threading;

namespace TPV
{
    /// <summary>
    /// Lógica de interacción para UserControlHora.xaml
    /// </summary>
    public partial class UserControlHora : UserControl
    {
        public UserControlHora()
        {
            InitializeComponent();
            DispatcherTimer dtClockTime = new DispatcherTimer();

            dtClockTime.Interval = new TimeSpan(0, 0, 1);
            dtClockTime.Tick += dtClockTime_Tick;

            dtClockTime.Start();
        }
        private void dtClockTime_Tick(object sender, EventArgs e)
        {
            lblClockTime.Content = DateTime.Now.ToLongTimeString();
        }
    }
}
