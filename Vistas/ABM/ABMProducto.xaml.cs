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
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using Microsoft.Win32;

namespace Vistas.ABM
{
    /// <summary>
    /// Interaction logic for ABMProducto.xaml
    /// </summary>
    public partial class ABMProducto : Window
    {
        CollectionView Vista;
        ObservableCollection<Producto> listaProducto;
        private CollectionViewSource vistaFiltrada;
        private bool elementoCreado;
        private bool elementoModificado;

        public ABMProducto()
        {
            InitializeComponent();

            vistaFiltrada = Resources["VISTA_PROD"] as CollectionViewSource;
            InitializeComponent();
            this.elementoModificado = false;
            this.elementoCreado = false;
            cambiar_estado_textboxs(false);
            btnGuardar.IsEnabled = false;
            btnCancelar.IsEnabled = false;          
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObjectDataProvider odp = (ObjectDataProvider)this.Resources["LIST_PROD"];
            listaProducto = odp.Data as ObservableCollection<Producto>;
            Vista = (CollectionView)CollectionViewSource.GetDefaultView(grid_content.DataContext);
            verificar_cantidad_elementos();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            
                if (this.elementoCreado)
                {
                    if (txtCodigo.Text != "" && cmbCategorias.SelectedValue.ToString() != "" && txtDescripcion.Text != ""  &&
                        txtPrecio.Text != "" && Decimal.Parse(txtPrecio.Text) != 0m)
                    {
                        

                        WinConfirms.WinConfirmacion winConf = new WinConfirms.WinConfirmacion();
                        winConf._Dialog = "Se requiere confirmar la operación: Alta de Producto";
                        winConf.ShowDialog();

                        if (winConf.DialogResult.Value)
                        {
                            Producto producto = new Producto();
                            producto.Prod_Cod = txtCodigo.Text;
                            producto.Prod_Categoria = cmbCategorias.SelectedValue.ToString();
                            producto.Prod_Descripcion = txtDescripcion.Text;
                            producto.Prod_Precio = txtPrecio.Text;
                            producto.Prod_Image = txtRutaImagen.Text;
                            ClasesBase.ABM.TrabajarProducto.insert_Producto(producto);
                            listaProducto[Vista.CurrentPosition] = producto;
                            ActividadLog actlog = new ActividadLog(Menu.Usu.Usu_Id, "Producto insertado: " + producto.ToString(), DateTime.Now.ToString("HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd"));
                            ClasesBase.ABM.TrabajarActLog.insert_actividad(actlog);

                            MessageBox.Show("La operación se realizó correctamente", "Alta de Producto", MessageBoxButton.OK, MessageBoxImage.Information);
                            
                            Vista.MoveCurrentToLast();
                            btnGuardar.IsEnabled = false;
                            btnCancelar.IsEnabled = false;
                            cambiar_estado_textboxs(false);
                            cambiar_estado_operaciones(true);
                            cambiar_estado_navegacion(true);
                            Vista = (CollectionView)CollectionViewSource.GetDefaultView(grid_content.DataContext);
                            Vista.Refresh();
                            elementoCreado = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Complete correctamente los campos", "Alta de Producto", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            

                    if (this.elementoModificado)
                    {
                        if (cmbCategorias.SelectedValue.ToString() != "" && txtDescripcion.Text != "" && txtPrecio.Text != "")
                        {
                            WinConfirms.WinConfirmacion winConf = new WinConfirms.WinConfirmacion();
                            winConf._Dialog = "Se requiere confirmar la operación: Modificar Producto";

                            winConf.ShowDialog();

                            if (winConf.DialogResult.Value)
                            {
                                listaProducto[Vista.CurrentPosition].Prod_Cod = txtCodigo.Text;
                                listaProducto[Vista.CurrentPosition].Prod_Descripcion = txtDescripcion.Text;
                                listaProducto[Vista.CurrentPosition].Prod_Categoria = cmbCategorias.SelectedValue.ToString();
                                listaProducto[Vista.CurrentPosition].Prod_Precio = txtPrecio.Text;
                                listaProducto[Vista.CurrentPosition].Prod_Image = txtRutaImagen.Text;
                                ClasesBase.ABM.TrabajarProducto.modificar_Producto(listaProducto[Vista.CurrentPosition]);
                                ActividadLog actlog = new ActividadLog(Menu.Usu.Usu_Id, "Producto modificado: " + listaProducto[Vista.CurrentPosition].ToString(), DateTime.Now.ToString("HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd"));
                                ClasesBase.ABM.TrabajarActLog.insert_actividad(actlog);

                                MessageBox.Show("La operación se realizó correctamente", "Modificar Producto", MessageBoxButton.OK, MessageBoxImage.Information);

                                btnGuardar.IsEnabled = false;
                                btnCancelar.IsEnabled = false;
                                cambiar_estado_textboxs(false);
                                cambiar_estado_operaciones(true);
                                cambiar_estado_navegacion(true);
                                this.elementoModificado = false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Complete correctamente los campos", "Modificar Proveedor", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }                    
                
        }

        private void mnNuevo_Click(object sender, RoutedEventArgs e)
        {
            cambiar_estado_textboxs(true);
            cmbCategorias.Focus();
            Producto prod = new Producto();
            elementoCreado = true;

            listaProducto.Add(prod);
            Vista.MoveCurrentToLast();

            txtCodigo.Text = (ClasesBase.ABM.TrabajarProducto.getLastProducto_Id() + 1).ToString();
            btnGuardar.IsEnabled = true;
            btnCancelar.IsEnabled = true;
            cambiar_estado_operaciones(false);
            cambiar_estado_navegacion(false);
            cmbCategorias.SelectedIndex = 0;
        }

        private void mnModificar_Click(object sender, RoutedEventArgs e)
        {
            this.elementoModificado = true;
            cmbCategorias.Focus();
            btnGuardar.IsEnabled = true;
            btnCancelar.IsEnabled = true;
            cambiar_estado_operaciones(false);
            cambiar_estado_textboxs(true);
            cambiar_estado_navegacion(false);
            txtCodigo.IsReadOnly = true;
        }

        private void mnEliminar_Click(object sender, RoutedEventArgs e)
        {
            WinConfirms.WinConfirmacion winConf = new WinConfirms.WinConfirmacion();
            winConf._Dialog = "Se requiere confirmar la operación: Eliminar Producto";
            winConf.ShowDialog();
            if (winConf.DialogResult.Value)
            {
                ActividadLog actlog = new ActividadLog(Menu.Usu.Usu_Id, "Producto eliminado: " + listaProducto[Vista.CurrentPosition].ToString(), DateTime.Now.ToString("HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd"));
                ClasesBase.ABM.TrabajarActLog.insert_actividad(actlog);
                ClasesBase.ABM.TrabajarProducto.delete_Producto(listaProducto[Vista.CurrentPosition].Prod_Cod);
                listaProducto.RemoveAt(Vista.CurrentPosition);

                MessageBox.Show("La operación se realizó correctamente" + "\nCantidad de elementos: " + Vista.Count, "Eliminar Producto", MessageBoxButton.OK, MessageBoxImage.Information);
                
                verificar_cantidad_elementos();
            }
        }

        private void btnVerificar_Click(object sender, RoutedEventArgs e)
        {
            Validators.ValidarProducto vp = new Validators.ValidarProducto();
            vp.ShowDialog();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (this.elementoCreado)
            {
                listaProducto.RemoveAt(Vista.CurrentPosition);
                cambiar_estado_navegacion(true);
                this.elementoCreado = false;
            }
            if (this.elementoModificado)
            {
                cambiar_estado_navegacion(true);
                this.elementoModificado = false;
            }
            cambiar_estado_textboxs(false);

            btnGuardar.IsEnabled = false;
            btnCancelar.IsEnabled = false;

            cambiar_estado_operaciones(true);

            verificar_cantidad_elementos();
        }

        private void btnCargarImagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            BitmapImage b = new BitmapImage();
            openFile.Title = "Seleccione la Imagen a Mostrar";
            openFile.Filter = "Archivos de imágenes (*.bmp, *.jpg, *.png, *.bmp)|*.bmp;*.jpg;*.png;*.bmp|Todos los archivos (*.*)|*.*";
            //"Imagenes(*.jpg;*.gif;*.png;*.bmp)";
            if (openFile.ShowDialog() == true)
            {
                b.BeginInit();
                b.UriSource = new Uri(openFile.FileName);
                b.EndInit();
                imgProducto.Stretch = Stretch.Fill;
                imgProducto.Source = b;

                txtRutaImagen.Text = openFile.FileName;
            }
        }

        private void eventVistaProducto_Filter(Object sender, FilterEventArgs e)
        {
            Producto prod = e.Item as Producto;
            if (cmbPatron.SelectedValue != null && prod.Prod_Categoria != null)
                if (prod.Prod_Categoria.StartsWith(cmbPatron.SelectedValue.ToString(), StringComparison.CurrentCultureIgnoreCase))
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
        }

        private void cmbPatron_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vistaFiltrada != null)
            {
                vistaFiltrada.Filter += eventVistaProducto_Filter;
            }
        }

        //Solo entrada de numeros(naturales, decimales)
        private void verificar_textbox_numerico(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]"); 
            e.Handled = regex.IsMatch(e.Text);
        }
        
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnPrimero_Click(object sender, RoutedEventArgs e)
        {
            Vista.MoveCurrentToFirst();
        }

        private void btnUltimo_Click(object sender, RoutedEventArgs e)
        {
            Vista.MoveCurrentToLast();
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            Vista.MoveCurrentToNext();
            if (Vista.IsCurrentAfterLast)
            {
                Vista.MoveCurrentToFirst();
            }

        }

        private void btnAnterior_Click(object sender, RoutedEventArgs e)
        {
            Vista.MoveCurrentToPrevious();
            if (Vista.IsCurrentBeforeFirst)
            {
                Vista.MoveCurrentToLast();
            }
        }

        void cambiar_estado_textboxs(bool estado)
        {
            cmbCategorias.IsEnabled = estado;
            txtPrecio.IsReadOnly = !estado;
            txtDescripcion.IsReadOnly = !estado;
            txtRutaImagen.IsReadOnly = !estado;

            btnCargarImagen.IsEnabled = estado;
        }

        public void cambiar_estado_navegacion(bool estado)
        {
            btnPrimero.IsEnabled = estado;
            btnSiguiente.IsEnabled = estado;
            btnAnterior.IsEnabled = estado;
            btnUltimo.IsEnabled = estado;
            btnImprimir.IsEnabled = estado;
        }

        public void cambiar_estado_operaciones(bool estado)
        {
            mnEliminar.IsEnabled = estado;
            mnModificar.IsEnabled = estado;
            mnNuevo.IsEnabled = estado;
            cmbPatron.IsEnabled = estado;
        }

        public void verificar_cantidad_elementos()
        {
            if (Vista.Count <= 0)
            {
                mnModificar.IsEnabled = false;
                mnEliminar.IsEnabled = false;
                cambiar_estado_navegacion(false);

                MessageBox.Show("No hay elementos en el regitro de productos", "ABM de Productos", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            ABM.Vista_Previa.VPProductos vpProductos = new Vista_Previa.VPProductos(vistaFiltrada);
            vpProductos.ShowDialog();
        }

        private void txtCodigo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
