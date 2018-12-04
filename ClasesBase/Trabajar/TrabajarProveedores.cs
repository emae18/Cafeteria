using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


namespace ClasesBase.ABM
{
    public class TrabajarProveedores
    {
        public DataTable traerProveedor()
        {
            //SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\Users\\Alon Nami\\Documents\\eev18-lpoo2grupo1-67d9944e713d\\ClasesBase\\muebleria.mdf';Integrated Security=True;Connect Timeout=30;User Instance=True");
            /*SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Proveedores";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;*/
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * From Proveedores", cnn);

            DataTable dt = new DataTable();

            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            sda.Fill(dt);

            return dt;


        }
    }
}
