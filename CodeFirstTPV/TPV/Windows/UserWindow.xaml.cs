using System;
using System.Collections.Generic;
using System.IO;
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
using TPV.Model;

namespace TPV.Windows
{
    /// <summary>
    /// Lógica de interacción para UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public static List<LineaVenta> carro = new List<LineaVenta>();
        public Venta v = new Venta();
        public static int descTarjeta;
        public static double tot = 0;
        //public double iva;
        public UserWindow()
        {
            InitializeComponent();
            label.Content = "Usuario: " + MainWindow.user.nombre;
            v.Usuario = MainWindow.user;
            GridCats();
            //GridProds(MainWindow.u.ProductosRepository.GetAll());
            comboBoxSearch.Items.Add("ID");
            comboBoxSearch.Items.Add("Nombre");
            Calc(false);
            calcDel.IsEnabled = false;
            carro.Clear();
        }








        private void buttDesc_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Adiós, "+MainWindow.user.nombre);
            this.Close();
        }
        private void buttSearch_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxSearch.Text == "ID" && txtSearch.Text != "")
            {
                GridProds(MainWindow.u.ProductosRepository.Get(c => c.ProductoID.ToString().Contains(txtSearch.Text)));
            }
            else if (comboBoxSearch.Text == "Nombre" && txtSearch.Text != "")
            {
                GridProds(MainWindow.u.ProductosRepository.Get(c => c.nombre.ToString().Contains(txtSearch.Text)));
            }
            else
            {
                MessageBox.Show("Escriba su búsqueda, y el campo que desea encontrar");
            }
        }


        /*****************************************/
        //                   GRIDS               //
        /*****************************************/

        void GridCats()
        {
            spCat.Children.Clear();
            foreach (Categoria cat in MainWindow.u.CategoriasRepository.GetAll())
            {
                Button newBtn = new Button();

                newBtn.Content = cat.nombre;
                newBtn.Name = "cButton" + cat.CategoriaID.ToString();
                //newBtn.MaxWidth = 50;
                newBtn.MaxHeight = 50;
                newBtn.Margin = new Thickness(5, 5, 5, 5);
                newBtn.Click += NewBtn_Click;

                spCat.Children.Add(newBtn);
            }
            Button newBtn2 = new Button();

            newBtn2.Content = "Todos";
            newBtn2.Name = "cButtonAll";
            newBtn2.MaxHeight = 50;
            newBtn2.Margin = new Thickness(5, 5, 5, 5);
            newBtn2.Click += NewBtn2_Click;

            spCat.Children.Add(newBtn2);
        }

        private void NewBtn_Click(object sender, RoutedEventArgs e)
        {
            string content = (sender as Button).Content.ToString();
            GridProds(MainWindow.u.ProductosRepository.Get(c => c.Categoria.nombre == content));
        }

        private void NewBtn2_Click(object sender, RoutedEventArgs e)
        {
            GridProds(MainWindow.u.ProductosRepository.GetAll());
        }

        void GridProds(List<Producto> p)
        {
            spProd.Children.Clear();
            foreach (Producto prod in p)
            {
                Button prodBtn = new Button();
                StackPanel stack = new StackPanel();
                Image img = new Image();

                string url = Directory.GetCurrentDirectory();
                url = url + "\\Productos\\" + prod.imagen;
                img.Source = new BitmapImage(new Uri(url));
                img.Height = 110;
                img.Width = 90;

                stack.Orientation = Orientation.Vertical;
                stack.Children.Add(img);

                prodBtn.ToolTip = prod.nombre.ToUpper();
                prodBtn.Content = stack;
                prodBtn.Name = "pButton" + prod.ProductoID.ToString();
                

                prodBtn.Margin = new Thickness(5, 5, 5, 5);
                prodBtn.Click += ProdBtn_Click;

                spProd.Children.Add(prodBtn);
            }
        }

        private void ProdBtn_Click(object sender, RoutedEventArgs e)
        {//Añadir producto al carro
            //Console.WriteLine("Tooltip: " + (sender as Button).ToolTip.ToString());
            Producto p = new Producto();
            LineaVenta lv = new LineaVenta();
            double descontado = new double();
            double precioF = new double();
            bool añadido = false;

            string prod = (sender as Button).ToolTip.ToString();

            p = MainWindow.u.ProductosRepository.Single(c => c.nombre.ToUpper() == prod);
            descontado = p.precio * p.descuento;
            descontado = descontado / 100;
            precioF = p.precio - descontado;
            foreach (LineaVenta l in carro)
            {
                if (l.ProductoID == p.ProductoID)
                {//Si el producto ya ha sido añadido sólo le añade uno a la cantidad, y aumenta el precio total
                    AumentarCant(l, precioF);
                    añadido = true;
                }
            }


            if (!añadido)
            {//item no está, hay que añadirlo
                lv.ProductoID = p.ProductoID;
                lv.Producto = p;
                lv.Venta = v;
                lv.cantidad = 1;
                lv.precioTotal = precioF.ToString();

                carro.Add(lv);
            }
            Refrescar();
        }

        /**********************************************************/
        //                         MÉTODOS                        //
        /**********************************************************/
        void Refrescar()
        {
            int select = dataGrid.SelectedIndex;
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = carro;
            dataGrid.SelectedIndex = select;

            int art = 0;
            tot = 0;
            foreach (LineaVenta item in carro)
            {
                art = art + item.cantidad;
                tot = tot + Convert.ToDouble(item.precioTotal);
            }
            lblArt.Content = "Artículos: " + art;
            CalcTotal();
        }
        void CalcTotal()
        {
            //iva = tot * 21 / 100;
            //tot = tot - iva;
            lblTotal.Content = "Total: " + tot + "€";
        }
        void AumentarCant(LineaVenta l, double precioF)
        {
            l.cantidad++;
            l.precioTotal = (Convert.ToDouble(l.precioTotal) + Convert.ToDouble(precioF)).ToString();
            Refrescar();
        }
        void ReducirCant(LineaVenta l, double precioF)
        {
            l.cantidad--;
            if (l.cantidad<1)
            {
                carro.Remove(l);
            }
            l.precioTotal = (Convert.ToDouble(l.precioTotal) - Convert.ToDouble(precioF)).ToString();
            Refrescar();
        }
        
        void Calc(bool b)
        {
            calc0.IsEnabled = b;
            calc1.IsEnabled = b;
            calc2.IsEnabled = b;
            calc3.IsEnabled = b;
            calc4.IsEnabled = b;
            calc5.IsEnabled = b;
            calc6.IsEnabled = b;
            calc7.IsEnabled = b;
            calc8.IsEnabled = b;
            calc9.IsEnabled = b;
        }
        void Interfaz(bool b)
        {
            // Des/habilitar todo
            buttUpCarro.IsEnabled = b;
            buttAddCarro.IsEnabled = b;
            buttDelCarro.IsEnabled = b;
            buttDownCarro.IsEnabled = b;
            calcDesc.IsEnabled = b;
            calcDel.IsEnabled = b;
            calcCalc.IsEnabled = b;

            txtSearch.IsEnabled = b;
            buttSearch.IsEnabled = b;
            comboBoxSearch.IsEnabled = b;


            Calc(b);
        }

        /*******************************/
        //     Buttons debajo Carro    //
        /*******************************/
        private void buttUpCarro_Click(object sender, RoutedEventArgs e)
        {//Sube al item anterior al seleccionado
            if (dataGrid.SelectedIndex>0)
            {
                dataGrid.SelectedIndex--;
            }
            else if (dataGrid.SelectedIndex==0)
            {
                dataGrid.SelectedIndex = carro.Count -1;
            }
        }

        private void buttAddCarro_Click(object sender, RoutedEventArgs e)
        {//Añade cantidad al item seleccionado
            if (dataGrid.SelectedIndex > -1)
            {//Si hay un item seleccionado
                Producto p = new Producto();
                double descontado = new double();
                double precioF = new double();

                p = carro[dataGrid.SelectedIndex].Producto;
                descontado = p.precio * p.descuento;
                descontado = descontado / 100;
                precioF = p.precio - descontado;

                AumentarCant(carro[dataGrid.SelectedIndex], precioF);
            }
        }

        private void buttDelCarro_Click(object sender, RoutedEventArgs e)
        {//Disminuye cantidad al item seleccionado
            if (dataGrid.SelectedIndex > -1)
            {//Si hay un item seleccionado
                Producto p = new Producto();
                double descontado = new double();
                double precioF = new double();
                
                p = carro[dataGrid.SelectedIndex].Producto;
                descontado = p.precio * p.descuento;
                descontado = descontado / 100;
                precioF = p.precio - descontado;

                ReducirCant(carro[dataGrid.SelectedIndex], precioF);
            }

        }

        private void buttDownCarro_Click(object sender, RoutedEventArgs e)
        {//Baja al item posterior al seleccionado
            if (dataGrid.SelectedIndex > -1 && dataGrid.SelectedIndex != (carro.Count-1))
            {
                dataGrid.SelectedIndex++;
            }
            else if (dataGrid.SelectedIndex == (carro.Count-1))
            {
                dataGrid.SelectedIndex = 0;
            }
        }

        private void buttDescartar_Click(object sender, RoutedEventArgs e)
        {//Anula la compra
            carro.Clear();
            Refrescar();

            //Habilitar todo de nuevo
            Interfaz(true);
            Calc(false);
            GridCats();
            calcDel.IsEnabled = false;
            descTarjeta = 0;
            lblDesc.Content = "Descuento Esp.: " + descTarjeta + "%";
        }

        private void buttFacturar_Click(object sender, RoutedEventArgs e)/**************************************************************/
        {//Crea y muestra la factura
            Factura factura = new Factura();
            factura.Start();
            factura.Show();
            factura.Met();
            this.Close();
        }





        /************************************/
        //            CALCULADORA           //
        /************************************/
        private void calcDesc_Click(object sender, RoutedEventArgs e)
        {
            Calc(true);
            calcDel.IsEnabled = true;
            calcDesc.IsEnabled = false;
            descTarjeta = 0;
        }

        private void Calc_Click(object sender, RoutedEventArgs e)
        {//Calculadora
            if (descTarjeta == 0)
            {
                descTarjeta = Convert.ToInt32((sender as Button).Content);
            }
            else
            {
                descTarjeta = descTarjeta * 10;
                descTarjeta = descTarjeta + Convert.ToInt32((sender as Button).Content);
                if (descTarjeta>100)
                {
                    MessageBox.Show("El porcentaje no puede superar el 100%");
                    descTarjeta = 0;
                }
            }
            lblDesc.Content = "Descuento Esp.: " + descTarjeta + "%";
        }

        private void calcCalc_Click(object sender, RoutedEventArgs e)
        {//Aplica el descuento al precio total (hacer al final)
            if (MessageBox.Show("Está seguro de que quiere calcular el precio definitivo?"+System.Environment.NewLine+
                "Una vez hecho no podrá modificar el carro.", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {//quiere hacerlo
                //Refrescar();
                double t = tot*descTarjeta/100;
                tot = tot - t;
                CalcTotal();

                //Deshabilitar todo
                Interfaz(false);
                spCat.Children.Clear();
                spProd.Children.Clear();
            }
        }

        private void calcDel_Click(object sender, RoutedEventArgs e)
        {
            descTarjeta = 0;
            lblDesc.Content = "Descuento Esp.: " + descTarjeta + "%";
            Calc(false);
            calcDel.IsEnabled = false;
            calcDesc.IsEnabled = true;
        }
    }
}
