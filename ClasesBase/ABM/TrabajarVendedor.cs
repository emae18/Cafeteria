using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
namespace ClasesBase.ABM
{
    public class TrabajarVendedor: ObservableCollection<Vendedor>
    {
        public static void insert_Vendedor(Vendedor ven)
        {
            SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insertarVendedor";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("vend_legajo", ven.Vend_Legajo);
            cmd.Parameters.AddWithValue("vend_nom", ven.Vend_Nombre);
            cmd.Parameters.AddWithValue("vend_ape", ven.Vend_Apellido);
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public static ObservableCollection<Vendedor> consul_Vendedor()
        {
            ObservableCollection<Vendedor> vendedores = new ObservableCollection<Vendedor>();
            try
            {
                SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Vendedores", cn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Vendedor aux;
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    aux = new Vendedor();
                    aux.Vend_Legajo = dt.Rows[k][0].ToString();
                    aux.Vend_Nombre = dt.Rows[k][1].ToString();
                    aux.Vend_Apellido = dt.Rows[k][2].ToString();                    
                    vendedores.Add(aux);
                }
            }
            catch {
                System.Console.WriteLine("Error en BD: consul_vendedor");
            }
            return vendedores;
        }
        public static DataTable consul_Vendedor2()
        {
            SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True");    
            SqlDataAdapter sda = new SqlDataAdapter("Select * from Vendedores", cn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                

               
            return dt;
        }
        public static void delete_Vendedor(string leg)
        {
            SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "eliminarVendedor";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("leg", leg);
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        public static void modificar_Vendedor(Vendedor ven)
        {
            SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "modificarVendedor";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("leg", ven.Vend_Legajo);
            cmd.Parameters.AddWithValue("nom", ven.Vend_Nombre);
            cmd.Parameters.AddWithValue("ape", ven.Vend_Apellido);
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public static Vendedor getVendedor_fromDB(string leg)
        {
            SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlCommand cmd = new SqlCommand();
            cnn.Open();
            cmd.CommandText = "SELECT * FROM Vendedores WHERE vend_legajo = @leg";
            cmd.Parameters.AddWithValue("@leg", leg);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cnn.Close();

            Vendedor vend = new Vendedor();

            vend.Vend_Legajo = dt.Rows[0][0].ToString();
            vend.Vend_Nombre = dt.Rows[0][1].ToString();
            vend.Vend_Apellido = dt.Rows[0][2].ToString();

            return vend;
        }
        public static bool verificar_cod(string legajo)
        {
            SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Vendedores", cn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                if (dt.Rows[k][0].ToString() == legajo)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
