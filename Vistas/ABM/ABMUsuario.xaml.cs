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

namespace Vistas.ABM
{
    /// <summary>
    /// Lógica de interacción para ABMUsuario.xaml
    /// </summary>
    public partial class ABMUsuario : Window
    {
        Usuario usuarioLogueado;
        private bool usuarioModificado = false;

        public ABMUsuario(Usuario usuario)
        {
            InitializeComponent();
            setUsuarioLogueado(usuario);
            load_textboxes();
            ucUserLogueado.setUsuarioLogueado(usuario);
            cambiar_estodos_textboxes(false);
        }

        public void setUsuarioLogueado(Usuario usuario)
        {
            this.usuarioLogueado = usuario;
        }

        public void load_textboxes()
        {
            txtNombre.Text = usuarioLogueado.Usu_Nombre;
            txtApellido.Text = usuarioLogueado.Usu_Apellido;
            txtNomUsu.Text = usuarioLogueado.Usu_NombreUsuario;
            txtContraseña.Password = usuarioLogueado.Usu_Contraseña;
            txtRol.Text = usuarioLogueado.Usu_Rol;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (usuarioModificado)
            {
                if (txtNombre.Text != "" && txtApellido.Text != "" && txtNomUsu.Text != "" && txtContraseña.Password != "")
                {
                    if (txtContraseña.Password.Equals(txtRepetirContraseña.Password))
                    {
                        WinConfirms.WinConfirmacion wConfirmacion = new WinConfirms.WinConfirmacion();
                        wConfirmacion._Dialog = "Modificar usuario: ¿Estás Seguro?";
                        wConfirmacion.ShowDialog();
                        if (wConfirmacion.DialogResult.Value)
                        {
                            Usuario usuario = new Usuario();
                            usuario.Usu_Id = usuarioLogueado.Usu_Id;
                            usuario.Usu_Nombre = txtNombre.Text;
                            usuario.Usu_Apellido = txtApellido.Text;
                            usuario.Usu_NombreUsuario = txtNomUsu.Text;
                            usuario.Usu_Contraseña = txtContraseña.Password;

                            ClasesBase.ABM.TrabajarUsuario.update_usuario(usuario);
                            cambiar_estodos_textboxes(false);
                            MessageBox.Show("La operacion se realizó correctamente \nDebe reiniciar la aplicación para que los cambios surtan efecto", "Modificar usuario", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Las contraseñas no coinciden", "Modificar usuario", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Complete correctamente los campos", "Modificar usuario", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                this.Close();
            }            
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {            
            convertir_a_eliminar();
        }

        private void convertir_a_eliminar()
        {
            if (usuarioModificado == false)
            {
                Uri uri = new Uri("/Vistas;component/Resources/Images/Icons/dark/appbar.cancel.png", UriKind.Relative);
                ImageSource imgS = new BitmapImage(uri);
                btnModificar.Tag = imgS;
                cambiar_estodos_textboxes(true);
                usuarioModificado = true;
            }
            else
            {
                Uri uri = new Uri("/Vistas;component/Resources/Images/Icons/dark/appbar.edit.png", UriKind.Relative);
                ImageSource imgS = new BitmapImage(uri);
                btnModificar.Tag = imgS;
                cambiar_estodos_textboxes(false);
                usuarioModificado = false;
            }
            
        }

        private void cambiar_estodos_textboxes(bool estado)
        { 
            txtApellido.IsReadOnly = !estado;
            txtContraseña.IsEnabled = estado;
            txtNombre.IsReadOnly = !estado;
            txtNomUsu.IsReadOnly = !estado;
            txtRepetirContraseña.IsEnabled = estado;
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnAceptar_Click(sender, e);
            }            
        }
    }
}
