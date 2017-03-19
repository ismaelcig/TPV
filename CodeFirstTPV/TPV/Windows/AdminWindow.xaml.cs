using Microsoft.Win32;
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
    /// Lógica de interacción para AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public Producto p = new Producto();

        public AdminWindow()
        {
            InitializeComponent();

            labelUser.Content = "Bienvenido, " + MainWindow.user.nombre;
            dataGridUsers.SelectedValuePath = "ID";
            dataGridProd.SelectedValuePath = "ID";
            pComboBox.DisplayMemberPath = "nombre";
            pComboBox.SelectedValuePath = "CategoriaID";
            RecargarUsers();
            RecargarCats();
            RecargarProds();

            RecargarComboBox();
        }



        /****************/
        //   User Tab   //
        /****************/
        private void buttAddUser_Click(object sender, RoutedEventArgs e)
        {
            if (uTxtName.Text !="" && passwordBox.Password!="")
            {
                Usuario user = new Usuario();
                user.nombre = uTxtName.Text;
                user.password = passwordBox.Password;
                user.admin = checkBox.IsChecked.Value;

                MainWindow.u.UsuariosRepository.Create(user);
                RecargarUsers();
            }
            else
            {
                MessageBox.Show("Nombre y contraseña no pueden estar vacíos");
            }
        }

        private void buttDelUser_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridUsers.SelectedIndex>-1)
            {//Hay un usuario seleccionado
                Usuario user = new Usuario();
                user = MainWindow.u.UsuariosRepository.Single(c => c.UsuarioID.ToString() == dataGridUsers.SelectedValue.ToString());

                if (user.UsuarioID != MainWindow.user.UsuarioID)
                {
                    MainWindow.u.UsuariosRepository.Delete(user);
                }
                else
                {
                    MessageBox.Show("No se pude borrar a si mismo");
                }
                RecargarUsers();
            }
        }




        /****************/
        //   Cate Tab   //
        /****************/
        private void buttAddCat_Click(object sender, RoutedEventArgs e)
        {
            if (cTxtName.Text != "")
            {
                Categoria cat = new Categoria();
                cat.nombre = cTxtName.Text;

                MainWindow.u.CategoriasRepository.Create(cat);
                RecargarCats();
                RecargarComboBox();
                cTxtName.Text = "";
            }
            else
            {
                MessageBox.Show("El nombre no puede estar vacío");
            }
        }


        /****************/
        //   Prod Tab   //
        /****************/
        private void pButtImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog(); ofd.InitialDirectory = Directory.GetCurrentDirectory().ToString() + @"\Productos\"; ;
            ofd.Filter = "Imágenes (*.png, *.jpg)|*.png;*.jpg|Todos (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            
            if (ofd.ShowDialog() ==true)
            {
                //p.imagen = ofd.FileName;
                string[] urlSplit = ofd.FileName.Split('\\');
                p.imagen = urlSplit[urlSplit.Length -1];
                string url = Directory.GetCurrentDirectory();
                url = url + "\\Productos\\"+p.imagen; 
                pImage.Source = new BitmapImage(new Uri(url));
            }
        }
        private void pButtAdd_Click(object sender, RoutedEventArgs e)
        {
            if (pTxtName.Text != "" && pTxtPrecio.Text != "" && p.imagen != "")
            {
                p.nombre = pTxtName.Text;
                if (pTxtDesc.Text != "")
                {
                    p.descuento = Int32.Parse(pTxtDesc.Text);
                }
                else
                {
                    p.descuento = 0;
                }
                p.precio = Convert.ToDouble(pTxtPrecio.Text);
                if (pComboBox.SelectedIndex != -1)
                {
                    p.Categoria = MainWindow.u.CategoriasRepository.Single(c => c.CategoriaID.ToString() == pComboBox.SelectedValue.ToString());
                }
                else
                {
                    p.Categoria = MainWindow.u.CategoriasRepository.Single(c => c.nombre == "Otros");
                }

                MainWindow.u.ProductosRepository.Create(p);

                RecargarProds();
                p = new Producto();
                pTxtName.Text = "";
                pTxtDesc.Text = "";
                pTxtPrecio.Text = "";
                pComboBox.SelectedIndex = -1;
                pImage.Source = null;
            }
            else
            {
                MessageBox.Show("Nombre, precio e imagen son campos obligatorios");
            }
            
        }
        private void pButtMod_Click(object sender, RoutedEventArgs e)
        {
            if (p.ProductoID != 0)
            {
                p.nombre = pTxtName.Text;
                p.descuento = Int32.Parse(pTxtDesc.Text);
                p.precio = Convert.ToDouble(pTxtPrecio.Text);
                p.Categoria = MainWindow.u.CategoriasRepository.Single(c => c.CategoriaID.ToString() == pComboBox.SelectedValue.ToString());
                MainWindow.u.ProductosRepository.Update(p);

                RecargarProds();
                pTxtName.Text = "";
                pTxtDesc.Text = "";
                pTxtPrecio.Text = "";
                pComboBox.SelectedIndex = -1;
                pImage.Source = null;
            }
            else
            {
                MessageBox.Show("Para modificar, primero debe seleccionar un usuario");
            }
        }
        private void pButtDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.u.ProductosRepository.Delete(p);
            }
            catch (Exception)
            {
                
            }

            RecargarProds();
            p = new Producto();
        }

        private void dataGridProd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                p = MainWindow.u.ProductosRepository.Single(c => c.ProductoID.ToString() == dataGridProd.SelectedValue.ToString());
                pTxtName.Text = p.nombre;
                pTxtDesc.Text = p.descuento.ToString();
                pTxtPrecio.Text = p.precio.ToString();
                pComboBox.Text = p.Categoria.nombre;

                string url = Directory.GetCurrentDirectory();
                url = url + "\\Productos\\" + p.imagen;

                pImage.Source = new BitmapImage(new Uri(url));
            }
            catch (Exception)
            {
                
            }
        }
        /****************/
        //    Refresh   //
        /****************/
        void RecargarUsers()
        {
            dataGridUsers.ItemsSource = null;
            dataGridUsers.ItemsSource = MainWindow.u.UsuariosRepository.GetAll().Select(c =>
            new { ID = c.UsuarioID, Nombre = c.nombre, Contraseña = c.password, Admin = c.admin });
        }
        void RecargarCats()
        {
            dataGridCat.ItemsSource = null;
            dataGridCat.ItemsSource = MainWindow.u.CategoriasRepository.GetAll().Select(c =>
              new { ID = c.CategoriaID, Nombre = c.nombre });
        }
        void RecargarProds()
        {
            dataGridProd.ItemsSource = null;
            dataGridProd.ItemsSource = MainWindow.u.ProductosRepository.GetAll().Select(c =>
            new {
                ID = c.ProductoID,
                Nombre = c.nombre,
                Precio = c.precio,
                Descuento = c.descuento,
                Categoría = c.Categoria.nombre
                });
        }


        void RecargarComboBox()
        {
            pComboBox.ItemsSource = null;
            pComboBox.ItemsSource = MainWindow.u.CategoriasRepository.GetAll();
        }





        private void buttDesc_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Adiós, " + MainWindow.user.nombre);
            this.Close();
        }
    }
}
