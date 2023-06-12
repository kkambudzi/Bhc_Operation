using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHC_PRACTICAL
{
    public class Bale
    {
        public string grower;
        public double mass;
        public double price;
        public string grade;
        public string barcode;

        public Bale()
        {

        }
        public Bale(string Grower, double Mass, double Price, string Grade, string Barcode)
        {
            grower= Grower;
            mass = Mass;
            price = Price;
            grade = Grade;
            barcode = Barcode;
            
          
           
        }
        public Bale(string Barcode, string Grade, double Price, double Mass)
        {
            barcode = Barcode;
            grade = Grade;
            price = Price;
            mass = Mass;
        }

    }
}
