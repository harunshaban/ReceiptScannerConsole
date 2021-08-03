using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptScannerConsole
{
    class Functions
    {
        // Display the data in console
        public void displayData(string fName, decimal fPrice, string fDesc, int fWeight)
        {
            Console.WriteLine("..." + fName);
            Console.WriteLine("   Price: $" + fPrice);
            // If the description is more than 10 character
            string str = fDesc.Substring(0, 10);
            Console.WriteLine("   " + str + "...");
            // If weight is 0 and/or null
            if (fWeight.Equals(0))
                Console.WriteLine("   Weight: " + "N/A");
            else
                Console.WriteLine("   Weight: " + fWeight);
        }

        // Calculating the price of group
        public decimal calcPrice(List<Receipt> receiptsCalc)
        {
            decimal calc = 0;
            foreach (Receipt rec in receiptsCalc)
                calc += rec.price;
            return calc;
        }

        // Sorting data in alphabetic order
        public void sortAlphabeticly(List<Receipt> receiptsSort)
        {
            // Verify that list is not empty
            if (!receiptsSort.Count.Equals(0))
                receiptsSort.Sort((u1, u2) => u1.name.CompareTo(u2.name));
            else
                Console.WriteLine("List is empty");
        }

        public void endingOutput(List<Receipt> d, List<Receipt> i)
        {
            // Calculating the total cost of domestic group
            Console.WriteLine("Domestic cost: $" + calcPrice(d));
            // Calculating the total cost of imported group
            Console.WriteLine("Domestic cost: $" + calcPrice(i));
            
            Console.WriteLine("Domestic count: " + d.Count);
            Console.WriteLine("Imported count: " + i.Count);
        }
    }
}
