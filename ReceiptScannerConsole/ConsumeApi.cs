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

        public void displayData(string fName, decimal fPrice, string fDesc, int fWeight)
        {
            Console.WriteLine("..." + fName);
            Console.WriteLine("   Price: $" + fPrice);
            string str = fDesc.Substring(0, 10);
            Console.WriteLine("   " + str + "...");
            if (fWeight.Equals(0))
                Console.WriteLine("   Weight: " + "N/A");
            else
                Console.WriteLine("   Weight: " + fWeight);
        }
        public decimal calcPrice(decimal fPriceCalc, List<Receipt> receiptsCalc)
        {
            decimal calc = 0;
            foreach (Receipt rec in receiptsCalc)
                calc += rec.price;
            return calc;
        }
        public void sortAlphabeticly(List<Receipt> receiptsSort)
        {
            receiptsSort.Sort((u1, u2) => u1.name.CompareTo(u2.name));
        }

        public void GetAllData() // Get all data from external api
        {
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.DownloadString(apiUrl);
            var receipts = JsonConvert.DeserializeObject<List<Receipt>>(json);

            List<Receipt> domestic = new List<Receipt>();
            List<Receipt> imported = new List<Receipt>();

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
            sortAlphabeticly(domestic);
            foreach (Receipt recd in domestic)
            {
                displayData(recd.name, recd.price, recd.description, recd.weight);
            }
            Console.WriteLine(".Imported");
            sortAlphabeticly(imported);
            foreach (Receipt reci in imported)
            {
                displayData(reci.name, reci.price, reci.description, reci.weight);
            }

            Console.WriteLine("Domestic cost: $" + calcPrice(receiptA.price, domestic));
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

    }
}
