using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public partial class PartialClass
    {
        private string descProd;

        public string DescProd
        {
            get { return descProd; }
            set { descProd = value; }
        }
        private string colorProd;

        public string ColorProd
        {
            get { return colorProd; }
            set { colorProd = value; }
        }
        private decimal precProd;

        public decimal PrecProd
        {
            get { return precProd; }
            set { precProd = value; }
        }
        private int cantProd;

        public int CantProd
        {
            get { return cantProd; }
            set { cantProd = value; }
        }
        private decimal precFinalVent;

        public decimal PrecFinalVent
        {
            get { return precFinalVent; }
            set { precFinalVent = decimal.Round(value, 2, MidpointRounding.AwayFromZero); ; }
        }
        public PartialClass()
        {
        }


    }
}
