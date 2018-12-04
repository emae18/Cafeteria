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
namespace Vistas.Ventas
{
    /// <summary>
    /// Interaction logic for WinGanancia.xaml
    /// </summary>
    public partial class WinGanancia : Window
    {
        public WinGanancia()
        {
            InitializeComponent();
        }

        private void btnGanFec_Click(object sender, RoutedEventArgs e)
        {
            if (dPFechaI.SelectedDate != null && dPFechaF.SelectedDate != null)
            {
                MessageBox.Show("Ganancia: " + ClasesBase.ABM.TrabajarVenta.gananciaFecha(dPFechaI.SelectedDate.Value, dPFechaF.SelectedDate.Value));
            }
            else
            {
                MessageBox.Show("Por Favor Seleccione Ambas Fechas");
            }
                
        }
       
        private void btnOneFecGan_Click(object sender, RoutedEventArgs e)
        {
             if (dPGanancia.SelectedDate != null)
            {
                MessageBox.Show("Ganancia de la fecha " + ClasesBase.ABM.TrabajarVenta.ganancia(DateTime.Parse(dPGanancia.SelectedDate.Value.ToString())));
            }
             else
             {
                 MessageBox.Show("Por favot seleccione una fecha");
             }
        }

        private void btnTotal_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ganancia hasta ahora: " + ClasesBase.ABM.TrabajarVenta.gananciaTotal());
        }
    }
}
