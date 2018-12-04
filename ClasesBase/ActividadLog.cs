using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class ActividadLog
    {
        private int actLog_Id;

        public int ActLog_Id
        {
            get { return actLog_Id; }
            set { actLog_Id = value; }
        }

        private int usu_Id;

        public int Usu_Id
        {
            get { return usu_Id; }
            set { usu_Id = value; }
        }

        private string actLog_Descripcion;

        public string ActLog_Descripcion
        {
            get { return actLog_Descripcion; }
            set { actLog_Descripcion = value; }
        }

        private string actLog_hora;

        public string ActLog_hora
        {
            get { return actLog_hora; }
            set { actLog_hora = value; }
        }

        private string actLog_fecha;

        public string ActLog_fecha
        {
            get { return actLog_fecha; }
            set { actLog_fecha = value; }
        }

        public ActividadLog() { }
        public ActividadLog(int usu, string descripcion, string hora, string fecha)
        {
            this.usu_Id = usu;
            this.actLog_Descripcion = descripcion;
            this.ActLog_hora = hora;
            this.ActLog_fecha = fecha;
        }
    }
}
