using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClasesBase
{
    public class Proveedor: INotifyPropertyChanged
    {
        public Proveedor(string cuit, string raSocial, string domicilio, string telefono)
        {
            this.prov_Cuit = cuit;
            this.prov_Domicilio = domicilio;
            this.prov_RazonSocial = raSocial;
            this.prov_Telefono = telefono;
        }
        public Proveedor()
        {
        }
        private string prov_Cuit;

        public string Prov_Cuit
        {
            get { return prov_Cuit; }
            set { prov_Cuit = value; Notificador("Prov_Cuit"); }
        }
        private string prov_RazonSocial;

        public string Prov_RazonSocial
        {
            get { return prov_RazonSocial; }
            set { prov_RazonSocial = value; Notificador("Prov_RazonSocial"); }
        }
        private string prov_Domicilio;

        public string Prov_Domicilio
        {
            get { return prov_Domicilio; }
            set { prov_Domicilio = value; Notificador("Prov_Domicilio"); }
        }
        private string prov_Telefono;

        public string Prov_Telefono
        {
            get { return prov_Telefono; }
            set { prov_Telefono = value; Notificador(prov_Telefono); }
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
            return "[CUIT]: " + Prov_Cuit + " [Razon Social]: " + Prov_RazonSocial + " [Domicilio]: " + Prov_Domicilio + " [Telefono]: " + Prov_Telefono;
        }
    }
}
