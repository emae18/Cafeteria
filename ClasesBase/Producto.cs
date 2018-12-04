using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClasesBase
{
    public class Producto: IDataErrorInfo, INotifyPropertyChanged
    {
        private string prod_Cod;

        public string Prod_Cod
        {
            get { return prod_Cod; }
            set { prod_Cod = value;
                Notificador("Prod_Cod"); }
        }
        private string prod_Categoria;

        public string Prod_Categoria
        {
            get { return prod_Categoria; }
            set { prod_Categoria = value; Notificador("Prod_Categoria"); }
        }
        
        private string prod_Descripcion;

        public string Prod_Descripcion
        {
            get { return prod_Descripcion; }
            set { prod_Descripcion = value; Notificador("Prod_Descripcion"); }
        }
        private string prod_Precio;

        public string Prod_Precio
        {
            get { return prod_Precio; }
            set
            {
                prod_Precio = String.Format("{0:0.00}", Decimal.Parse(value));
                Notificador("Prod_Precio"); }
        }
        public Producto(string cod,string categoria, string descripcion, string precio)
        {
            this.prod_Categoria = categoria;
            this.prod_Cod = cod;
            this.prod_Descripcion = descripcion;
            this.prod_Precio = precio;
        }
        public Producto()
        {
        }
        
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get 
            {
                string result = null;

                if (columnName == "Prod_Categoria")
                { 
                    if(String.IsNullOrEmpty(Prod_Categoria))
                    {
                        result = "Campo obligatorio";
                    } 
                }else if(columnName == "Prod_Descripcion")
                {
                    if(String.IsNullOrEmpty(prod_Descripcion))
                    {
                        result = "Campo obligatorio";
                    }
                }
                else if (columnName == "Prod_Precio")
                {
                   // double Num;
                    /*
                    bool isDouble = double.TryParse(Prod_Precio, out Num);
                    if (String.IsNullOrEmpty(Prod_Precio))
                    {
                        result = "Campo obligatorio";
                    }
                    else if (!isDouble)
                    {
                        result = "Solo valores númericos";
                    } else if (Double.Parse(Prod_Precio) <= 0)
                    {
                        result = "Debe ser mayor a cero";
                    }
                    */
                }
                return result;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Notificador(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        private string prod_Image;

        public string Prod_Image
        {
            get { return prod_Image; }
            set { prod_Image = value; Notificador("Prod_Image"); }
        }

        public override string ToString()
        {
            return "[COD]: " + Prod_Cod + " [Descripcion]: " + Prod_Descripcion + " [Categoria]: " + Prod_Categoria + " [Precio]: " + Prod_Precio + " [Imagen Path]: " + Prod_Image;
        }
    }
}
