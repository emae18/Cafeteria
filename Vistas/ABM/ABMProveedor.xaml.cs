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
using System.Collections.ObjectModel;

namespace Vistas.ABM
{
    /// <summary>
    /// Interaction logic for ABMProveedor.xaml
    /// </summary>
    public partial class ABMProveedor : Window
    {
        CollectionView Vista;
        ObservableCollection<Proveedor> listaProveedor;
        private CollectionViewSource vistaFiltrada;
        private bool elementoCreado;
        private bool elementoModificado;

        public ABMProveedor()
        {
            InitializeComponent();
            vistaFiltrada = Resources["VISTA_PROV"] as CollectionViewSource;
            txtPatron.Text = "";
            this.elementoModificado = false;
            this.elementoCreado = false;
            cambiar_estado_textboxs(false);
            btnGuardar.IsEnabled = false;
            btnCancelar.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObjectDataProvider odp = (ObjectDataProvider)this.Resources["LIST_PROV"];
            listaProveedor = odp.Data as ObservableCollection<Proveedor>;
            Vista = (CollectionView)CollectionViewSource.GetDefaultView(grid_content.DataContext);

            mnNuevo.Focus();
            verificar_cantidad_elementos();
        }

        private void mnNuevo_Click(object sender, RoutedEventArgs e)
        {
            cambiar_estado_textboxs(true);
            txtCuit.Focus();
            Proveedor prov = new Proveedor();
            elementoCreado = true;

            listaProveedor.Add(prov);
            Vista.MoveCurrentToLast();

            btnGuardar.IsEnabled = true;
            btnCancelar.IsEnabled = true;
            cambiar_estado_operaciones(false);
            cambiar_estado_navegacion(false);
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {            
            if (this.elementoCreado)
            {
                if (txtDomicilio.Text != "" && txtTelefono.Text != "" && txtCuit.Text != "" && txtRazonSocial.Text != "")
                {
                    if (!ClasesBase.ABM.TrabajarProveedores.verificar_cod(txtCuit.Text))
                    {
                        WinConfirms.WinConfirmacion winConf = new WinConfirms.WinConfirmacion();
                        winConf._Dialog = "Se requiere confirmar la operación: Alta de Proveedor";
                        winConf.ShowDialog();

                            if (winConf.DialogResult.Value)
                            {
                                Proveedor proveedor = new Proveedor();
                                proveedor.Prov_RazonSocial = txtRazonSocial.Text;
                                proveedor.Prov_Domicilio = txtDomicilio.Text;
                                proveedor.Prov_Cuit = txtCuit.Text;
                                proveedor.Prov_Telefono = txtTelefono.Text;

                                listaProveedor[Vista.CurrentPosition] = proveedor;
                                ClasesBase.ABM.TrabajarProveedores.insert_proveedor(proveedor);
                                ActividadLog actlog = new ActividadLog(Menu.Usu.Usu_Id, "Proveedor insertado: " + proveedor.ToString(), DateTime.Now.ToString("HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd"));
                                ClasesBase.ABM.TrabajarActLog.insert_actividad(actlog);
                        
                                MessageBox.Show("La operación se realizó correctamente" + "\nCantidad de elementos: " + Vista.Count,
                                    "Alta de Proveedor", MessageBoxButton.OK, MessageBoxImage.Information);

                                Vista.MoveCurrentToLast();
                                btnGuardar.IsEnabled = false;
                                btnCancelar.IsEnabled = false;
                                cambiar_estado_textboxs(false);
                                cambiar_estado_operaciones(true);
                                cambiar_estado_navegacion(true);
                                elementoCreado = false;
                            }
                    }
                    else
                    {
                        MessageBox.Show("El CUIT ya se encuentra en uso", "Modificar Proveedor", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Complete correctamente los campos", "Alta de Proveedor", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            if (this.elementoModificado)
            {
                if (txtDomicilio.Text != "" && txtTelefono.Text != "" && txtCuit.Text != "" && txtRazonSocial.Text != "")
                {
                        WinConfirms.WinConfirmacion winConf = new WinConfirms.WinConfirmacion();
                        winConf._Dialog = "Se requiere confirmar la operación: Modificar Proveedor";
                        winConf.ShowDialog();

                        if (winConf.DialogResult.Value)
                        {
                            listaProveedor[Vista.CurrentPosition].Prov_Cuit = txtCuit.Text;
                            listaProveedor[Vista.CurrentPosition].Prov_Domicilio = txtDomicilio.Text;
                            listaProveedor[Vista.CurrentPosition].Prov_RazonSocial = txtRazonSocial.Text;
                            listaProveedor[Vista.CurrentPosition].Prov_Telefono = txtTelefono.Text;
                            ClasesBase.ABM.TrabajarProveedores.update_proveedor(listaProveedor[Vista.CurrentPosition]);
                            ActividadLog actlog = new ActividadLog(Menu.Usu.Usu_Id, "Cliente modificado: " + listaProveedor[Vista.CurrentPosition].ToString(), DateTime.Now.ToString("HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd"));
                            ClasesBase.ABM.TrabajarActLog.insert_actividad(actlog);

                            MessageBox.Show("La operación se realizó correctamente" + "\nCantidad de elementos: " + Vista.Count,
                                "Modificacion de Proveedor", MessageBoxButton.OK, MessageBoxImage.Information);

                            Vista.MoveCurrentToLast();
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

        private void mnEliminar_Click(object sender, RoutedEventArgs e)
        {
            WinConfirms.WinConfirmacion winConf = new WinConfirms.WinConfirmacion();
            winConf._Dialog = "Se requiere confirmar la operación: Eliminar Vendedor";
            winConf.ShowDialog();
            if (winConf.DialogResult.Value)
            {
                ActividadLog actlog = new ActividadLog(Menu.Usu.Usu_Id, "Proveedor eliminado: " + listaProveedor[Vista.CurrentPosition].ToString(), DateTime.Now.ToString("HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd"));
                ClasesBase.ABM.TrabajarActLog.insert_actividad(actlog);
                ClasesBase.ABM.TrabajarProveedores.delete_proveedor(listaProveedor[Vista.CurrentPosition].Prov_Cuit);
                listaProveedor.RemoveAt(Vista.CurrentPosition);

                MessageBox.Show("La operación se realizó correctamente" + "\nCantidad de elementos: " + Vista.Count, "Eliminar Proveedor", MessageBoxButton.OK, MessageBoxImage.Information);
                
                verificar_cantidad_elementos();
            }
        }

        private void mnModificar_Click(object sender, RoutedEventArgs e)
        {
            this.elementoModificado = true;
            txtCuit.Focus();
            btnGuardar.IsEnabled = true;
            btnCancelar.IsEnabled = true;
            cambiar_estado_operaciones(false);
            cambiar_estado_textboxs(true);
            txtCuit.IsReadOnly = true;
            cambiar_estado_navegacion(false);
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (this.elementoCreado)
            {
                listaProveedor.RemoveAt(Vista.CurrentPosition);
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

        private void eventVistaProveedor_Filter(Object sender, FilterEventArgs e)
        {
            Proveedor prov = e.Item as Proveedor;
            if (txtPatron != null && txtPatron.Text != "")
                if (prov.Prov_RazonSocial.StartsWith(txtPatron.Text, StringComparison.CurrentCultureIgnoreCase))
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
        }

        private void txtPatron_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (vistaFiltrada != null)
            {
                vistaFiltrada.Filter += eventVistaProveedor_Filter;
            }
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
            txtCuit.IsReadOnly = !estado;
            txtDomicilio.IsReadOnly = !estado;
            txtRazonSocial.IsReadOnly = !estado;
            txtTelefono.IsReadOnly = !estado;
        }

        public void cambiar_estado_navegacion(bool estado)
        {
            btnPrimero.IsEnabled = estado;
            btnSiguiente.IsEnabled = estado;
            btnAnterior.IsEnabled = estado;
            btnUltimo.IsEnabled = estado;
        }

        public void cambiar_estado_operaciones(bool estado)
        {
            mnEliminar.IsEnabled = estado;
            mnModificar.IsEnabled = estado;
            mnNuevo.IsEnabled = estado;
            txtPatron.IsEnabled = estado;
        }

        public void verificar_cantidad_elementos()
        {
            if (Vista.Count <= 0)
            {
                mnModificar.IsEnabled = false;
                mnEliminar.IsEnabled = false;
                cambiar_estado_navegacion(false);

                MessageBox.Show("No hay elementos en el regitro de Proveedores", "ABM de Proveedores", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        //Solo entrada de numeros(naturales, decimales)
        private void verificar_textbox_numerico(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
