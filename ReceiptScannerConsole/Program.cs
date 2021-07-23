using System;

namespace ReceiptScannerConsole
{

    /* Author: Harun Shaban
     * Project finnished date: 23.07.2021
     * For further info check:
     * https://github.com/harunshaban/ReceiptScannerConsole
     */

    class Program
    {
        static void Main(string[] args)
        {
            ConsumeApi objsync = new ConsumeApi();
            objsync.GetAllData();
        }
    }
}
