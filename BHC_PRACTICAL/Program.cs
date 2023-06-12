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

            double rebate = 0.05;

            double netValueOfTobaccoDeliveredAfterRebate = ApplyRebate(grossValueOfTobaccoDelivered, rebate);
            Console.WriteLine($"Net value of tobacco delivered after rebate: ${netValueOfTobaccoDeliveredAfterRebate}");

            Rebate rebateToApply = new Rebate()
            {
                isFlatRate = false,
                flatRateAmount = 10,
                isFixedRatePerKg = true,
                fixedRatePerKgAmount = 0.02
            };

            ProcessSale(bales, debts, rebateToApply);

            BaleExtensions.Reworks(bales);


            // Create a list of bales to select from
            List<Bale> bales1 = new List<Bale>();
            bales.Add(new Bale("110000010", "TMOS", 4.50, 120));
            bales.Add(new Bale("110000011", "TLOS", 5.50, 110));
            bales.Add(new Bale("110000012", "TXLF", 350.00, 130));
            bales.Add(new Bale("110000013", "TMOX", 0.50, 90));
            bales.Add(new Bale("110000014", "TM1L", 1.50, 80));

            // Set the agreed price and number of bales dynamically
            double agreedPrice = 5.00;
            int numberOfBales = 3;

            // Select the qualifying bales
            List<Bale> qualifyingBales = SelectBale(bales, agreedPrice, numberOfBales);

            // Get the totals for the selected bales
            Tuple<int, double, double, double> totals = GetTotals(qualifyingBales);

            Console.WriteLine("Total Number of Bales Selected: " + totals.Item1);
            Console.WriteLine("Total Mass Selected: " + totals.Item2);
            Console.WriteLine("Average Price Selected: " + totals.Item3);
            Console.WriteLine("Total Gross Selected: " + totals.Item4);

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
        static double ApplyRebate(double grossValueOfTobaccoDelivered, double rebate)
        {
            double rebateAmount = grossValueOfTobaccoDelivered * rebate;

            double netValueOfTobaccoDeliveredAfterRebate = grossValueOfTobaccoDelivered + rebateAmount;

            return netValueOfTobaccoDeliveredAfterRebate;
        }
        static void ProcessSale(List<Bale> bales, List<Debt> debts, Rebate rebateToApply)
        {
            double grossValueOfTobaccoDelivered = bales.Sum(bale => bale.price * bale.mass);

            Console.WriteLine($"Gross value of tobacco delivered: ${grossValueOfTobaccoDelivered}");

            if (rebateToApply.isFlatRate)
            {
                grossValueOfTobaccoDelivered += rebateToApply.flatRateAmount;
                Console.WriteLine($"Applied flat rate rebate of ${rebateToApply.flatRateAmount}. Gross value of tobacco delivered after rebate: ${grossValueOfTobaccoDelivered}");
            }
            else if (rebateToApply.isFixedRatePerKg)
            {
                double rebateAmount = bales.Sum(bale => bale.mass * rebateToApply.fixedRatePerKgAmount);
                grossValueOfTobaccoDelivered += rebateAmount;

                Console.WriteLine($"Applied fixed rate per kg rebate of ${rebateToApply.fixedRatePerKgAmount}. Gross value of tobacco delivered after rebate: ${grossValueOfTobaccoDelivered}");
            }

            foreach (var debt in debts.OrderBy(debt => debt.priority))
            {
                if (debt.hasInterest)
                {
                    debt.amount += debt.amount * debt.interestRate;
                    Console.WriteLine($"Added interest to debt with priority {debt.priority}. Amount: ${debt.amount}");
                }

                grossValueOfTobaccoDelivered -= debt.amount;

                Console.WriteLine($"Processed debt with priority {debt.priority}. Amount: ${debt.amount}");
            }

            
        }
        static List<Bale> SelectBale(List<Bale> bales, double agreedPrice, int numberOfBales)
        {
            // Filter out the bales with grades containing X
            List<Bale> filteredBales = bales.Where(b => !b.grade.Contains("X")).ToList();

            // Sort the filtered bales by price in descending order
            filteredBales.Sort((b1, b2) => -1 * b1.price.CompareTo(b2.price));

            // Select the qualifying bales
            List<Bale> qualifyingBales = filteredBales.Where(b => b.price >= agreedPrice).Take(numberOfBales).ToList();

            return qualifyingBales;
        }
        static Tuple<int, double, double, double> GetTotals(List<Bale> selectedBales)
        {
            int totalNumber = selectedBales.Count;
            double totalMass = selectedBales.Sum(b => b.mass);
            double averagePrice = selectedBales.Average(b => b.price);
            double totalGross = selectedBales.Sum(b => (b.mass * b.price));

            return Tuple.Create(totalNumber, totalMass, averagePrice, totalGross);
        }
    }
}
