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
            foreach (Receipt recd in domestic)
            {
                Console.WriteLine("..." + recd.name);
                Console.WriteLine(" Price: $" + recd.price);
                string str = recd.description.Substring(0, 10);
                Console.WriteLine("   " + str + "...");
                if (recd.weight.Equals(0))
                    Console.WriteLine("   Weight: " + "N/A");
                else
                    Console.WriteLine("   Weight: " + recd.weight);
            }
            Console.WriteLine(".Imported");
            foreach (Receipt reci in imported)
            {
                Console.WriteLine("..." + reci.name);
                Console.WriteLine(" Price: $" + reci.price);
                string str = reci.description.Substring(0, 10);
                Console.WriteLine("   " + str + "...");
                if (reci.weight.Equals(0))
                    Console.WriteLine("   Weight: " + "N/A");
                else
                    Console.WriteLine("   Weight: " + reci.weight);
            }

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
