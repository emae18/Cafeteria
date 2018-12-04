using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ClasesBase.ABM
{
    public class TrabajarProducto
    {
        public static void insert_Producto(string cod, string descripcion, string categoria, string color, string precio)
        {

            SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\Users\\Alon Nami\\Documents\\Visual Studio 2010\\Projects\\lpoo2grupo1\\ClasesBase\\muebleria.mdf';Integrated Security=True;Connect Timeout=30;User Instance=True");
            //SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\Users\\salmazan\\Desktop\\tp3Actualizado\\lpoo2grupo1\\ClasesBase\\muebleria.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");

            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insertarProducto";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("prov_cod", cod);
            cmd.Parameters.AddWithValue("prod_descrip", descripcion);
            cmd.Parameters.AddWithValue("prod_categ", categoria);
            cmd.Parameters.AddWithValue("prod_color", color);
            cmd.Parameters.AddWithValue("prod_precio",precio);
            cmd.ExecuteNonQuery();
            cn.Close();
        }
       
        public static void delete_Producto(int cod)
        {

            SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\Users\\Alon Nami\\Documents\\Visual Studio 2010\\Projects\\lpoo2grupo1\\ClasesBase\\muebleria.mdf';Integrated Security=True;Connect Timeout=30;User Instance=True");
            //SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\Users\\salmazan\\Desktop\\tp3Actualizado\\lpoo2grupo1\\ClasesBase\\muebleria.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "eliminarProducto";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("prov_cod", cod);
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        public static void modificar_Producto(string cod, string descripcion, string categoria, string color, string precio)
        {

            SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\Users\\Alon Nami\\Documents\\Visual Studio 2010\\Projects\\lpoo2grupo1\\ClasesBase\\muebleria.mdf';Integrated Security=True;Connect Timeout=30;User Instance=True");
            //SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\Users\\salmazan\\Desktop\\tp3Actualizado\\lpoo2grupo1\\ClasesBase\\muebleria.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "modificarProducto";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("prov_cod", cod);
            cmd.Parameters.AddWithValue("prod_descrip", descripcion);
            cmd.Parameters.AddWithValue("prod_categ", categoria);
            cmd.Parameters.AddWithValue("prod_color", color);
            cmd.Parameters.AddWithValue("prod_precio", precio);
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public static DataTable consul_Producto()
        {

            SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\Users\\Alon Nami\\Documents\\Visual Studio 2010\\Projects\\lpoo2grupo1\\ClasesBase\\muebleria.mdf';Integrated Security=True;Connect Timeout=30;User Instance=True");
            //SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\Users\\salmazan\\Desktop\\tp3Actualizado\\lpoo2grupo1\\ClasesBase\\muebleria.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
            SqlDataAdapter sda0 = new SqlDataAdapter("SELECT * From Productos", cn);
            DataTable dt0 = new DataTable();
            sda0.Fill(dt0);
            string primer = dt0.Rows[0][0].ToString();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Productos where prov_cod like'" + primer + "'", cn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }
    }
}
