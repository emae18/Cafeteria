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
using System.Windows.Shapes;
using ClasesBase;

namespace Vistas
{
    /// <summary>
    /// Lógica de interacción para Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        private static Usuario usu;
        public static Usuario Usu
        {
            get { return usu; }
            set { usu = value; }
        }

        public Menu(Usuario usuarioLogueado)
        {
            InitializeComponent();
            ucUsuarioLogueado.setUsuarioLogueado(usuarioLogueado);
            Usu = usuarioLogueado;

            if (usuarioLogueado.Usu_Rol == "Vendedor")
            {
                mnVendedor.IsEnabled = false;
            }
        }

        private void btnVer_Click(object sender, RoutedEventArgs e)
        {
            ABM.ABMUsuario abmUsu = new ABM.ABMUsuario(usu);
            abmUsu.ShowDialog();

        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void mnCliente_Click(object sender, RoutedEventArgs e)
        {
            ABM.ABMCliente abmCli = new ABM.ABMCliente();
            abmCli.ShowDialog();
        }

        private void mnProducto_Click(object sender, RoutedEventArgs e)
        {
            ABM.ABMProducto abmProd = new ABM.ABMProducto();
            abmProd.ShowDialog();
        }

        private void mnProveedor_Click(object sender, RoutedEventArgs e)
        {
            ABM.ABMProveedor abmProv = new ABM.ABMProveedor();
            abmProv.ShowDialog();
        }

        private void mnVendedor_Click(object sender, RoutedEventArgs e)
        {
            ABM.ABMVendedor abmVen = new ABM.ABMVendedor();
            abmVen.ShowDialog();
        }

        private void mnAbout_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void mnVentas_Click(object sender, RoutedEventArgs e)
        {
            WinVentas wVentas = new WinVentas();
            wVentas.ShowDialog();
        }

        private void btnConfig_Click(object sender, RoutedEventArgs e)
        {
            WinConfig wC = new WinConfig(this, Usu);
            wC.ShowDialog();
        }

        private void mnGastos_Click(object sender, RoutedEventArgs e)
        {
            Gastos.WinGastos winGast = new Gastos.WinGastos();
            winGast.Show();
        }

        

        

        
    }
}
