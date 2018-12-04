using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows;

namespace ClasesBase
{
    public class Estilo
    {
        private string est_Nombre;

        public string Est_Nombre
        {
            get { return est_Nombre; }
            set { est_Nombre = value; }
        }
        private int est_Id;

        public int Est_Id
        {
            get { return est_Id; }
            set { est_Id = value; }
        }
        private string est_Ubicacion;

        public string Est_Ubicacion
        {
            get { return est_Ubicacion; }
            set { est_Ubicacion = value; }
        }
        private string est_Estado;

        public string Est_Estado
        {
            get { return est_Estado; }
            set { est_Estado = value; }
        }
        public Estilo() { }
        public Estilo(int id, string nombre, string ubicacion, string estado) 
        {
            this.est_Id = id;
            this.est_Nombre = nombre;
            this.est_Ubicacion = ubicacion;
            this.est_Estado = estado;
        }

    }
}
