using GrowleriaPOS.Models;
using Microsoft.PointOfService;
using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;

namespace GrowleriaPOS.Controllers
{
    public class PrinterController
    {
        string DeviceLogicName => ConfigurationManager.AppSettings["DeviceLogicName"];
        private PosPrinter Printer { get; set; }

        public Exception Error { get; private set; }

        CultureInfo MyCultureInfo = new CultureInfo("pt-BR");

        DateTimeFormatInfo dateFormat = new DateTimeFormatInfo() { MonthDayPattern = "MMMM" };   //Date Format



        public PrinterController()
        {
        }

        private Double Convert(double number)
        {
            return Math.Round(number, 2);
        }

        public bool PrintEmployeeCashier(CashierModel cashier)
        {
            //Initialization
            DateTime nowDate = DateTime.Now;                            //System date
            DateTimeFormatInfo dateFormat = new DateTimeFormatInfo();   //Date Format
            dateFormat.MonthDayPattern = "MMMM";
            string strDate = nowDate.ToString("dd/MM/yy  HH:mm:ss", dateFormat);

            try
            {
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|1B\n\n");
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|cA\u001b|2C" + strDate + "\n\n");

                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|cA" + "\u001b|uC" + cashier.Store.Name + "\n\n");
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N" + cashier.Store.Address + "\n\n");
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N" + "CNPJ " + cashier.Store.CNPJ + "\n\n");
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N" + "Emissor " + cashier.UserOpened.Nickname + " | CPF " + cashier.UserOpened.CPF + "\n");

                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|cA" + "\u001b|uC" + "Caixa #" + cashier.CashierNumber + "\n\n");

                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N" + "Abertura da Loja às " + DateTime.Parse(cashier.CreatedAt).ToString("dd/MM/yy  HH:mm:ss", dateFormat) + "\n\n");
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N" + "Aberto Por " + cashier.UserOpened.Nickname + "\n\n");
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N" + "Fechamento da Loja às " + DateTime.Parse(cashier.CloseDate).ToString("dd/MM/yy  HH:mm:ss", dateFormat) + "\n\n");
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N" + "Fechado Por " + cashier.UserClosed.Nickname + "\n\n");
                Printer.PrintNormal(PrinterStation.Receipt, "\n");
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|cA" + "\u001b|uC" + "Físico" + "\n\n");
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N" + "Caixa Inicial - R$ " + cashier.MoneyBalance + "\n\n");
                if (cashier.MoneyBalanceDifference > 0 || cashier.MoneyBalanceDifference < 0)
                {
                    Printer.PrintNormal(PrinterStation.Receipt, "\u001b|cA" + "********************************" + "\n\n");
                    Printer.PrintNormal(PrinterStation.Receipt, "\u001b|cA" + "Caixa Inicial Calculado - R$ " + cashier.CalculatedMoneyBalance + "\n\n");
                    Printer.PrintNormal(PrinterStation.Receipt, "\u001b|cA" + "Differença - R$ " + cashier.MoneyBalanceDifference + "\n\n");
                    Printer.PrintNormal(PrinterStation.Receipt, "\u001b|cA" + "********************************" + "\n\n");
                }
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N" + "Comprovante Cartão - R$ " + cashier.BalanceCard + "\n\n");
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N" + "Dinheiro em Caixa - R$ " + cashier.BalanceMoney + "\n\n");

                if (cashier.UberDifference.HasValue)
                {
                    Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N" + "Diferença Uber R$ " + (cashier.UberDifference) + "\n\n");
                }

                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N" + "Complemento de Caixa - R$ " + (cashier.CashierDepositTotal) + "\n\n");
                //n tem venda
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N" + "Diferença Dinheiro - R$ " + cashier.MoneyDifference + "\n\n");
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N" + "Diferença Cartão - R$ " + (cashier.CardDifference) + "\n\n");
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N" + "Diferença Uber - R$ " + (cashier.UberDifference) + "\n\n");

                Printer.PrintNormal(PrinterStation.Receipt, "\n");
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|cA" + "\u001b|uC" + "Recebimento" + "\n\n");
                if (cashier.BusTicketValue != 0)
                {
                    Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N" + "Valor do Vale Transporte - R$ " + Math.Round(cashier.BusTicketValue, 2) + "\n\n");
                }

                String userRole = string.IsNullOrWhiteSpace(cashier.UserRoleOpened) ? cashier.UserOpened.Role : cashier.UserRoleOpened;
                Boolean showSalaryValues = true;
                switch (userRole)
                {
                    case "employee":
                    case "manager":
                        showSalaryValues = false;
                        break;
                    default: break;
                }

                String workedHoursLine = "Horas Trabalhadas (" + cashier.StandardHoursDate + ")";
                String workedAdditionalHoursLine = "Horas Adicionais Trabalhadas (" + cashier.AdditionalHoursDate + ")";
                if (showSalaryValues)
                {
                    workedHoursLine += " - R$ " + Math.Round(cashier.PaymentStandardHours, 2);
                    workedAdditionalHoursLine += " - R$ " + Math.Round(cashier.PaymentAdditionalHours, 2);
                }

                if (Math.Abs(cashier.PaymentStandardHours) >= 0.01)
                {
                    Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N" + workedHoursLine + "\n\n");
                }

                if (Math.Abs(cashier.PaymentAdditionalHours) >= 0.01)
                {
                    Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N" + workedAdditionalHoursLine + "\n\n");
                }

                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N" + "Comissão R$ " + Math.Round(cashier.EmployeeComission, 2) + "\n\n");
                

                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N" + "Recebimento Total R$ " + Math.Round(cashier.EmployeeTotalPaymentAtMoment, 2) + "\n\n");
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N" + "Retirada para Malote R$ " + cashier.MoneyOverWithdrawLimit + "\n\n");
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N" + "Saldo para Próximo Caixa R$ " + Math.Round(cashier.NextCashierBalance) + "\n\n");



                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N" + "\n\n\n");


                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N" + "\u001b|cA" + "\u001b|uC" + "Assinatura                                  \n\n");
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|cA" + cashier.UserNameOpened + "\n\n");
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|cA" + "CPF " + cashier.UserOpened.CPF + "\n\n");
                if (!string.IsNullOrWhiteSpace(cashier.Store.CashierReceiptDescription))
                {
                    Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N" + "\n\n");
                    Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N" + cashier.Store.CashierReceiptDescription + "\n\n");
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

        private String formatPrice(long price)
        {
            return string.Format("{0:0.00}", price);

        }
        public bool PrintSalesDetails(SaleModel sale, String receipt)
        {
            string strbcData = sale.Id;
            try
            {
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|1B");
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|bC" + "\u001b|cA" + "Via do " + receipt + "\n\n");
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|bC" + "\u001b|cA" + sale.StoreName + "\n");
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|bC" + "Venda: " + sale.Id + "\n");
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|bC" + "Hora da Venda: " + DateTime.Parse(sale.CreatedAt, this.MyCultureInfo).ToString("dd/MM/yy HH:mm:ss", this.dateFormat) + "\n\n");

                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|bC" + "\u001b|2C" + "Produtos" + "\n");
                sale.ProductList.ForEach(item =>
                {
                    PrintProductDetail(item);
                });

                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|bC" + "\u001b|2C" + "Total        R$" + formatPrice(sale.Total) + "\u001b|N\n\n\n");
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|bC" + "\u001b|2C" + "Pagamento" + "\n");
                if (sale.PaymentMoney != 0)
                {
                    Printer.PrintNormal(PrinterStation.Receipt, "Dinheiro     R$" + formatPrice(sale.PaymentMoney) + "\n");
                }
                if (sale.PaymentCard != 0)
                {
                    Printer.PrintNormal(PrinterStation.Receipt, "Cartão       R$" + formatPrice(sale.PaymentCard) + "\n");
                }
                if (sale.PaymentUber != 0)
                {
                    Printer.PrintNormal(PrinterStation.Receipt, "Uber         R$" + formatPrice(sale.PaymentUber) + "\n");
                    Printer.PrintNormal(PrinterStation.Receipt, "Usuário Uber:  " + sale.UberUser + "\n");
                    Printer.PrintNormal(PrinterStation.Receipt, "Código Uber: " + sale.UberCode + "\n");
                }
                if (sale.PaymentToken != 0)
                {
                    Printer.PrintNormal(PrinterStation.Receipt, "Fichas       R$" + formatPrice(sale.PaymentToken) + "\u001b|N\n");
                }
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|bC" + "\u001b|2C" + "Total Pago   R$" + formatPrice((sale.PaymentMoney + sale.PaymentCard + sale.PaymentUber + sale.PaymentToken)) + "\u001b|N\n\n\n");
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|fP");


                return true;
            }
            catch (PosControlException err)
            {
                return false;
            }
        }

        private void PrintProductDetail(ProductModel productModel)
        {


            var textPrint = productModel.IsKeg ? productModel.TotalVolume + "L  " : productModel.TotalUnits + "un.";
            Printer.PrintNormal(PrinterStation.Receipt, "\u001b|bC" + productModel.ProductName + "\n");
            Printer.PrintNormal(PrinterStation.Receipt, textPrint + "   -  R$" + formatPrice(productModel.TotalSale) + "\n");
        }

        public TokenModel PrintSalesToken(TokenModel token)
        {
            string strbcData = token.Id;
            try
            {
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|1B");
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|bC" + "\u001b|cA" + token.StoreName + "\n\n\n");
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|bC" + "\u001b|2C" + "\u001b|cA" + token.ProductName + "\n");
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|bC" + "\u001b|cA" + token.ProviderName + "\n\n");
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|bC" + "\u001b|2C" + "\u001b|cA" + "Preço R$" + string.Format("{0:0.00}", token.Price) + "\n");
                if (token.Volume.HasValue)
                {
                    Printer.PrintNormal(PrinterStation.Receipt, "\u001b|bC" + "\u001b|2C" + "\u001b|cA" + token.Volume + "ml  \n");
                }
                Printer.PrintBarCode(PrinterStation.Receipt, strbcData,
                        BarCodeSymbology.QRCode, 80,
                        200, PosPrinter.PrinterBarCodeCenter,
                        BarCodeTextPosition.Above);
                Printer.PrintNormal(PrinterStation.Receipt, "\n\u001b|cA" + strbcData + "\n\n");
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|bC" + "\u001b|cA" + "Gerado em " + DateTime.Parse(token.CreatedAt, this.MyCultureInfo).ToString("dd/MM/yy HH:mm:ss", this.dateFormat) + "\n");
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|bC" + "\u001b|cA" + "Garantido o valor por 30 dias" + "\n");
                Printer.PrintNormal(PrinterStation.Receipt, "\u001b|fP");
                var now = DateTime.UtcNow;
                token.TimePrinted = now.ToString("s") + "." + now.Millisecond + "Z";
                return token;
            }
            catch (PosControlException err)
            {
                token.TimePrinted = null;
                token.Error = err;
                return token;
            }
        }
        public bool OpenConnection()
        {
            string strCurDir = Directory.GetCurrentDirectory();

            try
            {
                PosExplorer posExplorer = new PosExplorer();
                DeviceInfo deviceInfo = posExplorer.GetDevice(DeviceType.PosPrinter, this.DeviceLogicName);
                Printer = (PosPrinter)posExplorer.CreateInstance(deviceInfo);
                Printer.Open();
                Printer.Claim(1000);
                Printer.DeviceEnabled = true;
                //Printer.AsyncMode = true;
                Printer.RecLetterQuality = true;
                this.Error = null;
                return true;
            }
            catch (PosControlException err)
            {
                this.Error = err;
                if (this.Printer != null)
                {
                    this.Printer.Close();
                }
                return false;
            }
            catch (Exception err)
            {
                this.Error = err;
                if (this.Printer != null)
                {
                    this.Printer.Close();
                }

                return false;
            }
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
                catch (PosControlException err)
                {
                    this.Error = err;
                    return false;
                }
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
