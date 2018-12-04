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
using System.Collections.ObjectModel;
using System.ComponentModel;
using ClasesBase;
using System.Text.RegularExpressions;

namespace Vistas.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMCliente.xaml
    /// </summary>
    public partial class ABMCliente : Window
    {
        private bool elementoCreado;
        private bool elemendoModificado;

        CollectionView Vista;
        ObservableCollection<Cliente> listaCliente;
        private CollectionViewSource vistaFiltrada;

        public ABMCliente()
        {
            InitializeComponent();

            vistaFiltrada = Resources["VISTA_CLI"] as CollectionViewSource;
            txtPatronApellido.Text = "";

            this.elemendoModificado = false;
            this.elementoCreado = false;

            cambiar_estado_textboxs(false);

            btnGuardar.IsEnabled = false;
            btnCancelar.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObjectDataProvider odp = (ObjectDataProvider)this.Resources["LIST_CLI"];
            listaCliente = odp.Data as ObservableCollection<Cliente>;

            Vista = (CollectionView)CollectionViewSource.GetDefaultView(grid_content.DataContext);

            mnNuevo.Focus();
            verificar_cantidad_elementos();
        }        

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (this.elementoCreado)
            {
                if (txtApellido.Text != "" && txtDireccion.Text != "" && txtDni.Text != "" && txtNombre.Text != "")
                {
                    if (!ClasesBase.ABM.TrabajarCliente.verificar_cod(txtDni.Text))
                    {
                        WinConfirms.WinConfirmacion winConf = new WinConfirms.WinConfirmacion();
                        winConf._Dialog = "Se requiere confirmar la operación: Alta de Cliente";
                        winConf.ShowDialog();

                        if (winConf.DialogResult.Value)
                        {
                            Cliente cliente = new Cliente();
                            cliente.Cli_Nombre = txtNombre.Text;
                            cliente.Cli_Apellido = txtApellido.Text;
                            cliente.Cli_Dni = txtDni.Text;
                            cliente.Cli_Direccion = txtDireccion.Text;
                            listaCliente[Vista.CurrentPosition] = cliente;
                            ClasesBase.ABM.TrabajarCliente.insert_cliente(cliente);
                            ActividadLog actlog = new ActividadLog(Menu.Usu.Usu_Id, "Cliente insertado: " + cliente.ToString(), DateTime.Now.ToString("HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd"));
                            ClasesBase.ABM.TrabajarActLog.insert_actividad(actlog);

                            MessageBox.Show("La operación se realizó correctamente" + "\nCantidad de elementos: " + Vista.Count,
                                "Alta de Cliente", MessageBoxButton.OK, MessageBoxImage.Information);

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
                        MessageBox.Show("Este DNI ya se encuentra en uso", "Alta de Cliente", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else 
                {
                    MessageBox.Show("Complete correctamente los campos", "Alta de Cliente", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            if (this.elemendoModificado)
            {
                if (txtApellido.Text != "" && txtDireccion.Text != "" && txtDni.Text != "" && txtNombre.Text != "")
                {
                    WinConfirms.WinConfirmacion winConf = new WinConfirms.WinConfirmacion();
                    winConf._Dialog = "Se requiere confirmar la operación: Modificar Cliente";
                    winConf.ShowDialog();

                    if (winConf.DialogResult.Value)
                    {
                        listaCliente[Vista.CurrentPosition].Cli_Nombre = txtNombre.Text;
                        listaCliente[Vista.CurrentPosition].Cli_Apellido = txtApellido.Text;
                        listaCliente[Vista.CurrentPosition].Cli_Dni = txtDni.Text;
                        listaCliente[Vista.CurrentPosition].Cli_Direccion = txtDireccion.Text;
                        ClasesBase.ABM.TrabajarCliente.update_cliente(listaCliente[Vista.CurrentPosition]);
                        ActividadLog actlog = new ActividadLog(Menu.Usu.Usu_Id, "Cliente modificado: " + listaCliente[Vista.CurrentPosition].ToString(), DateTime.Now.ToString("HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd"));
                        ClasesBase.ABM.TrabajarActLog.insert_actividad(actlog);
                        
                        MessageBox.Show("La operación se realizó correctamente: " + "\nCantidad de elementos: " + Vista.Count,
                            "Modificacion de Cliente", MessageBoxButton.OK, MessageBoxImage.Information);

                        btnGuardar.IsEnabled = false;
                        btnCancelar.IsEnabled = false;
                        cambiar_estado_textboxs(false);
                        cambiar_estado_operaciones(true);
                        cambiar_estado_navegacion(true);
                        this.elemendoModificado = false;
                    }
                }
                else
                {
                    MessageBox.Show("Complete correctamente los campos", "Modificar Cliente", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private void mnNuevo_Click(object sender, RoutedEventArgs e)
        {
            cambiar_estado_textboxs(true);
            txtDni.Focus();
            Cliente cli = new Cliente();
            elementoCreado = true;

            listaCliente.Add(cli);
            Vista.MoveCurrentToLast();

            btnGuardar.IsEnabled = true;
            btnCancelar.IsEnabled = true;
            cambiar_estado_operaciones(false);
            cambiar_estado_navegacion(false);
        }

        private void mnModificar_Click(object sender, RoutedEventArgs e)
        {
            this.elemendoModificado = true;
            txtApellido.Focus();
            btnGuardar.IsEnabled = true;
            btnCancelar.IsEnabled = true;
            cambiar_estado_operaciones(false);
            cambiar_estado_textboxs(true);
            txtDni.IsReadOnly = true;
            cambiar_estado_navegacion(false);
        }

        private void mnEliminar_Click(object sender, RoutedEventArgs e)
        {
            WinConfirms.WinConfirmacion winConf = new WinConfirms.WinConfirmacion();
            winConf._Dialog = "Se requiere confirmar la operación: Eliminar Cliente";
            winConf.ShowDialog();
            if (winConf.DialogResult.Value)
            {
                ActividadLog actlog = new ActividadLog(Menu.Usu.Usu_Id, "Cliente eliminado: " + listaCliente[Vista.CurrentPosition].ToString(), DateTime.Now.ToString("HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd"));
                ClasesBase.ABM.TrabajarActLog.insert_actividad(actlog);
                ClasesBase.ABM.TrabajarCliente.delete_cliente(listaCliente[Vista.CurrentPosition].Cli_Dni);
                listaCliente.RemoveAt(Vista.CurrentPosition);

                MessageBox.Show("La operación se realizó correctamente" + "\nCantidad de elementos: " + Vista.Count, "Eliminar Cliente", MessageBoxButton.OK, MessageBoxImage.Information);
                
                verificar_cantidad_elementos();
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (this.elementoCreado)
            {
                listaCliente.RemoveAt(Vista.CurrentPosition);
                cambiar_estado_navegacion(true);
                this.elementoCreado = false;
            }
            if (this.elemendoModificado)
            {
                cambiar_estado_navegacion(true);
                this.elemendoModificado = false;
            }
            cambiar_estado_textboxs(false);

            btnGuardar.IsEnabled = false;
            btnCancelar.IsEnabled = false;

            cambiar_estado_operaciones(true);
        }

        private void eventVistaCliente_Filter(Object sender, FilterEventArgs e)
        {
            Cliente cli = e.Item as Cliente;
            if (txtPatronApellido.Text != "")
                if (cli.Cli_Apellido.StartsWith(txtPatronApellido.Text, StringComparison.CurrentCultureIgnoreCase))
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
        }

        private void txtPatronApellido_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (vistaFiltrada != null)
            {
                vistaFiltrada.Filter += eventVistaCliente_Filter;
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnVistaPrevia_Click(object sender, RoutedEventArgs e)
        {
            VistaPrevia vistaprev = new VistaPrevia(this.vistaFiltrada);
            vistaprev.Show();
        }

        public void verificar_cantidad_elementos()
        {
            if (Vista.Count <= 0)
            {
                mnModificar.IsEnabled = false;
                mnEliminar.IsEnabled = false;
                cambiar_estado_navegacion(false);
                MessageBox.Show("No hay elementos en el regitro de clientes","ABM de Cliente", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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

        private void verificar_textbox_numerico(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        void cambiar_estado_textboxs(bool estado)
        {
            txtDireccion.IsReadOnly = !estado;
            txtApellido.IsReadOnly = !estado;
            txtDni.IsReadOnly = !estado;
            txtNombre.IsReadOnly = !estado;

            if (estado)
            {
                txtEstado.Text = "Habilitado";
                txtEstado.Foreground = Brushes.LightGreen;
            }
            else
            {
                txtEstado.Text = "Deshabilitado";
                txtEstado.Foreground = Brushes.Red;
            }
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
        }  
    }
}
