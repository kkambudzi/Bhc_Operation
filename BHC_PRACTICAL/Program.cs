using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHC_PRACTICAL
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Bale> bales = new List<Bale>()

        {
            new Bale() { grower = "123456", mass = 115, price = 2.50, grade = "TMOS", barcode = "110000011" },
            new Bale() { grower = "123456", mass = 85, price = 4.50, grade = "TLOS", barcode = "110000012" },
            new Bale() { grower = "123456", mass = 95, price = 5.50, grade = "TLOS", barcode = "110000013" }
        };
            double grossValueOfTobaccoDelivered = CalculateGross(bales);
            Console.WriteLine($"Gross value of tobacco delivered: ${grossValueOfTobaccoDelivered}");

            Console.ReadLine();
        }
        static double CalculateGross(List<Bale> bales)
        {
            return bales.Sum(bale => bale.mass * bale.price);
        }
    }
}
