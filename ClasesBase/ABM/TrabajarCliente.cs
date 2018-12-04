using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace ClasesBase.ABM
{
    public class TrabajarCliente: ObservableCollection<Cliente>
    {
        public static ObservableCollection<Cliente> traerCliente()
        {
            ObservableCollection<Cliente> cli = new ObservableCollection<Cliente>();
            try {
                SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
                SqlDataAdapter sda = new SqlDataAdapter("Select * from Clientes", cn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Cliente aux;

                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    aux = new Cliente();
                    aux.Cli_Dni = dt.Rows[k][0].ToString();
                    aux.Cli_Nombre = dt.Rows[k][1].ToString();
                    aux.Cli_Apellido = dt.Rows[k][2].ToString();
                    aux.Cli_Direccion = dt.Rows[k][3].ToString();
                    
                    cli.Add(aux);
                }
            }
            catch {
                System.Console.WriteLine("Error en BD");
            }        
            return cli;
        }

        public static void insert_cliente(Cliente cli)
        {

            SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "INSERT INTO Clientes ";
            cmd.CommandText += "(cli_dni, cli_nom, cli_ape, cli_dir) ";
            cmd.CommandText += "VALUES (@dni, @nom, @ape, @dir)";

            cmd.Parameters.AddWithValue("@dni", cli.Cli_Dni);
            cmd.Parameters.AddWithValue("@nom", cli.Cli_Nombre);
            cmd.Parameters.AddWithValue("@ape", cli.Cli_Apellido);
            cmd.Parameters.AddWithValue("@dir", cli.Cli_Direccion);

            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();            
       }

        public static void update_cliente(Cliente cli)
        {
            SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "UPDATE Clientes ";
            cmd.CommandText += "SET cli_dni = @dni, cli_nom = @nom, cli_ape = @ape, cli_dir = @dir ";
            cmd.CommandText += "WHERE cli_dni = @dni";

            cmd.Parameters.AddWithValue("@dni", cli.Cli_Dni);
            cmd.Parameters.AddWithValue("@nom", cli.Cli_Nombre);
            cmd.Parameters.AddWithValue("@ape", cli.Cli_Apellido);
            cmd.Parameters.AddWithValue("@dir", cli.Cli_Direccion);
            cmd.Connection = cnn;
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close(); 
        }

        public static void delete_cliente(string dni)
        {
            SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            cnn.Open();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "DELETE FROM Clientes ";
            cmd.CommandText += "WHERE cli_dni = @dni";

            cmd.Parameters.AddWithValue("@dni", dni);

            cmd.Connection = cnn;
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static Cliente getCliente_fromDB(string dni)
        {
            SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlCommand cmd = new SqlCommand();
            cnn.Open();
            cmd.CommandText = "SELECT * FROM Clientes WHERE cli_dni = @dni";
            cmd.Parameters.AddWithValue("@dni", dni);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cnn.Close();

            Cliente cli = new Cliente();

            cli.Cli_Dni = dt.Rows[0][0].ToString();
            cli.Cli_Nombre = dt.Rows[0][1].ToString();
            cli.Cli_Apellido = dt.Rows[0][2].ToString();
            cli.Cli_Direccion = dt.Rows[0][3].ToString();

            return cli;
        }
        
        public static bool verificar_cod(string dni)
        {
            SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Clientes", cn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                if (dt.Rows[k][0].ToString() == dni)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
