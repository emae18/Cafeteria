using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using ClasesBase;
namespace ClasesBase.ABM
{
    public class TrabajarEstilo
    {
        public static ObservableCollection<Estilo> traerEstilos()
        {
            ObservableCollection<Estilo> estilos = new ObservableCollection<Estilo>();
            try
            {
                SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Estilos";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cnn;
                cnn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cnn.Close();
                Estilo aux;
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    aux = new Estilo();
                    aux.Est_Id = Int32.Parse(dt.Rows[k][0].ToString());
                    aux.Est_Nombre = dt.Rows[k][1].ToString();
                    aux.Est_Ubicacion = dt.Rows[k][2].ToString();
                    aux.Est_Estado = dt.Rows[k][3].ToString();
                    estilos.Add(aux);
                }
                return estilos;
            }
            catch 
            {
                return estilos;
            }            
        }

        public static void update_estilo(int id)
        {
            SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE Estilos ";
            cmd.CommandText += "SET style_estado=@est" ;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@est", "false");
            cmd.Connection = cnn;
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
            cmd.CommandText = "UPDATE Estilos ";
            cmd.CommandText += "SET style_estado='true' ";
            cmd.CommandText += "WHERE style_id=@id";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Connection = cnn;
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }
    }
}
