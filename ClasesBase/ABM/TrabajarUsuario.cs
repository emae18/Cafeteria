using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace ClasesBase.ABM
{
    public class TrabajarUsuario
    {
        public static DataTable traerUsuarios()
        {
            SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT ";
            cmd.CommandText += "usu_nom as 'Nombres', ";
            cmd.CommandText += "usu_ape as 'Apellido', ";
            cmd.CommandText += "usu_username 'Nombre de Usuario', ";
            cmd.CommandText += "usu_contraseña as 'Contraseña', ";
            cmd.CommandText += "usu_rol as 'Rol' ";
            cmd.CommandText += "FROM Usuarios";

            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            cnn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cnn.Close();
            return dt;
        }

        public static DataTable buscar_usuario(string username)
        {
            SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT ";
            cmd.CommandText += "usu_id as 'ID', ";
            cmd.CommandText += "usu_nom as 'Nombres', ";
            cmd.CommandText += "usu_ape as 'Apellido', ";
            cmd.CommandText += "usu_username 'Nombre de Usuario', ";
            cmd.CommandText += "usu_contraseña as 'Contraseña', ";
            cmd.CommandText += "usu_rol as 'Rol', ";
            cmd.CommandText += "usu_image as 'Imagen' ";
            cmd.CommandText += "FROM Usuarios WHERE usu_username = @nom";

            cmd.Parameters.AddWithValue("@nom", username);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            cnn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cnn.Close();

            return dt;
        }

        public static void cosultar_cant_admins()
        { }

        public static void insert_usuario()
        {}

        public static void delete_usuario()
        {}

        public static void update_usuario(Usuario usuario)
        {
            SqlConnection cnn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\USERS\\ALON MATZU\\DOWNLOADS\\CAFE\\CLASESBASE\\MUEBLERIA.MDF';Integrated Security=True;Connect Timeout=30;User Instance=True"); 
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE Usuarios ";
            cmd.CommandText += "SET usu_nom=@nom,usu_ape=@ape,usu_username=@user,usu_contraseña=@pass ";
            cmd.CommandText += "WHERE usu_id=@id";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            cmd.Parameters.AddWithValue("@id", usuario.Usu_Id);
            cmd.Parameters.AddWithValue("@nom", usuario.Usu_Nombre);
            cmd.Parameters.AddWithValue("@ape", usuario.Usu_Apellido);
            cmd.Parameters.AddWithValue("@user", usuario.Usu_NombreUsuario);
            cmd.Parameters.AddWithValue("@pass", usuario.Usu_Contraseña);
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

    }
}
