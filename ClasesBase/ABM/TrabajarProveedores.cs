using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;


namespace ClasesBase.ABM
{
    public class TrabajarProveedores
    {
        public static ObservableCollection<Proveedor> traerProveedores()
        {
            ObservableCollection<Proveedor> prov = new ObservableCollection<Proveedor>();
            try
            {
                SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
                SqlDataAdapter sda = new SqlDataAdapter("Select * from Proveedores", cn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Proveedor aux;

                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    aux = new Proveedor();
                    aux.Prov_Cuit = dt.Rows[k][0].ToString();
                    aux.Prov_Domicilio = dt.Rows[k][1].ToString();
                    aux.Prov_RazonSocial = dt.Rows[k][2].ToString();
                    aux.Prov_Telefono = dt.Rows[k][3].ToString();

                    prov.Add(aux);
                }
            }
            catch {
                System.Console.WriteLine("Error en BD");
            }
            return prov;
        }

        public static void insert_proveedor(Proveedor prov)
        {
            SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "INSERT INTO Proveedores ";
            cmd.CommandText += "(prov_id, prov_domicilio, prov_razonSocial, prov_telefono) ";
            cmd.CommandText += "VALUES (@id, @dom, @raz, @tel)";

            cmd.Parameters.AddWithValue("@id", prov.Prov_Cuit);
            cmd.Parameters.AddWithValue("@dom", prov.Prov_Domicilio);
            cmd.Parameters.AddWithValue("@raz", prov.Prov_RazonSocial);
            cmd.Parameters.AddWithValue("@tel", prov.Prov_Telefono);

            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static void update_proveedor(Proveedor prov)
        {
            SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "UPDATE Proveedores ";
            cmd.CommandText += "SET prov_id = @id, prov_domicilio = @dom, prov_razonSocial = @raz, prov_telefono = @tel ";
            cmd.CommandText += "WHERE prov_id = @id";

            cmd.Parameters.AddWithValue("@id", prov.Prov_Cuit);
            cmd.Parameters.AddWithValue("@dom", prov.Prov_Domicilio);
            cmd.Parameters.AddWithValue("@raz", prov.Prov_RazonSocial);
            cmd.Parameters.AddWithValue("@tel", prov.Prov_Telefono);
            cmd.Connection = cnn;
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static void delete_proveedor(string id)
        {
            SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            cnn.Open();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "DELETE FROM Proveedores ";
            cmd.CommandText += "WHERE prov_id = @id";

            cmd.Parameters.AddWithValue("@id", id);

            cmd.Connection = cnn;
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public DataTable traerProveedor()
        {
            //SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\Users\\Alon Nami\\Documents\\eev18-lpoo2grupo1-67d9944e713d\\ClasesBase\\muebleria.mdf';Integrated Security=True;Connect Timeout=30;User Instance=True");
            /*SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Proveedores";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;*/
            SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * From Proveedores", cnn);

            DataTable dt = new DataTable();

            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            sda.Fill(dt);

            return dt;
        }

        public static bool verificar_cod(string cuit)
        {

            SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Proveedores", cn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                if (dt.Rows[k][0].ToString() == cuit)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
