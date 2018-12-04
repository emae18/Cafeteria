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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClasesBase;
using System.IO;

namespace Vistas.UserControls
{
    /// <summary>
    /// Lógica de interacción para UserControl1.xaml
    /// </summary>
    public partial class UCUserLogueado : UserControl
    {
        public UCUserLogueado()
        {
            InitializeComponent();
        }

        public void setUsuarioLogueado(Usuario usuario)
        {
            txtUsuario.Text  = usuario.Usu_Nombre + " " + usuario.Usu_Apellido + " (" + usuario.Usu_NombreUsuario + ")" ;
            txtRol.Text = usuario.Usu_Rol;
            usuImg.Source = LoadImage(usuario.Usu_Image);
        }

        private BitmapSource LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;    
        }
    }
}
