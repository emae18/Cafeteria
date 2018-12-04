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

namespace Vistas
{
    /// <summary>
    /// Lógica de interacción para WinVentas.xaml
    /// </summary>
    public partial class WinVentas : Window
    {
        ObservableCollection<Venta> listaVentas;
        private CollectionViewSource vistaFiltrada;
        private string filtroSeleccionado = "";
        ObservableCollection<Producto> productos;
        private int valor;
        ObservableCollection<VentaDetalle> carrito;

        public WinVentas()
        {
            InitializeComponent();

            vistaFiltrada = Resources["VISTA_VENTAS"] as CollectionViewSource;

            limpiar_campos();
            cambiar_estado_textbox(false);
            cambiar_estado_operaciones(true);
            btnGuardar.IsEnabled = false;
            btnCancelar.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObjectDataProvider odp = (ObjectDataProvider)this.Resources["LIST_VENTAS"];
            listaVentas = odp.Data as ObservableCollection<Venta>;
            productos = new ObservableCollection<Producto>();
            valor = productos.Count;
            lblC.Content = valor;
            carrito = new ObservableCollection<VentaDetalle>();
            dtpFecha.SelectedDate = DateTime.Now.Date + DateTime.Now.TimeOfDay;
            dtpFecha.IsEnabled = false;
        }

