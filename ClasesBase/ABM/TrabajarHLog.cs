using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ClasesBase.ABM
{
    public class TrabajarHLog
    {
        public static DataTable traerHistorial()
        {
            try
            {
                SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Historial_Usuarios_Login_View";
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

        public static List<string> traerListadoHistorial()
        {
            List<string> listaHLog = new List<string>();
            try
            {
                SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
                SqlDataAdapter sda = new SqlDataAdapter("Select * from Historial_Usuarios_Login_View", cn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                string aux;

                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    aux = "ID: " + dt.Rows[k][0].ToString() + " || ";
                    aux += "Usuario: " + dt.Rows[k][1].ToString() + " || ";
                    aux += "Hora: " + dt.Rows[k][2].ToString() + " || ";
                    aux += "Fecha: " + DateTime.Parse(dt.Rows[k][3].ToString()).ToShortDateString() + " || ";
                    aux += "Duracion: " + dt.Rows[k][4].ToString() + "\n";
                    listaHLog.Add(aux);
                }
            }
            catch
            {
                System.Console.WriteLine("Error en BD");
            }
            return listaHLog;
        }

        public static void insert_hlog(HistorialLogin hlog)
        {
            SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO Historial_Login ";
            cmd.CommandText += "(usu_id,hlog_hora,hlog_fecha,hlog_duracion) ";
            cmd.CommandText += "VALUES (@usu,@hora,@fecha,@duracion)";
            cmd.Parameters.AddWithValue("@usu", hlog.Usu_Id);
            cmd.Parameters.AddWithValue("@hora", TimeSpan.Parse(hlog.Hlog_hora));
            cmd.Parameters.AddWithValue("@fecha", DateTime.Parse(hlog.Hlog_fecha).Date);
            cmd.Parameters.AddWithValue("@duracion", hlog.Hlog_duracion);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static int getLastHistorial_Id()
        {
            SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Historial_Login ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            cnn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow dr = dt.Rows[dt.Rows.Count - 1];
            return (int)dr[0];
        }

        public static void update_historial(HistorialLogin hlog, int cod)
        {
            SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE Historial_Login ";
            cmd.CommandText += "SET usu_id=@usu,hlog_hora=@hora,hlog_fecha=@fecha,hlog_duracion=@duracion";
            cmd.CommandText += " WHERE hlog_id=@id";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@usu", hlog.Usu_Id);
            cmd.Parameters.AddWithValue("@hora", hlog.Hlog_hora);
            cmd.Parameters.AddWithValue("@fecha", hlog.Hlog_fecha);
            cmd.Parameters.AddWithValue("@duracion", hlog.Hlog_duracion);
            cmd.Parameters.AddWithValue("@id", cod);
            cmd.Connection = cnn;
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static void clear_historial()
        {
            SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "DELETE FROM Historial_Login WHERE hlog_id <> @id";
            cmd.Parameters.AddWithValue("@id", getLastHistorial_Id());
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }
    }
}
