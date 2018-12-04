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

namespace Vistas
{
    /// <summary>
    /// Lógica de interacción para VistaPrevia.xaml
    /// </summary>
    public partial class VistaPrevia : Window
    {
        // private CollectionViewSource vistaFiltrada;

        public VistaPrevia(CollectionViewSource listaFiltrada)
        {
            InitializeComponent();


            lVwClientes.ItemsSource = listaFiltrada.View;
            
        }
        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
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
