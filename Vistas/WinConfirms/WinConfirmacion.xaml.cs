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

namespace Vistas.WinConfirms
{
    /// <summary>
    /// Interaction logic for WinConfirmacion.xaml
    /// </summary>
    public partial class WinConfirmacion : Window
    {
        private string dialog;
        public string _Dialog
        {
            get { return this.dialog; }
            set { this.dialog = value; tbDialog.Text = this.dialog; }
        }

        public WinConfirmacion()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
