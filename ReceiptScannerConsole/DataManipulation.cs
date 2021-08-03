using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptScannerConsole
{
    class DataManipulation
    {
        GetData getDataApi = new GetData();
        Functions functions = new Functions();

        public void GetAllDataOrganized() // Get all data from external api
        {
            // Getting data from external source
            getDataApi.GetDataFromApi();

            // Create two lists to save grouped data
            List<Receipt> domestic = new List<Receipt>();
            List<Receipt> imported = new List<Receipt>();

            // Grouping the data and added in specified Lists above
            foreach (Receipt receipt in getDataApi.receipts)
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
            functions.sortAlphabeticly(domestic); // Sorting the "domestic" group of List
            foreach (Receipt recd in domestic)
            {
                // Display the data of domestic group in specified scope
                functions.displayData(recd.name, recd.price, recd.description, recd.weight);
            }
            Console.WriteLine(".Imported");
            functions.sortAlphabeticly(imported); // Sorting the "imported" group of List
            foreach (Receipt reci in imported)
            {
                // Display the data of imported group in specified scope
                functions.displayData(reci.name, reci.price, reci.description, reci.weight);
            }
            functions.endingOutput(domestic, imported);
        }
    }
}
