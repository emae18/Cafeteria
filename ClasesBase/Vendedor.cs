using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClasesBase
{
    public class Vendedor: INotifyPropertyChanged
    {
        private string vend_Legajo;

        public string Vend_Legajo
        {
            get { return vend_Legajo; }
            set { vend_Legajo = value; Notificador("Vend_Legajo"); }
        }
        private string vend_Apellido;

        public string Vend_Apellido
        {
            get { return vend_Apellido; }
            set { vend_Apellido = value; Notificador("Vend_Apellido"); }
        }
        private string vend_Nombre;

        public string Vend_Nombre
        {
            get { return vend_Nombre; }
            set { vend_Nombre = value; Notificador("Vend_Nombre"); }
        }

        public Vendedor()
        {
        }

        public Vendedor(string leg, string apellido, string nombre)
        {
            this.vend_Apellido = apellido;
            this.vend_Legajo = leg;
            this.vend_Nombre = nombre;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Notificador(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        public override string ToString()
        {
            return "[Legajo]: " + Vend_Legajo + " [Apellido]: " + Vend_Apellido + " [Nombres]: " + Vend_Nombre;
        }
    }
}
