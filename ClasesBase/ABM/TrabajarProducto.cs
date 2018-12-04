using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace ClasesBase.ABM
{
    public class TrabajarProducto : ObservableCollection<Producto>
    {
        public ObservableCollection<Producto> traerProductos()
        {
            ObservableCollection<Producto> prod = new ObservableCollection<Producto>();
            try
            {
                SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
                SqlDataAdapter sda = new SqlDataAdapter("Select * from Productos", cn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Producto aux;

                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    aux = new Producto();
                    aux.Prod_Cod = dt.Rows[k][0].ToString();
                    aux.Prod_Descripcion = dt.Rows[k][1].ToString();
                    aux.Prod_Categoria = dt.Rows[k][2].ToString();
                    aux.Prod_Precio = dt.Rows[k][3].ToString();
                    aux.Prod_Image = dt.Rows[k][4].ToString();
                    prod.Add(aux);
                }
            }
            catch {
                System.Console.WriteLine("Error en BD");
            }
            return prod;
        }
        public static string descProd(string cod)
        {
            SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlDataAdapter sda = new SqlDataAdapter("Select prod_descrip from Productos WHERE prod_cod=" + cod, cn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt.Rows[0][0].ToString();
        }

        public static Producto traerProducto(string cod)
        {

            SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "SELECT ";
                    cmd.CommandText += "prod_cod as 'Codigo', ";
                    cmd.CommandText += "prod_descrip as 'Descripcion', ";
                    cmd.CommandText += "prod_categ as 'Categoria', ";
                    cmd.CommandText += "prod_precio as 'Precio', ";
                    cmd.CommandText += "prod_image as 'Imagen' ";
                    cmd.CommandText += "FROM Productos WHERE prod_cod = @cod";

                    cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@cod", cod);

            Producto pro = new Producto();

            cnn.Open();

            try
            {
                SqlDataReader unReader = cmd.ExecuteReader();
                if (unReader.Read())
                {
                    pro.Prod_Cod = unReader["Codigo"].ToString();
                    pro.Prod_Categoria = unReader["Categoria"].ToString();
                    pro.Prod_Descripcion = unReader["Descripcion"].ToString();
                    pro.Prod_Precio = unReader["Precio"].ToString();
                    pro.Prod_Image = unReader["Imagen"].ToString();
                }
            }
            catch 
            {
                pro.Prod_Cod = "";
                pro.Prod_Descripcion = "";
                pro.Prod_Precio = "";
                pro.Prod_Categoria = "";
            }

            cnn.Close();
            return pro;
        }
        
        public static void insert_Producto(Producto prod)
        {

            SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True");

            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insertarProducto";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("prod_cod", prod.Prod_Cod);
            cmd.Parameters.AddWithValue("prod_descrip", prod.Prod_Descripcion);
            cmd.Parameters.AddWithValue("prod_categ", prod.Prod_Categoria);
            cmd.Parameters.AddWithValue("prod_precio", Decimal.Parse(prod.Prod_Precio));
            cmd.Parameters.AddWithValue("prod_image", prod.Prod_Image);
            cmd.ExecuteNonQuery();
            cn.Close();
        }
       
        public static void delete_Producto(string cod)
        {

            SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "eliminarProducto";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("prod_cod", cod);
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        public static void modificar_Producto(Producto prod)
        {
            SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "modificarProducto";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("prod_cod", prod.Prod_Cod);
            cmd.Parameters.AddWithValue("prod_descrip", prod.Prod_Descripcion);
            cmd.Parameters.AddWithValue("prod_categ", prod.Prod_Categoria);
            cmd.Parameters.AddWithValue("prod_precio", Decimal.Parse(prod.Prod_Precio));
            cmd.Parameters.AddWithValue("prod_image", prod.Prod_Image);
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public static Producto getProducto_fromDB(string cod)
        {
            SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlCommand cmd = new SqlCommand();
            cnn.Open();
            cmd.CommandText = "SELECT * FROM Productos WHERE prod_cod = @cod";
            cmd.Parameters.AddWithValue("@cod", cod);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cnn.Close();

            Producto prod = new Producto();

            prod.Prod_Cod =dt.Rows[0][0].ToString();
            prod.Prod_Descripcion = dt.Rows[0][1].ToString();
            prod.Prod_Categoria = dt.Rows[0][2].ToString();
            prod.Prod_Precio = dt.Rows[0][3].ToString();
            prod.Prod_Image = dt.Rows[0][4].ToString();
            return prod;
        }
        
        public static DataTable consul_Producto()
        {

            SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlDataAdapter sda0 = new SqlDataAdapter("SELECT * From Productos", cn);
            DataTable dt0 = new DataTable();
            sda0.Fill(dt0);
            string primer = dt0.Rows[0][0].ToString();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Productos where prod_cod like'" + primer + "'", cn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt; 
        }

        public static int getLastProducto_Id()
        {
            SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Productos ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            cnn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow dr = dt.Rows[dt.Rows.Count - 1];
            return Int32.Parse(dr[0].ToString());
        }
    }
}
