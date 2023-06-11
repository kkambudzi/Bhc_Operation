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

            double netValueOfTobaccoDeliveredAfterTaxes = CalculateNet(bales);
            Console.WriteLine($"Net value of tobacco delivered after taxes: ${netValueOfTobaccoDeliveredAfterTaxes}");

            double commission = 0.005;
            double debt = 500;
            double interestRate = 0.1;

            double netValueOfTobaccoDeliveredAfterDebt = CalculateNet(grossValueOfTobaccoDelivered, commission, debt, interestRate);
            Console.WriteLine($"Net value of tobacco delivered after debt: ${netValueOfTobaccoDeliveredAfterDebt}");

            List<Debt> debts = new List<Debt>()
        {
            new Debt() { priority = 1, amount = 1000, hasInterest = true, interestRate = 0.1 },
            new Debt() { priority = 2, amount = 500, hasInterest = false },
            new Debt() { priority = 3, amount = 2000, hasInterest = true, interestRate = 0.05 }
        };



            ProcessDebts(debts, grossValueOfTobaccoDelivered, commission);

            Console.ReadLine();
        }
        static double CalculateGross(List<Bale> bales)
        {
            return bales.Sum(bale => bale.mass * bale.price);
        }
        static double CalculateNet(List<Bale> bales)
        {
            double grossValueOfTobaccoDelivered = CalculateGross(bales);

            double tax1 = grossValueOfTobaccoDelivered * 0.003;
            double tax2 = (grossValueOfTobaccoDelivered / 100) * 1.5 + (bales.Sum(bale => bale.mass) * 0.02);
            double tax3 = bales.Count * 5;

            double netValueOfTobaccoDeliveredAfterTaxes = grossValueOfTobaccoDelivered - tax1 - tax2 - tax3;

            return netValueOfTobaccoDeliveredAfterTaxes;
        }

        static double CalculateNet(double grossValueOfTobaccoDelivered, double commission, double debt, double interestRate)
        {
            double interest = debt * interestRate;
            double totalDebt = debt + interest;
            double commissionOnDebt = totalDebt * commission;

            double netValueOfTobaccoDeliveredAfterDebt = grossValueOfTobaccoDelivered - totalDebt - commissionOnDebt;

            return netValueOfTobaccoDeliveredAfterDebt;
        }

        static void ProcessDebts(List<Debt> debts, double grossValueOfTobaccoDelivered, double commission)
        {
            debts.Sort((d1, d2) => d1.priority.CompareTo(d2.priority));

            foreach (var debt in debts)
            {
                if (debt.hasInterest)
                {
                    debt.amount += debt.amount * debt.interestRate;
                }

                double commissionOnDebt = debt.amount * commission;

                grossValueOfTobaccoDelivered -= (debt.amount + commissionOnDebt);

                Console.WriteLine($"Processed debt with priority {debt.priority}. Amount: ${debt.amount}. Commission on debt: ${commissionOnDebt}");
            }

            Console.WriteLine($"Total commission: ${debts.Sum(debt => debt.amount * commission)}");
            Console.WriteLine($"Remaining gross: ${grossValueOfTobaccoDelivered}");
        }
    }
}
