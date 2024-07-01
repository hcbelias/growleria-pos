
using GrowleriaPOS.Controllers;
using GrowleriaPOS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace GrowleriaWebPOS.Controllers
{
    class ResponseError : ResponseMessage
    {
        public ResponseError(string errorMessage, Exception exception) : base(errorMessage)
        {
            this.ErrorException = exception;
        }
        public Exception ErrorException { get; set; }
    }

    class ResponseMessage
    {
        public ResponseMessage(string message)
        {
            this.Message = message;
        }
        public String Message { get; set; }
    }
    public class PrintersController : ApiController
    {
        // GET api/values
        [Route("api/printers/test")]
        public IHttpActionResult Get()
        {
            PrinterController controller = new PrinterController();
            var connect = controller.OpenConnection();
            if (!connect)
            {
                return Json<ResponseMessage>(new ResponseError("Erro ao conectar", controller.Error));
            }
            var print = controller.PrintTest();
            var close = controller.CloseConnection();
            if (!print)
            {
                return Json<ResponseMessage>(new ResponseError("Erro ao Imprimir", controller.Error));
            }
            return Json<ResponseMessage>(new ResponseMessage("Operação concluida"));
        }

        [Route("api/version")]
        public IHttpActionResult GetVersion()
        {
            return Json<ResponseMessage>(new Controllers.ResponseMessage("v2.0.0"));
        }

        // POST api/values
        [Route("api/printers/cashier")]
        public IHttpActionResult Post([FromBody] CashierModel value)
        {
            PrinterController controller = new PrinterController();
            var connect = controller.OpenConnection();
            if (!connect)
            {
                return Json<ResponseMessage>(new ResponseError("Erro ao conectar", controller.Error));
            }
            var print = controller.PrintEmployeeCashier(value);
            var close = controller.CloseConnection();
            if (!print)
            {
                return Json<ResponseMessage>(new ResponseError("Erro ao Imprimir", controller.Error));
            }
            return Json<ResponseMessage>(new ResponseMessage("Operação concluida"));
        }

        // POST api/values
        [Route("api/printers/token")]
        public IHttpActionResult PostToken([FromBody] TokenModel value)
        {
            PrinterController controller = new PrinterController();
            var connect = controller.OpenConnection();
            if (!connect)
            {
                return Json<ResponseMessage>(new ResponseError("Erro ao conectar", controller.Error));
            }
            var token = controller.PrintSalesToken(value);
            var close = controller.CloseConnection();
            return Json(new { data = token });
        }

        [Route("api/printers/sale")]
        public IHttpActionResult PostSale([FromBody] SaleModel value)
        {
            PrinterController controller = new PrinterController();
            var connect = controller.OpenConnection();
            if (!connect)
            {
                return Json<ResponseMessage>(new ResponseError("Erro ao conectar", controller.Error));
            }
            var printClient = controller.PrintSalesDetails(value, "Cliente");
            var printUser = controller.PrintSalesDetails(value, "Vendedor");
            var close = controller.CloseConnection();
            return Json(new { printClient, printUser });
        }

        [Route("api/printers/token/many")]
        public IHttpActionResult PostManyToken([FromBody] List<TokenModel> value)
        {
            PrinterController controller = new PrinterController();
            var connect = controller.OpenConnection();
            if (!connect)
            {
                return Json<ResponseMessage>(new ResponseError("Erro ao conectar", controller.Error));
            }


            value.ForEach(item =>
            {
                controller.PrintSalesToken(item);
            });

            var close = controller.CloseConnection();


            return Json(new { data = value });
        }

        [Route("api/printers/nfce")]
        public IHttpActionResult PostNFCe([FromBody] NFCeModel nfce)
        {

            PrinterController controller = new PrinterController();
            var connect = controller.OpenConnection();
            if (!connect)
            {
                return Json<ResponseMessage>(new ResponseError("Erro ao conectar", controller.Error));
            }



            controller.PrintNFCeReceipt(nfce);

            var close = controller.CloseConnection();


            return Json(new { data = nfce});
        }
    }
}
