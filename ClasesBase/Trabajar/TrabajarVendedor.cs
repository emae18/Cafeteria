using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ClasesBase.ABM
{
    public class TrabajarVendedor
    {
        public static void insert_Vendedor(string leg, string nom,string ap )
        {
            SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\Users\\Alon Nami\\Documents\\Visual Studio 2010\\Projects\\lpoo2grupo1\\ClasesBase\\muebleria.mdf';Integrated Security=True;Connect Timeout=30;User Instance=True");
            //SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\Users\\salmazan\\Desktop\\tp3Actualizado\\lpoo2grupo1\\ClasesBase\\muebleria.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insertarVendedor";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("vend_legajo", leg);
            cmd.Parameters.AddWithValue("vend_nom", nom);
            cmd.Parameters.AddWithValue("vend_ape", ap);
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        public static DataTable consul_Vendedor()
        {
            //SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\Users\\Alon Nami\\Documents\\Visual Studio 2010\\Projects\\lpoo2grupo1\\ClasesBase\\muebleria.mdf';Integrated Security=True;Connect Timeout=30;User Instance=True");
            SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\Users\\salmazan\\Desktop\\tp3Actualizado\\lpoo2grupo1\\ClasesBase\\muebleria.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
            SqlDataAdapter sda0 = new SqlDataAdapter("SELECT * From Vendedores", cn);
            DataTable dt0 = new DataTable();
            sda0.Fill(dt0);
            string primer = dt0.Rows[0][0].ToString();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Vendedores where vend_legajo like'"+primer+"'", cn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }
        public static void delete_Vendedor(string leg)
        {
            SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\Users\\Alon Nami\\Documents\\Visual Studio 2010\\Projects\\lpoo2grupo1\\ClasesBase\\muebleria.mdf';Integrated Security=True;Connect Timeout=30;User Instance=True");
            //SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\Users\\salmazan\\Desktop\\tp3Actualizado\\lpoo2grupo1\\ClasesBase\\muebleria.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "eliminarVendedor";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("leg", leg);
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        public static void modificar_Vendedor(string leg,string nom, string ape)
        {
            SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\Users\\Alon Nami\\Documents\\Visual Studio 2010\\Projects\\lpoo2grupo1\\ClasesBase\\muebleria.mdf';Integrated Security=True;Connect Timeout=30;User Instance=True");
            //SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\Users\\salmazan\\Desktop\\tp3Actualizado\\lpoo2grupo1\\ClasesBase\\muebleria.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "modificarVendedor";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("leg", leg);
            cmd.Parameters.AddWithValue("nom", nom);
            cmd.Parameters.AddWithValue("ape", ape);
            cmd.ExecuteNonQuery();
            cn.Close();
        }
    }
}
