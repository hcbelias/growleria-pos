using GrowleriaPOS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrowleriaPOS 
{
    class Program
    {
        static void Main(string[] args)
        {
            PrinterController controller = new PrinterController();
            controller.OpenConnection();
            //controller.PrintSalesTicket();
            controller.CloseConnection();
        }
    }
}
