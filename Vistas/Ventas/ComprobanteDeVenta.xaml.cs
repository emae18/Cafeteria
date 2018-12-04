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
    /// Lógica de interacción para ComprobanteDeVenta.xaml
    /// </summary>
    public partial class ComprobanteDeVenta : Window
    {
        public ComprobanteDeVenta(Venta venta)
        {
            InitializeComponent();
            Cliente cli = new Cliente();
            Vendedor vend = ClasesBase.ABM.TrabajarVendedor.getVendedor_fromDB(venta.Venta_Vendedor.Vend_Legajo);

            cargar_campos(venta, cli, vend);
        }

        public void cargar_campos(Venta venta, Cliente cliente, Vendedor vendedor)
        {
            txtFecHor.Text = venta.Venta_FechaFactura.ToString("dd/MM/yyyy HH:mm:ss");
            txtNro.Text = venta.Venta_NroFactura.ToString();
            impTotal.Content ="$ " +  ClasesBase.ABM.TrabajarVentaDetalle.impTotal(venta.Venta_NroFactura).ToString();
            txtCliente.Text = cliente.Cli_Apellido + ", " + cliente.Cli_Nombre;
            txtVendedor.Text = vendedor.Vend_Apellido + ", " + vendedor.Vend_Nombre;
        }
    }
}
