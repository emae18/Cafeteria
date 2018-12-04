using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClasesBase;
using System.Data;
using System.Data.SqlClient;

namespace Vistas.UserControls
{
    /// <summary>
    /// Interaction logic for UCLogin.xaml
    /// </summary>
    public partial class UCLogin : UserControl
    {
        private HistorialLogin hlog;
        public HistorialLogin Hlog
        {
            get { return hlog; }
            set { hlog = value; }
        }

        public UCLogin()
        {
            InitializeComponent();
        }      

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            var winLogin = Window.GetWindow(this);
            bool encontrado = false;
            Usuario usuarioLogueado = new Usuario();            
            try
            {
                DataTable dt = new DataTable();
                dt = ClasesBase.ABM.TrabajarUsuario.buscar_usuario(txtUsuario.Text);
                DataRow dr = dt.Rows[0];
                if (dr["Contraseña"].ToString() == pbContraseña.Password.ToString())
                {
                    usuarioLogueado.Usu_Id = (Int32)dr["ID"];
                    usuarioLogueado.Usu_Nombre = dr["Nombres"].ToString();
                    usuarioLogueado.Usu_Apellido = dr["Apellido"].ToString();
                    usuarioLogueado.Usu_NombreUsuario = dr["Nombre de Usuario"].ToString();
                    usuarioLogueado.Usu_Contraseña = dr["Contraseña"].ToString();
                    usuarioLogueado.Usu_Rol = dr["Rol"].ToString();
                    usuarioLogueado.Usu_Image = (byte[])dr["Imagen"];
                    encontrado = true;
                }
                else 
                {
                    MessageBox.Show("Contraseña incorrecta", "Autenticación", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }                
            }
            catch
            {
                MessageBox.Show("Nombre de Usuario incorrecto", "Autenticación", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                encontrado = false;
            }
            if (encontrado == true)
            {
                DateTime date = DateTime.Today;
                Hlog = new HistorialLogin(usuarioLogueado.Usu_Id, DateTime.Now.ToString("HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd"), "EN CURSO");          
                winLogin.Hide();
                Menu menu = new Menu(usuarioLogueado);
                ClasesBase.ABM.TrabajarHLog.insert_hlog(Hlog);
                menu.ShowDialog();
                Hlog.Hlog_duracion = menu.ucCron.Duracion;
                ClasesBase.ABM.TrabajarHLog.update_historial(Hlog, ClasesBase.ABM.TrabajarHLog.getLastHistorial_Id());
                Hlog = new HistorialLogin();
                pbContraseña.Password = null;
                pbContraseña.Focus();
                winLogin.Show();
            }
             
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            var winLogin = Window.GetWindow(this);
            winLogin.Close();        
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtUsuario.Focus();
        }

        private void gridLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                btnIngresar_Click(sender, e);
            }
        }

        //Estilos
        private void btnIngresar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnIngresar.Background = Brushes.GhostWhite;
        }

        private void btnIngresar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnIngresar.Background = Brushes.LightSlateGray;
        }

        private void btnSalir_MouseLeave(object sender, MouseEventArgs e)
        {
            btnSalir.Background = Brushes.LightSlateGray;
        }

        private void btnSalir_MouseEnter(object sender, MouseEventArgs e)
        {
            btnSalir.Background = Brushes.GhostWhite;
        }
    }
}
