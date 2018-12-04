using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class VentaDetalle
    {

        private int vd_NroFactura;

        public int Vd_NroFactura
        {
            get { return vd_NroFactura; }
            set { vd_NroFactura = value; }
        }
        private string vd_CodProducto;

        public string Vd_CodProducto
        {
            get { return vd_CodProducto; }
            set { vd_CodProducto = value; }
        }
        private int vd_Cantidad;

        public int Vd_Cantidad
        {
            get { return vd_Cantidad; }
            set { vd_Cantidad = value; }
        }

        private decimal vd_Precio;

        public decimal Vd_Precio
        {
            get { return vd_Precio; }
            set { vd_Precio = value; }
        }


        private decimal vd_Importe;

        public decimal Vd_Importe
        {
            get { return vd_Importe; }
            set { vd_Importe = value; }
        }

        


    }
}
