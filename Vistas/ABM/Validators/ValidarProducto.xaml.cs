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
using ClasesBase.ABM;

namespace Vistas.ABM.Validators
{
    /// <summary>
    /// Lógica de interacción para ValidarProducto.xaml
    /// </summary>
    public partial class ValidarProducto : Window
    {
        public ValidarProducto()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtCodigo_TextChanged(object sender, TextChangedEventArgs e)
        {
         
        }

    }
}
