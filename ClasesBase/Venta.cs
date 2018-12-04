using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClasesBase
{
    public class Venta : INotifyPropertyChanged
    {
        private int venta_NroFactura;

        public int Venta_NroFactura
        {
            get { return venta_NroFactura; }
            set
            {
                venta_NroFactura = value;
                Notificador("Venta_NroFactura");
            }
        }
        private DateTime venta_FechaFactura;

        public DateTime Venta_FechaFactura
        {
            get { return venta_FechaFactura; }
            set
            {
                venta_FechaFactura = value;
                Notificador("Venta_FechaFactura");
            }
        }
        /*
        private string venta_CodProducto;

        public string Venta_CodProducto
        {
            get { return venta_CodProducto; }
            set { venta_CodProducto = value; Notificador("Venta_CodProducto"); }
        }
        
        private string venta_Legajo;

        public string Venta_Legajo
        {
            get { return venta_Legajo; }
            set { venta_Legajo = value; Notificador("Venta_Legajo"); }
        }
        private string venta_Dni;

        public string Venta_Dni
        {
            get { return venta_Dni; }
            set { venta_Dni = value; Notificador("Venta_Dni"); }
        }
        private string venta_Importe;

        public string Venta_Importe
        {
            get { return venta_Importe; }
            set { venta_Importe = value; }
        }*/


        private Vendedor venta_Vendedor;
        public Vendedor Venta_Vendedor
        {
            get
            {
                return venta_Vendedor;
            }
            set
            {
                this.venta_Vendedor = value;
                Notificador("Vendedor");
            }
        }

        private Producto venta_Producto;
        public Producto Venta_Producto
        {
            get
            {
                return venta_Producto;
            }
            set
            {
                venta_Producto = value;
                Notificador("Producto");
            }
        }

        private Cliente venta_Cliente;
        public Cliente Venta_Cliente
        {
            get
            {
                return venta_Cliente;
            }
            set
            {
                venta_Cliente = value;
                Notificador("Cliente");
            }
        }

        private decimal venta_ImporteTotal;

        public decimal Venta_ImporteTotal
        {
            get { return venta_ImporteTotal; }
            set
            {
                venta_ImporteTotal = decimal.Round(value, 2, MidpointRounding.AwayFromZero); ;
            Notificador("Venta_ImporteTotal");
            }
        }

        public Venta(int nroFact, DateTime fecFact, Producto prod, Vendedor vend, Cliente cli,decimal importeTotal)
        {
            this.Venta_NroFactura = nroFact;
            this.Venta_FechaFactura = fecFact;
            this.Venta_Producto = prod;
            this.Venta_Vendedor = vend;
            this.Venta_Cliente = cli;
            this.Venta_ImporteTotal = importeTotal;
        }

        public Venta() 
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
            return "[NFAC]: " + Venta_NroFactura + " [COD_PROD]: " + Venta_Producto.Prod_Cod + " [DNI]: " + Venta_Cliente.Cli_Dni + " [LEGAJO]: " + Venta_Vendedor.Vend_Legajo + " [FECHA]: " + Venta_FechaFactura + " [IMPORTE:] " + Venta_ImporteTotal;
        }
    }
}
