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
        public const string apiUrl = "x";

        public void GetAllData() // Get all data from external api
        {
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.DownloadString(apiUrl);
            var receipts = JsonConvert.DeserializeObject<List<Receipt>>(json);

            foreach (Receipt receipt in receipts)
            {
                Console.WriteLine("..." + receipt.name);
                Console.WriteLine("   " + receipt.price);
                Console.WriteLine("   " + receipt.description);
                Console.WriteLine("   " + receipt.weight);
                Console.WriteLine("   " + receipt.domestic);
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