        private void mnNuevo_Click(object sender, RoutedEventArgs e)
        {
            btnGuardar.IsEnabled = true;
            btnCancelar.IsEnabled = true;
            cambiar_estado_textbox(true);
            cambiar_estado_operaciones(false);
            txtNroFactura.Text = (ClasesBase.ABM.TrabajarVenta.getLastVenta_Id() + 1).ToString();            
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (carrito.Count != 0)
            {
                Venta venta = new Venta();
                venta.Venta_NroFactura = Int32.Parse(txtNroFactura.Text);
                DateTime dateNow = DateTime.Now;
                TimeSpan hora = new TimeSpan(dateNow.Hour, dateNow.Minute, dateNow.Second);
                venta.Venta_FechaFactura = (DateTime)dtpFecha.SelectedDate;
                venta.Venta_FechaFactura.Add(DateTime.Now.TimeOfDay);
                venta.Venta_Cliente = null;
                venta.Venta_Vendedor = ClasesBase.ABM.TrabajarVendedor.getVendedor_fromDB(cbVendedor.SelectedValue.ToString());

                WinConfirms.WinConfirmacion winConf = new WinConfirms.WinConfirmacion();
                winConf._Dialog = "Se requiere confirmar la operación: Alta de Cliente";
                winConf.ShowDialog();
                if (winConf.DialogResult.Value)
                {
                    for (int i = 0; i < carrito.Count; i++)
                    {
                        carrito[i].Vd_Importe = precioItem(carrito[i].Vd_CodProducto, carrito[i].Vd_Cantidad);
                        ClasesBase.ABM.TrabajarVentaDetalle.insert_ventaDetalle(carrito[i]);
                    }
                    venta.Venta_ImporteTotal = precioFinal();
                    listaVentas.Add(venta);
                    ClasesBase.ABM.TrabajarVenta.insert_venta(venta);
                    MessageBox.Show("La operación se realizó correctamente", "Alta de Venta", MessageBoxButton.OK, MessageBoxImage.Information);
                  ComprobanteDeVenta winComp = new ComprobanteDeVenta(venta);
                  winComp.ShowDialog();

                    cambiar_estado_operaciones(true);
                    limpiar_campos();
                    cambiar_estado_textbox(false);
                    carrito.Clear();
                    lblC.Content = carrito.Count.ToString();
                    btnGuardar.IsEnabled = false;
                    btnCancelar.IsEnabled = false;
                }
            }
            else
            {
                MessageBox.Show("Complete los campos", "Alta de Ventas", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private decimal precioFinal()
        {
            decimal a = 0;
            for (int i = 0; i < carrito.Count; i++)
            {
                a = a + carrito[i].Vd_Importe;
            }
            return a;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            cambiar_estado_operaciones(true);
            limpiar_campos();
            cambiar_estado_textbox(false);
            btnGuardar.IsEnabled = false;
            btnCancelar.IsEnabled = false;
        }

        public void cambiar_estado_textbox(bool estado)
        {
            //cbCliente.IsEnabled = estado;
            cbProducto.IsEnabled = estado;
            cbVendedor.IsEnabled = estado;
        }

        public void cambiar_estado_operaciones(bool estado)
        {
            mnNuevo.IsEnabled = estado;
            expFiltros.IsEnabled = estado;
        }

        public void limpiar_campos()
        {
            txtNroFactura.Text = "";
            dtpFecha.SelectedDate = DateTime.Now;
            //cbCliente.SelectedValue = "";
            cbProducto.SelectedValue = "";
            cbVendedor.SelectedValue = "";
            txtCantProd.Text = "";
        }

        public void reset_visibilidad_listas()
        {
            listVentas.Visibility = Visibility.Visible;
            listClientes.Visibility = Visibility.Hidden;
            listVendedor.Visibility = Visibility.Hidden;
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void btnCar_Click(object sender, RoutedEventArgs e)
        {
            if (txtNroFactura.Text != "" && dtpFecha.SelectedDate != null
                && cbProducto.SelectedValue != null && cbVendedor.SelectedValue != null && txtCantProd.Text != "" && txtCantProd.Text != "0")
            {
                VentaDetalle item = new VentaDetalle();

                item.Vd_NroFactura = Int32.Parse(txtNroFactura.Text);
                item.Vd_CodProducto = cbProducto.SelectedValue.ToString();
                item.Vd_Cantidad = Int32.Parse(txtCantProd.Text);
                item.Vd_Precio = decimal.Parse(ClasesBase.ABM.TrabajarProducto.traerProducto(item.Vd_CodProducto).Prod_Precio);
                item.Vd_Importe = 0;
                WinConfirms.WinConfirmacion winConf = new WinConfirms.WinConfirmacion();
                winConf._Dialog = "Se requiere confirmar la operación: Cargar al Carrito";
                winConf.ShowDialog();
                if (winConf.DialogResult.Value)
                {
                    cbVendedor.IsEnabled = false;
                    //cbCliente.IsEnabled = false;
                    agregarCarrito(item);
                    txtCantProd.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Complete los campos", "Agregar al Carrito", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void agregarCarrito(VentaDetalle item)
        {
            bool encontrado = false;
            if (carrito.Count == 0)
            {
                carrito.Add(item);
                lblC.Content = carrito.Count.ToString();
            }
            else
            {
                int aux = 0;
                for (int i = 0; i < carrito.Count; i++)
                {
                    if (carrito[i].Vd_CodProducto == cbProducto.SelectedValue.ToString())
                    {
                        aux = i;
                        encontrado = true;
                    }
                }
                if (encontrado)
                {
                    carrito[aux].Vd_Cantidad = carrito[aux].Vd_Cantidad + Int32.Parse(txtCantProd.Text.ToString());
                }
                else
                {
                    carrito.Add(item);
                    lblC.Content = carrito.Count.ToString();
                }

            }
        }

        private decimal precioItem(string cod, int cant)
        {
            decimal precio;
            precio = decimal.Parse(ClasesBase.ABM.TrabajarProducto.traerProducto(cod).Prod_Precio) * cant;
            return precio;
        }
         
        private void expander1_Expanded(object sender, RoutedEventArgs e)
        {
            expFiltros.Height = 95;
            brdBListado.Height = brdBListado.Height + 70;
            Thickness margin = new Thickness();
            margin.Top = 106;
            margin.Left = 6;
            margin.Bottom = 6;
            margin.Right = 6;
            listVentas.Margin = margin;
            listClientes.Margin = margin;
        }

        private void expFiltros_Collapsed(object sender, RoutedEventArgs e)
        {
            expFiltros.Height = 23;
            brdBListado.Height = brdBListado.Height - 70;
            Thickness margin = new Thickness();
            margin.Top = 36;
            margin.Left = 6;
            margin.Bottom = 6;
            margin.Right = 6;
            listVentas.Margin = margin;
            listClientes.Margin = margin;
        }

        private void eventVistaVentas_Filter(object sender, FilterEventArgs e)
        {
            Venta vnt = e.Item as Venta;
            if (filtroSeleccionado == "Fecha")
            {
                if (dpFechaInicial.SelectedDate != null && dpFechaFinal.SelectedDate != null)
                {
                    int result = DateTime.Compare(dpFechaInicial.SelectedDate.Value, dpFechaFinal.SelectedDate.Value);
                    if (result <= 0 && vnt.Venta_FechaFactura.Date >= dpFechaInicial.SelectedDate.Value && vnt.Venta_FechaFactura.Date <= dpFechaFinal.SelectedDate.Value)
                    {
                        e.Accepted = true;
                    }
                    else
                    {
                        e.Accepted = false;
                    }
                }
            }

            if (filtroSeleccionado == "Clientes")
            {
                if (cmbCliente.SelectedValue != null)
                {
                    if (vnt.Venta_Cliente.Cli_Dni.Equals(cmbCliente.SelectedValue))
                    {
                        e.Accepted = true;
                    }
                    else
                    {
                        e.Accepted = false;
                    }
                }
            }

            if (filtroSeleccionado == "Vendedores")
            {
                if (cmbVendedor.SelectedValue != null)
                {
                    if (vnt.Venta_Vendedor.Vend_Legajo.Equals(cmbVendedor.SelectedValue.ToString()))
                    {
                        e.Accepted = true;
                    }
                    else
                    {
                        e.Accepted = false;
                    }
                }
            }
        }

        private void dpFechaInicial_CalendarOpened(object sender, RoutedEventArgs e)
        {
            if (dpFechaFinal.SelectedDate != null)
            {
                dpFechaInicial.DisplayDateEnd = dpFechaFinal.SelectedDate.Value;
            }
        }

        private void dpFechaFinal_CalendarOpened(object sender, RoutedEventArgs e)
        {
            if (dpFechaInicial.SelectedDate != null)
            {
                dpFechaFinal.DisplayDateStart = dpFechaInicial.SelectedDate.Value;
            }
        }

        private void btnLimpiarFiltros_Click(object sender, RoutedEventArgs e)
        {
            dpFechaFinal.SelectedDate = null;
            dpFechaFinal.DisplayDateStart = null;            
            dpFechaInicial.SelectedDate = null;
            dpFechaInicial.DisplayDateEnd = null;            
            cmbVendedor.SelectedItem = null;            
            cmbCliente.SelectedItem = null;           
            
            filtroSeleccionado = "";

            cambiar_estado_filtros();
            reset_visibilidad_listas();
        }

        private void cambiar_estado_filtros()
        {
            dpFechaFinal.IsEnabled = true;
            cmbVendedor.IsEnabled = true;
            dpFechaInicial.IsEnabled = true;
            cmbCliente.IsEnabled = true;
        }

        private void control_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            filtroSeleccionado = "Fecha";
            cmbCliente.IsEnabled = false;
            cmbVendedor.IsEnabled = false;
            if (vistaFiltrada != null)
            {
                vistaFiltrada.Filter += eventVistaVentas_Filter;                
            }
            listVentas.Visibility = Visibility.Hidden;
            listClientes.Visibility = Visibility.Visible;
        }

        private void cmbCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filtroSeleccionado = "Clientes";
            dpFechaFinal.IsEnabled = false;
            dpFechaInicial.IsEnabled = false;
            cmbVendedor.IsEnabled = false;
            {
                vistaFiltrada.Filter += eventVistaVentas_Filter;
            }
            listVentas.Visibility = Visibility.Hidden;
            listClientes.Visibility = Visibility.Visible;
        }

        private void cmbVendedor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filtroSeleccionado = "Vendedores";
            dpFechaFinal.IsEnabled = false;
            dpFechaInicial.IsEnabled = false;
            cmbCliente.IsEnabled = false;
            {
                vistaFiltrada.Filter += eventVistaVentas_Filter;
            }
            listVentas.Visibility = Visibility.Hidden;
            listVendedor.Visibility = Visibility.Visible;
        }
        private void verificar_textbox_numerico(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void btnGridCarrito_MouseEnter(object sender, MouseEventArgs e)
        {
            listCarrito.ItemsSource = vistaCarrito();
            listCarrito.Visibility = Visibility.Visible;
        }

        private void btnGridCarrito_MouseLeave(object sender, MouseEventArgs e)
        {
            listCarrito.ItemsSource = vistaCarrito();
            listCarrito.Visibility = Visibility.Hidden;
        }
        private ObservableCollection<PartialClass> vistaCarrito()
        {
            ObservableCollection<PartialClass> aux = new ObservableCollection<PartialClass>();
            for (int i = 0; i < carrito.Count; i++)
            {
                aux.Add(new PartialClass());
                aux[i].CantProd = carrito[i].Vd_Cantidad;
                aux[i].PrecFinalVent = carrito[i].Vd_Precio * carrito[i].Vd_Cantidad;
                aux[i].DescProd = ClasesBase.ABM.TrabajarProducto.descProd(carrito[i].Vd_CodProducto);
                aux[i].PrecProd = carrito[i].Vd_Precio;
            }
            return aux;
        }

        private void listCarrito_MouseEnter(object sender, MouseEventArgs e)
        {
            listCarrito.ItemsSource = vistaCarrito();
            listCarrito.Visibility = Visibility.Visible;
        }

        private void listCarrito_MouseLeave(object sender, MouseEventArgs e)
        {
            listCarrito.ItemsSource = vistaCarrito();
            listCarrito.Visibility = Visibility.Hidden;
        }

        private void btnVenta_Click(object sender, RoutedEventArgs e)
        {
            Vistas.Ventas.WinGanancia winG = new Vistas.Ventas.WinGanancia();
            winG.Show(); 
        }

        private void btnPrecios_Click(object sender, RoutedEventArgs e)
        {
            ABM.ABMProducto lista = new ABM.ABMProducto();
            lista.Show();
        }

        
    }        
}
