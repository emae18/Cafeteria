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
using Microsoft.Win32;


namespace Vistas
{
    /// <summary>
    /// Lógica de interacción para Presentacion.xaml
    /// </summary>
    public partial class Presentacion : Window
    {
        public Presentacion()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string pathMp3;
            pathMp3 = @"../../Resources/Sounds/au.mp3";
            meAudio.LoadedBehavior = MediaState.Play;
            meAudio.Source = new Uri(pathMp3, UriKind.Relative);
            btnInicio.Focus();
        }

        private void btnInicio_Click(object sender, RoutedEventArgs e)
        {           
            WinLogin wLg = new WinLogin();
            wLg.Show();
            this.Close();            
        }     
    }
}
