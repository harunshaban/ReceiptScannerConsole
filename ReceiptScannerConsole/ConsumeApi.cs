using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptScannerConsole
{
    class ConsumeApi
    {
        public const string apiUrl = "https://interview-task-api.mca.dev/qr-scanner-codes/alpha-qr-gFpwhsQ8fkY1";

        Receipt receiptA = new Receipt();

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
        public decimal calcPrice(decimal fPriceCalc, List<Receipt> receiptsCalc)
        {
            decimal calc = 0;
            foreach (Receipt rec in receiptsCalc)
                calc += rec.price;
            return calc;
        }

        // Sorting data in alphabetic order
        public void sortAlphabeticly(List<Receipt> receiptsSort)
        {
            receiptsSort.Sort((u1, u2) => u1.name.CompareTo(u2.name));

            /* NOTE:
             * I could have been done in logic:
             * Copy, sort in place, then copy the sorted list back.
             * But that method is inefficient
             * It could Have been done also with LINQ, but I choose this way
             * (No reason)           
            */

        }

        public void GetAllData() // Get all data from external api
        {
            // Prepare the webclient for getting data externaly
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            // Getting the data in JSON
            string json = client.DownloadString(apiUrl);
            var receipts = JsonConvert.DeserializeObject<List<Receipt>>(json);

            // Create two lists to save grouped data
            List<Receipt> domestic = new List<Receipt>();
            List<Receipt> imported = new List<Receipt>();

            // Grouping the data and added in specified Lists above
            foreach (Receipt receipt in receipts)
            {
                if (receipt.domestic == true)
                {
                    domestic.Add(
                        new Receipt
                        {
                            name = receipt.name,
                            domestic = receipt.domestic,
                            price = receipt.price,
                            weight = receipt.weight,
                            description = receipt.description
                        }
                        );
                }
                if (receipt.domestic == false)
                {
                    imported.Add(
                        new Receipt
                        {
                            name = receipt.name,
                            domestic = receipt.domestic,
                            price = receipt.price,
                            weight = receipt.weight,
                            description = receipt.description
                        }
                        );
                }
            }

            Console.WriteLine(".Domestic");
            sortAlphabeticly(domestic); // Sorting the "domestic" group of List
            foreach (Receipt recd in domestic)
            {
                // Display the data of domestic group in specified scope
                displayData(recd.name, recd.price, recd.description, recd.weight);
            }
            Console.WriteLine(".Imported");
            sortAlphabeticly(imported); // Sorting the "imported" group of List
            foreach (Receipt reci in imported)
            {
                // Display the data of imported group in specified scope
                displayData(reci.name, reci.price, reci.description, reci.weight);
            }

            // Calculating the total cost of domestic group
            Console.WriteLine("Domestic cost: $" + calcPrice(receiptA.price, domestic));
            // Calculating the total cost of imported group
            Console.WriteLine("Domestic cost: $" + calcPrice(receiptA.price, imported));
            Console.WriteLine("Domestic count: " + domestic.Count);
            Console.WriteLine("Imported count: " + imported.Count);

            /* Works fine but displays data in Json format with all brackets
            using (var client = new WebClient()) // create instance for web client
            {
                client.Headers.Add("Content-Type:application/json"); // Type of content
                var result = client.DownloadString(apiUrl); // URI
                Console.Write(Environment.NewLine + result);
            }
            */
        }


        /* Note:
             * This file could have been done more clear
             * by creating seperate classes for the first
             * three methods, but as for the task assignment
             * and reviewing purposes I choose to set everything
             * in one file.
             * Thank You.
            */



    }
}
