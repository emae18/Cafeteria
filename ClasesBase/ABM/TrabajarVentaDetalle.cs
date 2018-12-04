using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace ClasesBase.ABM
{
    public class TrabajarVentaDetalle
    {
        public static void insert_ventaDetalle(VentaDetalle item)
        {
            SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "INSERT INTO VentaDetalle ";
            cmd.CommandText += "(vnt_nro_factura, prod_cod, vd_cantidad , vd_precio ,vd_importe) ";
            cmd.CommandText += "VALUES (@nrofa, @prodcod, @cant, @pre, @imp)";
            cmd.Parameters.AddWithValue("@nrofa", item.Vd_NroFactura);
            cmd.Parameters.AddWithValue("@prodcod", item.Vd_CodProducto);
            cmd.Parameters.AddWithValue("@cant", item.Vd_Cantidad);
            cmd.Parameters.AddWithValue("@pre", item.Vd_Precio);
            cmd.Parameters.AddWithValue("@imp", item.Vd_Importe);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }
        public static decimal impTotal(int nro_factura)
        {
            decimal aux = 0;
            SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlDataAdapter sda = new SqlDataAdapter("SELECT vd_importe FROM VentaDetalle WHERE vnt_nro_factura = " + nro_factura, cnn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                aux = aux + decimal.Parse(dt.Rows[i][0].ToString());
            }

            return aux;

        }
        public static void update_ventaDetalle(VentaDetalle item)
        {
            SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "UPDATE VentaDetalle SET vd_cantidad=@cant WHERE prod_cod=@codProd";
            cmd.Parameters.AddWithValue("@cant", item.Vd_Cantidad);
            cmd.Parameters.AddWithValue("@codProd", item.Vd_CodProducto);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }
        public static void delete_ventaDetalle(VentaDetalle item)
        {
            SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlCommand cmd = new SqlCommand();
            //TRUNCATE TABLE nombre_tabla;
            cmd.CommandText = "DELETE FROM VentaDetalle WHERE vd_cantidad=@cant WHERE prod_cod=@codProd";
            cmd.Parameters.AddWithValue("@cant", item.Vd_Cantidad);
            cmd.Parameters.AddWithValue("@codProd", item.Vd_CodProducto);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }
    }
}
