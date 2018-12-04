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
    public class TrabajarGastos
    {
         public static DataTable traerGastos()
        {
            DataTable gastos = new DataTable();
            try
            {
                SqlConnection cn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Gastos", cn);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    gastos.Rows[k][0] = dt.Rows[k][0].ToString();
                    gastos.Rows[k][1] = (double)dt.Rows[k][1];
                }
            }
            catch
            {
                System.Console.WriteLine("Error en BD: Traer ventas");
            }
            return gastos;
        }

         public static void insertarGasto(Gasto gasto)
         {
             SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True");
             SqlCommand cmd = new SqlCommand();

             cmd.CommandText = "INSERT INTO Gastos ";
             cmd.CommandText += "(gs_Descripcion, gs_Precio) ";
             cmd.CommandText += "VALUES (@des, @pre)";

             cmd.Parameters.AddWithValue("@des", gasto.Gasto_Descripcion);
             cmd.Parameters.AddWithValue("@pre", gasto.Gasto_Precio);
             cmd.CommandType = CommandType.Text;
             cmd.Connection = cnn;

             cnn.Open();
             cmd.ExecuteNonQuery();
             cnn.Close(); 
         }
    }
}
