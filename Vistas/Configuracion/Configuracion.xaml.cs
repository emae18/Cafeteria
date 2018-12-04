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
using System.IO;
using System.Collections.ObjectModel;

namespace Vistas
{
    /// <summary>
    /// Lógica de interacción para Configuracion.xaml
    /// </summary>
    public partial class WinConfig : Window
    {
        private Window menu;
        bool skinModificado;
        private Usuario usuario;
        public WinConfig(Window menuInstance, Usuario usuarioLogueado)
        {
            InitializeComponent();
            App.cargar_estilos();
            cbSkins.ItemsSource = App.ListaEstilos;
            cbSkins.DisplayMemberPath = "Nombre";
            cbSkins.SelectedValuePath = "Ubicacion";
            cbSkins.SelectedIndex = App.getCurrentStyle();
            skinModificado = false;
            menu = menuInstance;
            this.usuario = usuarioLogueado;
            if (usuario.Usu_Rol != "Administrador")
            {
                tabHistorial.Visibility = Visibility.Hidden;                
            }
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (skinModificado)
            {
                WinConfirms.WinConfirmacion wConf = new WinConfirms.WinConfirmacion();
                wConf._Dialog = "¿Aplicar Cambios?";
                wConf.ShowDialog();
                if (wConf.DialogResult.Value)
                {
                    ClasesBase.ABM.TrabajarEstilo.update_estilo(cbSkins.SelectedIndex);
                    Application.Current.Resources.MergedDictionaries.Clear();
                    Application.Current.Resources.MergedDictionaries.Add((ResourceDictionary)cbSkins.SelectedValue);
                    MessageBox.Show("Se reiniciará la aplicación", "Información", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    menu.Close();
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }

        }

        private void cbSkins_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            skinModificado = true;
            lblEstilos.Content = "Estilos: *";
            foreach (Tema tema in App.ListaEstilos)
            {
                if (cbSkins.SelectedValue == tema.Ubicacion && tema.Estado == true)
                {
                    skinModificado = false;
                    lblEstilos.Content = "Estilos: ";
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnExportarAct_Click(object sender, RoutedEventArgs e)
        {
            string carpetaLogs = "C:\\DKMuebleria\\Logs\\Actividades";
            if (!System.IO.Directory.Exists(carpetaLogs))
            {
                System.IO.Directory.CreateDirectory(carpetaLogs);
            }
            else
            {
                string archivo = "C:\\DKMuebleria\\Logs\\Actividades\\log" + DateTime.Now.ToString("yyyy-MM-dd") + "_" + DateTime.Now.ToString("hh-mm-ss") + ".txt";
                using (var fileStream = File.Create(archivo))
                {
                    List<string> listaHLog = ClasesBase.ABM.TrabajarActLog.traerListadoActividades();
                    foreach (string i in listaHLog)
                    {
                        string datastring = i + Environment.NewLine;
                        var texto = new UTF8Encoding(true).GetBytes(datastring);
                        fileStream.Write(texto, 0, texto.Length);
                        fileStream.Flush();
                    }

                    MessageBox.Show("Log exportado correctamente en " + carpetaLogs);
                }
            }
        }

        private void btnExportarHLog_Click(object sender, RoutedEventArgs e)
        {
            string carpetaLogs = "C:\\DKMuebleria\\Logs\\Login";
            if (!System.IO.Directory.Exists(carpetaLogs))
            {
                System.IO.Directory.CreateDirectory(carpetaLogs);
            }
            else
            {
                string archivo = "C:\\DKMuebleria\\Logs\\Login\\log" + DateTime.Now.ToString("yyyy-MM-dd") + "_" + DateTime.Now.ToString("hh-mm-ss") + ".txt";
                using (var fileStream = File.Create(archivo))
                {                    
                    List<string> listaHLog = ClasesBase.ABM.TrabajarHLog.traerListadoHistorial();
                    foreach (string i in listaHLog)
                    {
                        string datastring = i + Environment.NewLine; 
                        var texto = new UTF8Encoding(true).GetBytes(datastring);
                        fileStream.Write(texto, 0, texto.Length);
                        fileStream.Flush();
                    }                    

                    MessageBox.Show("Log exportado correctamente en " + carpetaLogs);
                }                
            }             
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            ClasesBase.ABM.TrabajarHLog.clear_historial();
            lwHistorial.ItemsSource = null;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ClasesBase.ABM.TrabajarActLog.clear_historial();
            lwActividades.ItemsSource = null;
        }
    }
}
