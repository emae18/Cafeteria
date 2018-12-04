using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel; 
namespace ClasesBase
{
    public class Cliente: INotifyPropertyChanged
    {        
        private string cli_Dni;
        public string Cli_Dni
        {
            get { return cli_Dni; }
            set {
                  cli_Dni = value;
                  OnPropertyChanged("Cli_Dni");
            }
        }

        private string cli_Apellido;

        public string Cli_Apellido
        {
            get { return cli_Apellido; }
            set { cli_Apellido = value;
                  OnPropertyChanged("Cli_Apellido");
                }
        }
        private string cli_Nombre;

        public string Cli_Nombre
        {
            get { return cli_Nombre; }
            set { cli_Nombre = value;
                  OnPropertyChanged("Cli_Nombre");
            }
        }
        private string cli_Direccion;

        public string Cli_Direccion
        {
            get { return cli_Direccion; }
            set { cli_Direccion = value;
                  OnPropertyChanged("Cli_Direccion");
            }
        }
        public Cliente(string dni, string apellido, string nombre, string direccion ) {
            this.cli_Apellido = apellido;
            this.cli_Direccion = direccion;
            this.cli_Dni = dni;
            this.cli_Nombre = nombre;
        }

        public Cliente(){}

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string info){    
            if (PropertyChanged != null)  
              {                  
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public override string ToString()
        {
            return "[Nombre y Apellido]: " + Cli_Apellido + " [DNI]: " + Cli_Dni + " [DIRECCION]: " + Cli_Direccion;
        }
    }
}
