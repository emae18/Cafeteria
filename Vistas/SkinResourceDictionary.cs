using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
namespace Vistas
{
    public class SkinResourceDictionary : ResourceDictionary
    {
        private Uri defaultSkin;

        public Uri DefaultSkin
        {
            get { return defaultSkin; }
            set { defaultSkin = value;
            UpdateSource();
            }
        }

        private Uri black_orangeSkin;

        public Uri Black_orangeSkin
        {
            get { return black_orangeSkin; }
            set { black_orangeSkin = value;
            UpdateSource();
            }
        }
        private void UpdateSource()
        {
            // Condicion ? verdadero : falso
            var val = App.SkinSeleccionado == App.skins.Blue ? DefaultSkin : Black_orangeSkin;
            if (val != null && base.Source != val)
                base.Source = val;
        }
    }
}
