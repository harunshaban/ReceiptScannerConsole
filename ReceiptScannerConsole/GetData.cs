using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptScannerConsole
{
    class GetData
    {
        // In case you want to try in error scenario
        public const string errorApiUrl = "https://interview-task-api.mca.dev/qr-scanner-codes/alpha-qr-wFpwhsQ8fkYU";
        public const string workingApiUrl = "https://interview-task-api.mca.dev/qr-scanner-codes/alpha-qr-gFpwhsQ8fkY1";

        public List<Receipt> receipts = new List<Receipt>();

        public void GetDataFromApi()
        {
            try
            {
                // Prepare the webclient for getting data externaly
                WebClient client = new WebClient();
                client.Headers["Content-type"] = "application/json";
                client.Encoding = Encoding.UTF8;
                // Getting the data in JSON
                string json = client.DownloadString(workingApiUrl);
                receipts = JsonConvert.DeserializeObject<List<Receipt>>(json);
            }
            catch(Exception e)
            {
                // Display error message
                Console.WriteLine("Error getting data");
                // Display error in log
                System.Diagnostics.Debug.WriteLine(e.ToString());
                // Ending application
                Environment.Exit(0);
            }
        }


    }
}
