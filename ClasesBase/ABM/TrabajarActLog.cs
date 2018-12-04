using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;

namespace ClasesBase.ABM
{
    public class TrabajarActLog
    {
        public static DataTable traerActividades()
        {
            try
            {
                SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True");
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Actividades_Log_View";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cnn;
                cnn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cnn.Close();
                return dt;
            }
            catch
            {
                DataTable dt = new DataTable();
                System.Console.WriteLine("ERROR EN BD");
                return dt;
            }
        }

        public static List<string> traerListadoActividades()
        {
            List<string> listaActividades = new List<string>();
            try
            {
                SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\Users\\Alon Nami\\Documents\\Visual Studio 2010\\Cafetería 2.0\\lpoo2grupo1\\ClasesBase\\muebleria.mdf';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
                SqlDataAdapter sda = new SqlDataAdapter("Select * from Actividades_Log_View", cn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                string aux;

                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    aux = "ID: " + dt.Rows[k][0].ToString() + " || ";
                    aux += "Usuario: " + dt.Rows[k][1].ToString() + " || ";
                    aux += "Descripcion: " + dt.Rows[k][2].ToString() + " || ";
                    aux += "Fecha: " + dt.Rows[k][3].ToString() + " || ";
                    aux += "Hora: " + dt.Rows[k][4].ToString();
                    listaActividades.Add(aux);
                }
            }
            catch
            {
                System.Console.WriteLine("Error en BD");
            }
            return listaActividades;
        }

        public static void insert_actividad(ActividadLog actlog)
        {
            SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO Actividad_Log ";
            cmd.CommandText += "(usu_id,actlog_descripcion,actlog_fecha,actlog_hora) ";
            cmd.CommandText += "VALUES (@usu,@descripcion,@fecha,@hora)";
            cmd.Parameters.AddWithValue("@usu", actlog.Usu_Id);
            cmd.Parameters.AddWithValue("@hora", TimeSpan.Parse(actlog.ActLog_hora));
            cmd.Parameters.AddWithValue("@fecha", DateTime.Parse(actlog.ActLog_fecha).Date);
            cmd.Parameters.AddWithValue("@descripcion", actlog.ActLog_Descripcion);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static void clear_historial()
        {
            SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "TRUNCATE TABLE Actividad_Log";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }
    }
}
