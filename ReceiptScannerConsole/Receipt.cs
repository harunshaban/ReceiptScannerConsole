using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptScannerConsole
{
    class Receipt
    {
        public string name { get; set; }
        public bool domestic { get; set; }
        public decimal price { get; set; }
        public int weight { get; set; }
        public string description { get; set; }
    }
}
