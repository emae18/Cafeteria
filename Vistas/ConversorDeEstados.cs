using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Globalization;
using System.Windows.Data;

namespace Vistas
{
    public class ConversorDeEstados : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //MessageBox.Show("aqui");
            if (value.ToString() == "Pendiente")
            {
                return "Red";
            }
            else if (value.ToString() == "Pagada")
            {
                return "Green";
            }
            else if (value.ToString() == "Contabilizada")
            {
                return "Blue";
            }
            else if (value.ToString() == "Anulada")
            {
                return "Gray";
            }
            else
                return "Transparent";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
