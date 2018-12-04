using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class Usuario
    {
        public Usuario() { }

        public Usuario(int id, string nombreUsuario, string contraseña, string nombre, string apellido, string rol, byte[] image)
        {
            this.usu_Id = id;
            this.usu_NombreUsuario = nombreUsuario;
            this.usu_Contraseña = contraseña;
            this.usu_Rol = rol;
            this.usu_Nombre = nombre;
            this.usu_Apellido = apellido;
            this.usu_Image = image;
        }

        private string usu_NombreUsuario;

        public string Usu_NombreUsuario
        {
            get { return usu_NombreUsuario; }
            set { usu_NombreUsuario = value; }
        }
        private string usu_Contraseña;

        public string Usu_Contraseña
        {
            get { return usu_Contraseña; }
            set { usu_Contraseña = value; }
        }
        private string usu_Rol;

        public string Usu_Rol
        {
            get { return usu_Rol; }
            set { usu_Rol = value; }
        }

        private string usu_Nombre;

        public string Usu_Nombre
        {
            get { return usu_Nombre; }
            set { usu_Nombre = value; }
        }
        private string usu_Apellido;

        public string Usu_Apellido
        {
            get { return usu_Apellido; }
            set { usu_Apellido = value; }
        }

        private byte[] usu_Image;

        public byte[] Usu_Image
        {
            get { return usu_Image; }
            set { usu_Image = value; }
        }

        private int usu_Id;

        public int Usu_Id
        {
            get { return usu_Id; }
            set { usu_Id = value; }
        }
    }
}
