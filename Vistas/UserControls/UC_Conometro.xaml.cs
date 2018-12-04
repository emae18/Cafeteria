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

namespace Vistas.UserControls
{
    /// <summary>
    /// Lógica de interacción para UC_Conometro.xaml
    /// </summary>
    public partial class UC_Conometro : UserControl
    {
        private System.Windows.Threading.DispatcherTimer dTimer = new System.Windows.Threading.DispatcherTimer();
        private int segundos;
        public int _segundos
        {
            get { return segundos; }
            set { segundos = value; }
        }

        private int minuto;
        public int _minuto
        {
            get { return minuto; }
            set { minuto = value; }
        }

        private int hora;
        public int _hora
        {
            get { return hora; }
            set { hora = value; }
        }

        private string duracion;
        public string Duracion
        {
            get { return duracion; }
            set { duracion = value; }
        }

        public UC_Conometro()
        {
            InitializeComponent();
            _segundos = 0;
            _minuto = 0;
            _hora = 0;
            txtTimer.Text = "00:00:00";
            dTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dTimer.Interval = new TimeSpan(0, 0, 1);
            dTimer.Start();
        }     

        public void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            string mostrarSegundos;
            string mostrarMinutos;
            string mostrarHoras;
            // Segundos            
            if (_segundos >= 59)
            {
                _segundos = 0;
                mostrarSegundos = "00";
                _minuto++;
            }
            else 
            {
                _segundos++;
                
                if (_segundos <= 9)
                {
                    mostrarSegundos = "0" + _segundos;
                }
                else 
                {
                    mostrarSegundos = _segundos.ToString();
                }
            }
            // Minutos
            if (_minuto >= 59)
            {
                _minuto = 0;
                mostrarMinutos = "00";
                _hora++;
            }
            else
            {
                if (_minuto <= 9)
                {
                    mostrarMinutos = "0" + _minuto;
                }
                else
                {
                    mostrarMinutos = _minuto.ToString();
                }
            }
            //Horas
            if (_hora >= 59)
            {
                mostrarHoras = "59";
            }
            else
            {
                if (_hora <= 9)
                {
                    mostrarHoras = "0" + _hora;
                }
                else
                {
                    mostrarHoras = _hora.ToString();
                }
            }
            // Resultado
            Duracion = mostrarHoras + ":" + mostrarMinutos + ":" + mostrarSegundos;
            txtTimer.Text = Duracion;
        }
    }
}
