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
    public class TrabajarVenta: ObservableCollection<Venta>
    {
        public ObservableCollection<Venta> traerVentas()
        {
            ObservableCollection<Venta> ventas = new ObservableCollection<Venta>();
            try
            {
                SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Ventas", cn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Venta aux;

                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    aux = new Venta();
                    aux.Venta_NroFactura = Int32.Parse(dt.Rows[k][0].ToString());
                 // aux.Venta_Cliente = TrabajarCliente.getCliente_fromDB((String)dt.Rows[k][1]);
                    aux.Venta_Vendedor = TrabajarVendedor.getVendedor_fromDB((String)dt.Rows[k][2]);
                    aux.Venta_FechaFactura = (DateTime)dt.Rows[k][3];
                    aux.Venta_ImporteTotal = (Decimal)dt.Rows[k][4];
                    ventas.Add(aux);
                }
            }
            catch
            {
                System.Console.WriteLine("Error en BD: Traer ventas");
            }
            return ventas;
        }

        public static void insert_venta(Venta venta)
        {
            SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "INSERT INTO Ventas ";
            cmd.CommandText += "(vnt_nro_factura, cli_dni, vend_legajo, vnt_fec_factura, vnt_imp_total) ";
            cmd.CommandText += "VALUES (@nro, @dni, @vend, @fac, @imp)";

            cmd.Parameters.AddWithValue("@dni", 000);
            cmd.Parameters.AddWithValue("@vend", venta.Venta_Vendedor.Vend_Legajo);
            cmd.Parameters.AddWithValue("@nro", venta.Venta_NroFactura);
            cmd.Parameters.AddWithValue("@fac", venta.Venta_FechaFactura);
            cmd.Parameters.AddWithValue("@imp", venta.Venta_ImporteTotal);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close(); 
        }
        
        public ObservableCollection<PartialClass> traerVentaFinal(string nroFact)
        {
            ObservableCollection<PartialClass> ventas = new ObservableCollection<PartialClass>();
            try
            {
                SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
                cn.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM vFinal WHERE vnt_nro_factura = " + nroFact, cn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                cn.Close();
                PartialClass aux;
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    aux = new PartialClass();
                    aux.DescProd = dt.Rows[k][0].ToString();
                    aux.ColorProd = dt.Rows[k][1].ToString();
                    aux.PrecProd = decimal.Parse(dt.Rows[k][2].ToString());
                    aux.CantProd = Int32.Parse(dt.Rows[k][3].ToString());
                    aux.PrecFinalVent = decimal.Parse(dt.Rows[k][4].ToString());
                    ventas.Add(aux);
                }
            }
            catch
            {
                System.Console.WriteLine("Error en BD: Venta --> VentaDetalle");
            }
            return ventas;
        }

        public static int getLastVenta_Id()
        {
            SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Ventas ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            cnn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow dr = dt.Rows[dt.Rows.Count - 1];
            return Int32.Parse(dr[0].ToString());
         
        }

        /*public static bool verificar_cod(int cod)
        {
            SqlConnection cn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("Select * from Ventas", cn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                if (Int32.Parse(dt.Rows[k][0].ToString()) == cod)
                {
                    return true;
                }
            }
            return false;
        }*/
        public static double ganancia(DateTime d)
        {
            double total = 0;
            try
            {

                SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True");
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Ventas WHERE vnt_fec_factura >= @day AND vnt_fec_factura <= @day2";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@day", d.Date);
                cmd.Parameters.AddWithValue("@day2", d.Date.AddHours(24));
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                DateTime d2 = d.Date.AddHours(24);
                for (int i = 0; i <= dt.Rows.Count; i++)
                {
                    total = total + double.Parse(dt.Rows[i][4].ToString());
                }
                return total;
            }
            catch {
                return total;
            }

            

        }
        public static double gananciaTotal()
        {
        
            double total = 0;
            try
            {

                SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True");
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Ventas";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i <= dt.Rows.Count; i++)
                {
                    total = total + double.Parse(dt.Rows[i][4].ToString());
                }
                return total;
            }
            catch
            {
                return total;
            }

        }
        public static double gananciaFecha(DateTime d1, DateTime d2)
        {
            double total = 0;
            try
            {

                SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True");
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Ventas WHERE vnt_fec_factura >= @day AND vnt_fec_factura <= @day2";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@day", d1.Date);
                cmd.Parameters.AddWithValue("@day2", d2.Date.AddHours(24));
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i <= dt.Rows.Count; i++)
                {
                    total = total + double.Parse(dt.Rows[i][4].ToString());
                }
                return total;
            }
            catch
            {
                return total;
            }   
        }
        
        
    }
}
