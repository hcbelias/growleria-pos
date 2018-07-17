using Microsoft.PointOfService;
using System;
using System.Globalization;
using System.IO;

namespace GrowleriaPOS.Controllers
{
    public class PrinterController
    {
        string DeviceLogicName => "PosPrinter";
        private PosPrinter Printer { get; set; }

        public Exception Error { get; private set; }

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static PrinterController()
        {
        }

        private PrinterController()
        {
        }

        public static PrinterController Instance { get; } = new PrinterController();

        public bool PrintEmployeeCashier()
        {
            //Initialization
            DateTime nowDate = DateTime.Now;                            //System date
            DateTimeFormatInfo dateFormat = new DateTimeFormatInfo();   //Date Format
            dateFormat.MonthDayPattern = "MMMM";
            string strDate = nowDate.ToString("dd/MM/yy  HH:mm:ss", dateFormat);
            string strbcData = "4902720005074";

            //try
            //{
            //<<<step3>>>--Start
            Printer.PrintNormal(PrinterStation.Receipt, "\u001b|1B");
            //<<<step3>>>--End

            //Print address
            Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                + "123xxstreet,xxxcity,xxxxstate\n");

            //Print phone number
            Printer.PrintNormal(PrinterStation.Receipt, "\u001b|rA"
                + "TEL 9999-99-9999   C#2\n");
            //Print date
            //   \u001b|cA = Centaring char
            Printer.PrintNormal(PrinterStation.Receipt, "\u001b|cA" + strDate + "\n\n");
            //Print buying goods
            Printer.PrintNormal(PrinterStation.Receipt, "apples                  $20.00\n");

            Printer.PrintNormal(PrinterStation.Receipt, "grapes                  $30.00\n");

            Printer.PrintNormal(PrinterStation.Receipt, "bananas                 $40.00\n");

            Printer.PrintNormal(PrinterStation.Receipt, "lemons                  $50.00\n");

            Printer.PrintNormal(PrinterStation.Receipt, "oranges                 $60.00\n\n");

            //Print the total cost
            //\u001b|bC = Bold
            //\u001b|uC = Underline
            //\u001b|2C = Wide charcter
            Printer.PrintNormal(PrinterStation.Receipt, "\u001b|bC"
                + "Tax excluded.          $200.00" + "\u001b|N\n");

            Printer.PrintNormal(PrinterStation.Receipt, "\u001b|uC"
                + "Tax  5.0%               $10.00" + "\u001b|N\n");

            Printer.PrintNormal(PrinterStation.Receipt, "\u001b|bC" + "\u001b|2C"
                + "Total   $210.00" + "\u001b|N\n");
            Printer.PrintNormal(PrinterStation.Receipt, "Customer's payment     $250.00\n");
            Printer.PrintNormal(PrinterStation.Receipt, "Change                  $40.00\n\n");
            if (Printer.CapRecBarCode == true)
            {
                Printer.PrintNormal(PrinterStation.Receipt, strbcData);
                Printer.PrintBarCode(PrinterStation.Receipt, strbcData,
                    BarCodeSymbology.QRCode, 80,
                    200, PosPrinter.PrinterBarCodeCenter,
                    BarCodeTextPosition.Above);
            }
            Printer.PrintNormal(PrinterStation.Receipt, "\u001b|fP");
            this.Error = null;
            return true;
            //}
            //catch (PosControlException err)
            //{
            //    this.Error = err;
            //    return false;
            //}
        }

        public bool PrintTest()
        {
            //Initialization
            DateTime nowDate = DateTime.Now;                            //System date
            DateTimeFormatInfo dateFormat = new DateTimeFormatInfo();   //Date Format
            dateFormat.MonthDayPattern = "MMMM";
            string strDate = nowDate.ToString("dd/MM/yy  HH:mm:ss", dateFormat);
            string strbcData = "4902720005074";

            //try
            //{
            //    //<<<step3>>>--Start
            Printer.PrintNormal(PrinterStation.Receipt, "\u001b|1B");
            //<<<step3>>>--End

            //Print address
            Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                + "123xxstreet,xxxcity,xxxxstate\n");

            //Print phone number
            Printer.PrintNormal(PrinterStation.Receipt, "\u001b|rA"
                + "TEL 9999-99-9999   C#2\n");
            //Print date
            //   \u001b|cA = Centaring char
            Printer.PrintNormal(PrinterStation.Receipt, "\u001b|cA" + strDate + "\n\n");
            //Print buying goods
            Printer.PrintNormal(PrinterStation.Receipt, "apples                  $20.00\n");

            Printer.PrintNormal(PrinterStation.Receipt, "grapes                  $30.00\n");

            Printer.PrintNormal(PrinterStation.Receipt, "bananas                 $40.00\n");

            Printer.PrintNormal(PrinterStation.Receipt, "lemons                  $50.00\n");

            Printer.PrintNormal(PrinterStation.Receipt, "oranges                 $60.00\n\n");

