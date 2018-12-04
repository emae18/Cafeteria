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
using System.Windows.Threading;
using Microsoft.Win32;
using System.Windows.Controls.Primitives;

namespace Vistas.Reproductor
{
    /// <summary>
    /// Interaction logic for winVideo.xaml
    /// </summary>
    public partial class winVideo : Window
    {
        public winVideo()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private bool mediaPlayerIsPlaying = false;
        private bool userIsDraggingSlider = false;
        private bool isFullScreen = false;
        DispatcherTimer timer;
                
        private void timer_Tick(object sender, EventArgs e)
        {
            if((mePlayer.Source != null) && (mePlayer.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            {
                sliProgress.Minimum = 0;
                sliProgress.Maximum = mePlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliProgress.Value = mePlayer.Position.TotalSeconds;
            }
        }

        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de Video (*.mkv; *.mp4;*.mp3;*.mpg;*.mpeg)|*.mp4;*.mp3;*.mpg;*.mpeg|All files (*.*)|*.*";
            if(openFileDialog.ShowDialog() == true && openFileDialog.FileName != "")
                    mePlayer.Source = new Uri(openFileDialog.FileName);
                    
                    Play_Executed(sender, e);
        }

        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
                e.CanExecute = (mePlayer != null) && (mePlayer.Source != null);
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Play();
            btnAvanzar.IsEnabled = true;
            btnRetroceder.IsEnabled = true;
            btnPause.IsEnabled = true;
            mediaPlayerIsPlaying = true;
            visibilidad_controles_video();
        }

        private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Pause();
            btnPause.IsEnabled = false;
        }

        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Stop();
            btnAvanzar.IsEnabled = false;
            btnRetroceder.IsEnabled = false;
            mediaPlayerIsPlaying = false;              
        }

        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }

        private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            mePlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            mePlayer.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }                

        private void btnRetroceder_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan sp = new TimeSpan(0,0,5);
            mePlayer.Position = mePlayer.Position -  sp;
        }

        private void btnAvanzar_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan sp = new TimeSpan(0, 0, 5);
            mePlayer.Position = mePlayer.Position + sp;
        }

        private void btnFullScreen_Click(object sender, RoutedEventArgs e)
        {
            if (isFullScreen)
            {
                this.WindowState = WindowState.Normal;
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.ResizeMode = ResizeMode.CanResize;
                isFullScreen = false;
            }
            else
            {
                this.ResizeMode = ResizeMode.NoResize;
                this.WindowState = WindowState.Maximized;
                this.WindowStyle = WindowStyle.None;                        
                isFullScreen = true;
            }
                    
        }

        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            visibilidad_controles_video();
        }

        public void visibilidad_controles_video()
        {
            // Visibilidad de los controles de video
            gridSB.Visibility = Visibility.Visible;
            btnOpen.Visibility = Visibility.Visible;
            if (timer != null)
            {
                timer.Tick -= timer_statusBar;
            }
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Tick += new EventHandler(timer_statusBar);
            timer.Start();
        }

        public void timer_statusBar(object sender, EventArgs e)
        {
            // Contador antes de ocultar controles de video
            if (gridSB.IsVisible && mediaPlayerIsPlaying)
            {
                gridSB.Visibility = Visibility.Hidden;
                btnOpen.Visibility = Visibility.Hidden;
                //System.Console.WriteLine("Mouse Quieto");
            }
        }

        private void mePlayer_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Doble click, pantalla completa, al contrario si lo está.
            if (e.ClickCount == 2)
            {
                if (isFullScreen)
                {
                    this.WindowState = WindowState.Normal;
                    this.WindowStyle = WindowStyle.SingleBorderWindow;
                    this.ResizeMode = ResizeMode.CanResize;
                    isFullScreen = false;
                }
                else
                {
                    this.ResizeMode = ResizeMode.NoResize;
                    this.WindowState = WindowState.Maximized;
                    this.WindowStyle = WindowStyle.None;
                    isFullScreen = true;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mePlayer.Source = new Uri(@"../../Resources/Videos/naturaleza.mp4", UriKind.Relative);
            mePlayer.Play();
            mediaPlayerIsPlaying = true;
            visibilidad_controles_video();
        }
    }
}
