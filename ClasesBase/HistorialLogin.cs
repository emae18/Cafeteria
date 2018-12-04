using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class HistorialLogin
    {
        private int hlog_Id;

        public int Hlog_Id
        {
            get { return hlog_Id; }
            set { hlog_Id = value; }
        }
        private int usu_Id;

        public int Usu_Id
        {
            get { return usu_Id; }
            set { usu_Id = value; }
        }
        private string hlog_hora;

        public string Hlog_hora
        {
            get { return hlog_hora; }
            set { hlog_hora = value; }
        }
        private string hlog_fecha;

        public string Hlog_fecha
        {
            get { return hlog_fecha; }
            set { hlog_fecha = value; }
        }
        private string hlog_duracion;

        public string Hlog_duracion
        {
            get { return hlog_duracion; }
            set { hlog_duracion = value; }
        }

        public HistorialLogin(int usu_id, string hora, string fecha, string duracion)
        {            
            this.usu_Id = usu_id;
            this.hlog_hora = hora;
            this.hlog_fecha = fecha;
            this.hlog_duracion = duracion;
        }

        public HistorialLogin() { }
    }
}
