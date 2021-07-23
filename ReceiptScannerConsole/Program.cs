using System;

namespace ReceiptScannerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsumeApi objsync = new ConsumeApi();
            objsync.GetAllData();
        }
    }
}
