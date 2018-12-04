using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Collections.ObjectModel;
using ClasesBase;
namespace Vistas
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static ObservableCollection<Tema> listaEstilos;
        public static ObservableCollection<Tema> ListaEstilos
        {
            get { return App.listaEstilos; }
            set { App.listaEstilos = value; }
        }
        //Lista de skins
        public enum skins { Blue, Black }
        private static skins skinSeleccionado;
        public static skins SkinSeleccionado { get { return skinSeleccionado; } set { skinSeleccionado = value; } }

        public App()
        {
            ListaEstilos = new ObservableCollection<Tema>();
            cargar_estilos();
            SkinSeleccionado = skins.Blue;
            switch (getCurrentStyle())
            {
                case 0:
                    SkinSeleccionado = skins.Blue;
                    break;
                case 1:
                    SkinSeleccionado = skins.Black;
                    break;
            }
        }

        public static void cargar_estilos()
        {
            ListaEstilos.Clear();
            foreach (Estilo i in ClasesBase.ABM.TrabajarEstilo.traerEstilos())
            {
                ResourceDictionary rd = new ResourceDictionary();
                rd.Source = new Uri(i.Est_Ubicacion, UriKind.Relative);
                Tema tema = new Tema(i.Est_Id, i.Est_Nombre, rd, Boolean.Parse(i.Est_Estado));
                ListaEstilos.Add(tema);                
            }
        }

        public static int getCurrentStyle()
        {
            int id = 0;
            foreach (Tema i in ListaEstilos)
            {
                if (i.Estado)
                {
                    id = i.Id;
                }
            }
            return id;
        }
    }

    public class Tema
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ResourceDictionary Ubicacion { get; set; }
        public bool Estado { get; set; }
        public Tema() { }
        public Tema(int id, string nombre, ResourceDictionary rd, bool estado)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Ubicacion = rd;
            this.Estado = estado;
        }
    }
}
