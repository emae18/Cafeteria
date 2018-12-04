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
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;
 
namespace Vistas.Gastos
{
    /// <summary>
    /// Interaction logic for WinGastos.xaml
    /// </summary>
    public partial class WinGastos : Window
    {
        public WinGastos()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Gasto gs = new Gasto();
            gs.Gasto_Descripcion = txtDescripcion.Text;
            gs.Gasto_Precio = double.Parse( txtPrecio.Text);
            ClasesBase.ABM.TrabajarGastos.insertarGasto(gs);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dt = ClasesBase.ABM.TrabajarGastos.traerGastos();
            foreach (DataRow row in dt.Rows)
            {
                lV1.Items.Add(row[0].ToString());

                
            }
        }

        
    }
}