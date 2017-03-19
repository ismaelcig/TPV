using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TPV.Model;

namespace TPV.Windows
{
    /// <summary>
    /// Lógica de interacción para Factura.xaml
    /// </summary>
    public partial class Factura : Window
    {
        public double size;

        public Factura()
        {
            InitializeComponent();
        }



        public void Start()
        {
            lblFecha.Content = "Fecha: " + DateTime.Today.ToShortDateString();
            lblHora.Content = "Hora: " + DateTime.Now.ToLongTimeString();
            dataGrid.ItemsSource = UserWindow.carro;

            //Resize datagrid
            size = 0;
            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                size += dataGrid.RowHeight;
            }
            dataGrid.Height = size;
        }
        
        public void Met()
        {
            Label lbl = new Label();
            lbl.Content = "Total: " + UserWindow.tot + "€";

            lbl.Margin = new Thickness(150, 160 + dataGrid.ActualHeight, 0, 0);
            gridGlobal.Children.Add(lbl);

            double pbase = 0;
            foreach (LineaVenta l in UserWindow.carro)
            {
                pbase += l.cantidad * (Convert.ToDouble(l.precioTotal) - Convert.ToDouble(l.precioTotal) * (l.Producto.descuento / 100));
            }
            double descEfectivo = pbase * UserWindow.descTarjeta / 100;

            lblBase.Content = pbase + "€";
            lbls(lblBase);

            lblPDesc.Content = UserWindow.descTarjeta + "%";
            lbls(lblPDesc);

            lblDesc.Content = descEfectivo + "€";
            lbls(lblDesc);
            //lblPIva.Content = "21%";
            //lblIva.Content = preIva * 0.21;
            lblSubtotal.Content = pbase - descEfectivo + "€";
            lbls(lblSubtotal);
            lbls(label15);
            lbls(label16);
            lbls(label2);
            lbls(label3);
            lbls(label13);
            lbls(label14);
        }

        void lbls(Label l)
        {
            Thickness t = new Thickness();
            t = l.Margin;
            t.Top = t.Top + dataGrid.ActualHeight - 40;
            l.Margin = t;
        }
        
        
    }
}
