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
using TPV.DAL;
using TPV.Model;
using TPV.Windows;

namespace TPV
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static UnitOfWork u = new UnitOfWork();
        public static Usuario user = new Usuario();
        public MainWindow()
        {
            InitializeComponent();

            //Si no existe ningún usuario, crea un administrador
            if (u.UsuariosRepository.GetAll().Count<1)
            {
                Usuario admin = new Usuario();
                admin.nombre = "admin1";
                admin.password = "abc123.";
                admin.admin = true;
                u.UsuariosRepository.Create(admin);
            }

            comboBox.ItemsSource = u.UsuariosRepository.GetAll();
            comboBox.DisplayMemberPath = "nombre";
            comboBox.SelectedValuePath = "nombre";
        }

        private void buttEnter_Click(object sender, RoutedEventArgs e)
        {//Comprueba que todos los datos son correctos


            //Console.WriteLine("Check: " + checkBox.IsChecked.Value);
            //Console.WriteLine("Short Date: " + DateTime.Today.ToShortDateString());
            //Console.WriteLine("Long Date: " + DateTime.Today.ToLongDateString());

            
            if (comboBox.Text != "" && passwordBox.Password != "")
            {
                if (u.UsuariosRepository.Single(c=>c.nombre == comboBox.Text) != null)
                {
                    user = u.UsuariosRepository.Single(c => c.nombre == comboBox.Text);
                    if (user.password == passwordBox.Password)
                    {
                        //Compruebo si es admin o no
                        if (user.admin)
                        {//Abro ventana de administrador
                            AdminWindow aW = new AdminWindow();
                            aW.Show();
                            Limpiar();
                        }
                        else
                        {//Abro ventana de usuario
                            UserWindow uW = new UserWindow();
                            uW.Show();
                            Limpiar();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Contraseña incorrecta");
                        //Esto no estaría en una aplicación de cara al público, pero indico la contraseña
                        //labelPass.Content = "";
                        //labelPass.Content = u.UsuariosRepository.Single(c => c.nombre == comboBox.Text).password.ToString();
                    }
                }
                else
                {
                    MessageBox.Show("El usuario no existe, sólo un administrador puede crearle una cuenta");
                }
            }
            else
            {
                MessageBox.Show("Ningún campo puede estar vacío");
            }
        }


        void Limpiar()
        {
            comboBox.Text = "";
            passwordBox.Password = "";
            labelPass.Content = "";
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Usuario us = new Usuario();
                us = (Usuario)comboBox.SelectedItem;
                passwordBox.Password = us.password;
            }
            catch (Exception)
            {
                passwordBox.Password = "";
            }
            
        }
    }
}
