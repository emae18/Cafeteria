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
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
namespace Vistas.ABM
{
    /// <summary>
    /// Interaction logic for ABMVendedor.xaml
    /// </summary>
    public partial class ABMVendedor : Window
    {
        CollectionView Vista;
        ObservableCollection<Vendedor> listaVendedor;
        private CollectionViewSource vistaFiltrada;
        private bool elementoCreado;
        private bool elementoModificado;
        //private Vendedor a = new Vendedor();
        
        public ABMVendedor()
        {
            InitializeComponent();
            vistaFiltrada = Resources["VISTA_VEN"] as CollectionViewSource;
            txtPatron.Text = "";
            this.elementoModificado = false;
            this.elementoCreado = false;
            cambiar_estado_textboxs(false);
            btnGuardar.IsEnabled = false;
            btnCancelar.IsEnabled = false;
        }
        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObjectDataProvider odp = (ObjectDataProvider)this.Resources["LIST_VEN"];
            listaVendedor = odp.Data as ObservableCollection<Vendedor>;
            Vista = (CollectionView)CollectionViewSource.GetDefaultView(grid_content.DataContext);

            mnNuevo.Focus();
            verificar_cantidad_elementos();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            cambiar_estado_textboxs(true);
            txtLegajo.Focus();
            Vendedor ven = new Vendedor();
            elementoCreado = true;

            listaVendedor.Add(ven);
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
                if (txtApellido.Text != "" && txtLegajo.Text != "" && txtNombre.Text != "")
                {
                    if (!ClasesBase.ABM.TrabajarVendedor.verificar_cod(txtLegajo.Text))
                    {
                        WinConfirms.WinConfirmacion winConf = new WinConfirms.WinConfirmacion();
                        winConf._Dialog = "Se requiere confirmar la operación: Alta de Vendedor";
                        winConf.ShowDialog();
                        if (winConf.DialogResult.Value)
                        {
                            Vendedor ven = new Vendedor();
                            ven.Vend_Nombre = txtNombre.Text;
                            ven.Vend_Apellido = txtApellido.Text;
                            ven.Vend_Legajo = txtLegajo.Text;
                            listaVendedor[Vista.CurrentPosition] = ven;
                            ClasesBase.ABM.TrabajarVendedor.insert_Vendedor(ven);
                            ActividadLog actlog = new ActividadLog(Menu.Usu.Usu_Id, "Vendedor insertado: " + ven.ToString(), DateTime.Now.ToString("HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd"));
                            ClasesBase.ABM.TrabajarActLog.insert_actividad(actlog);

                            MessageBox.Show("La operación se realizó correctamente" + "\nCantidad de elementos: " + Vista.Count,
                                "Alta de Vendedor", MessageBoxButton.OK, MessageBoxImage.Information);

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
                        MessageBox.Show("El Legajo ya se encuentra en uso", "Alta de Vendedor", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Complete correctamente los campos", "Alta de Vendedor", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }

            if (this.elementoModificado)
            {
                if (txtApellido.Text != "" && txtLegajo.Text != "" && txtNombre.Text != "")
                {
                    WinConfirms.WinConfirmacion winConf = new WinConfirms.WinConfirmacion();
                    winConf._Dialog = "Se requiere confirmar la operación: Modificar Vendedor";
                    winConf.ShowDialog();

                    if (winConf.DialogResult.Value)
                    {
                        listaVendedor[Vista.CurrentPosition].Vend_Nombre = txtNombre.Text;
                        listaVendedor[Vista.CurrentPosition].Vend_Apellido = txtApellido.Text;
                        listaVendedor[Vista.CurrentPosition].Vend_Legajo = txtLegajo.Text;
                        ClasesBase.ABM.TrabajarVendedor.modificar_Vendedor(listaVendedor[Vista.CurrentPosition]);
                        ActividadLog actlog = new ActividadLog(Menu.Usu.Usu_Id, "Vendedor modificado: " + listaVendedor[Vista.CurrentPosition].ToString(), DateTime.Now.ToString("HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd"));
                        ClasesBase.ABM.TrabajarActLog.insert_actividad(actlog);

                        MessageBox.Show("La operación se realizó correctamente" + "\nCantidad de elementos: " + Vista.Count,
                            "Modificacion de Vendedor", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    MessageBox.Show("Complete correctamente los campos", "Modificar Vendedor", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            WinConfirms.WinConfirmacion winConf = new WinConfirms.WinConfirmacion();
            winConf._Dialog = "Se requiere confirmar la operación: Eliminar Vendedor";
            winConf.ShowDialog();
            if (winConf.DialogResult.Value)
            {
                ActividadLog actlog = new ActividadLog(Menu.Usu.Usu_Id, "Vendedor eliminado: " + listaVendedor[Vista.CurrentPosition].ToString(), DateTime.Now.ToString("HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd"));
                ClasesBase.ABM.TrabajarActLog.insert_actividad(actlog);
                ClasesBase.ABM.TrabajarVendedor.delete_Vendedor(listaVendedor[Vista.CurrentPosition].Vend_Legajo);
                listaVendedor.RemoveAt(Vista.CurrentPosition);

                MessageBox.Show("La operación se realizó correctamente" + "\nCantidad de elementos: " + Vista.Count, "Eliminar Vendedor", MessageBoxButton.OK, MessageBoxImage.Information);

                verificar_cantidad_elementos();
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            this.elementoModificado = true;
            txtApellido.Focus();
            btnGuardar.IsEnabled = true;
            btnCancelar.IsEnabled = true;

            cambiar_estado_operaciones(false);
            cambiar_estado_textboxs(true);
            txtLegajo.IsReadOnly = true;
            cambiar_estado_navegacion(false);
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (this.elementoCreado)
            {
                listaVendedor.RemoveAt(Vista.CurrentPosition);
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

        private void eventVistaVendedor_Filter(Object sender, FilterEventArgs e)
        {
            Vendedor ven = e.Item as Vendedor;
            if (txtPatron.Text != "")
                if (ven.Vend_Apellido.StartsWith(txtPatron.Text, StringComparison.CurrentCultureIgnoreCase))
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
                vistaFiltrada.Filter += eventVistaVendedor_Filter;
            }
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
            txtLegajo.IsReadOnly = !estado;
            txtNombre.IsReadOnly = !estado;
            txtApellido.IsReadOnly = !estado;
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

        public void verificar_cantidad_elementos()
        {
            if (Vista.Count <= 0)
            {
                mnModificar.IsEnabled = false;
                mnEliminar.IsEnabled = false;
                cambiar_estado_navegacion(false);
                MessageBox.Show("No hay elementos en el regitro de Vendedores", "ABM de Vendedores", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        public void des(Vendedor a)
        {
            for (int i = 0; i < listaVendedor.Count; i++)
            {
                if (listaVendedor[i].Vend_Legajo.Equals(a.Vend_Legajo))
                {
                    Vista.MoveCurrentToPosition(i);
                }

            }            
        }
    }

}
