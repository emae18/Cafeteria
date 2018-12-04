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

namespace Vistas.ABM.Vista_Previa
{
    /// <summary>
    /// Lógica de interacción para VPProductos.xaml
    /// </summary>
    public partial class VPProductos : Window
    {
        public VPProductos(CollectionViewSource listaFiltrada)
        {
            InitializeComponent();
            lvwProductos.ItemsSource = listaFiltrada.View;
        }

        private void btnImpri_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog dialogPrint = new PrintDialog();
            if (dialogPrint.ShowDialog() == true)
            {
                dialogPrint.PrintDocument(((IDocumentPaginatorSource)DocMain).DocumentPaginator, "Imprimir");
            }
        }
    }
}