            //Print the total cost
            //\u001b|bC = Bold
            //\u001b|uC = Underline
            //\u001b|2C = Wide charcter
            Printer.PrintNormal(PrinterStation.Receipt, "\u001b|bC"
                + "Tax excluded.          $200.00" + "\u001b|N\n");

            Printer.PrintNormal(PrinterStation.Receipt, "\u001b|uC"
                + "Tax  5.0%               $10.00" + "\u001b|N\n");

            Printer.PrintNormal(PrinterStation.Receipt, "\u001b|bC" + "\u001b|2C"
                + "Total   $210.00" + "\u001b|N\n");
            Printer.PrintNormal(PrinterStation.Receipt, "Customer's payment     $250.00\n");
            Printer.PrintNormal(PrinterStation.Receipt, "Change                  $40.00\n\n");
            if (Printer.CapRecBarCode == true)
            {
                Printer.PrintNormal(PrinterStation.Receipt, strbcData);
                Printer.PrintBarCode(PrinterStation.Receipt, strbcData,
                    BarCodeSymbology.QRCode, 80,
                    200, PosPrinter.PrinterBarCodeCenter,
                    BarCodeTextPosition.Above);
            }
            Printer.PrintNormal(PrinterStation.Receipt, "\u001b|fP");
            this.Error = null;
            return true;
            //}
            //catch (PosControlException err)
            //{
            //    this.Error = err;
            //    return false;
            //}
        }

        public bool PrintSalesTicket()
        {
            //Initialization
            DateTime nowDate = DateTime.Now;                            //System date
            DateTimeFormatInfo dateFormat = new DateTimeFormatInfo();   //Date Format
            dateFormat.MonthDayPattern = "MMMM";
            string strDate = nowDate.ToString("dd/MM/yy  HH:mm:ss", dateFormat);
            string strbcData = "4902720005074";

            try
            {
                //<<<step3>>>--Start
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|1B");
                //<<<step3>>>--End

                //Print address
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                    + "123xxstreet,xxxcity,xxxxstate\n");

                //Print phone number
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|rA"
                    + "TEL 9999-99-9999   C#2\n");
                //Print date
                //   \u001b|cA = Centaring char
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|cA" + strDate + "\n\n");
                //Print buying goods
                Printer.PrintNormal(PrinterStation.Receipt, "apples                  $20.00\n");

                Printer.PrintNormal(PrinterStation.Receipt, "grapes                  $30.00\n");

                Printer.PrintNormal(PrinterStation.Receipt, "bananas                 $40.00\n");

                Printer.PrintNormal(PrinterStation.Receipt, "lemons                  $50.00\n");

                Printer.PrintNormal(PrinterStation.Receipt, "oranges                 $60.00\n\n");

                //Print the total cost
                //\u001b|bC = Bold
                //\u001b|uC = Underline
                //\u001b|2C = Wide charcter
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|bC"
                    + "Tax excluded.          $200.00" + "\u001b|N\n");

                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|uC"
                    + "Tax  5.0%               $10.00" + "\u001b|N\n");

                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|bC" + "\u001b|2C"
                    + "Total   $210.00" + "\u001b|N\n");
                Printer.PrintNormal(PrinterStation.Receipt, "Customer's payment     $250.00\n");
                Printer.PrintNormal(PrinterStation.Receipt, "Change                  $40.00\n\n");
                if (Printer.CapRecBarCode == true)
                {
                    Printer.PrintNormal(PrinterStation.Receipt, strbcData);
                    Printer.PrintBarCode(PrinterStation.Receipt, strbcData,
                        BarCodeSymbology.QRCode, 80,
                        200, PosPrinter.PrinterBarCodeCenter,
                        BarCodeTextPosition.Above);
                }
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|fP");
                this.Error = null;
                return true;
            }
            catch (PosControlException err)
            {
                this.Error = err;
                return false;
            }
        }
        public bool OpenConnection()
        {
            string strCurDir = Directory.GetCurrentDirectory();

            //try
            //{
                PosExplorer posExplorer = new PosExplorer();
                DeviceInfo deviceInfo = null;
                //try
                //{
                deviceInfo = posExplorer.GetDevice(DeviceType.PosPrinter, this.DeviceLogicName);
                Printer = (PosPrinter)posExplorer.CreateInstance(deviceInfo);
            //}
            //catch (Exception err)
            //{
            //    this.Error = err;
            //    return false;
            //}

            Printer.Open();
            Printer.Claim(1000);
            Printer.DeviceEnabled = true;
            Printer.RecLetterQuality = true;
            this.Error = null;
            return true;
            //}
            //catch (PosControlException err)
            //{
            //    this.Error = err;
            //    return false;
            //}
        }
        public bool CloseConnection()
        {
            if (Printer != null)
            {
                try
                {
                    //Cancel the device
                    Printer.DeviceEnabled = false;

                    //Release the device exclusive control right.
                    Printer.Release();
                    return true;
                }
                //catch (PosControlException err)
                //{
                //    this.Error = err;
                //    return false;
                //}
                finally
                {
                    //Finish using the device.
                    Printer.Close();
                }
            }
            return true;
        }
    }
}
