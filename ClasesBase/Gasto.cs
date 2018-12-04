using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace ClasesBase
{
    public class Gasto : INotifyPropertyChanged
    {
        private string gasto_Descripcion;

        public string Gasto_Descripcion
        {
            get { return gasto_Descripcion; }
            set { gasto_Descripcion = value;
            Notificador("Gasto_Descripcion");
            }
        }
        private double gasto_Precio;

        public double Gasto_Precio
        {
            get { return gasto_Precio; }
            set { gasto_Precio = value;
            Notificador("Gasto_Precio");
            }
        }
        public Gasto()
        {

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
            return "[Descripcion]: " +  Gasto_Descripcion + " [IMPORTE:] " + Gasto_Precio;
        }
    }
}
