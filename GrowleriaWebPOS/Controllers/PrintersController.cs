
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
        //public IHttpActionResult PostNFCe([FromBody] NFCeModel value)
        public IHttpActionResult GetNFCe()
        {
            var nfce = JsonConvert.DeserializeObject<NFCeModel>("{\"nfce\":{\"id\":\"6681c3d1907dc26c8f6a35ac\",\"payments\":[{\"value\":10,\"paymentMethod\":\"04\",\"description\":\"6681c3d1907dc26c8f6a34e2\",\"_id\":\"6681c3d1907dc26c8f6a35ad\",\"createdAt\":\"2024-06-30T20:45:05.547Z\",\"updatedAt\":\"2024-06-30T20:45:05.547Z\",\"id\":\"6681c3d1907dc26c8f6a35ad\",\"paymentDescription\":\"Cartão de Débito\"}],\"itens\":[{\"product\":\"5a38476db2d1c30014d76f25\",\"description\":\"AUSTRIA PILSEN - Half Pint - 300ml\",\"ncm\":\"22030000\",\"cfop\":\"5405\",\"cest\":\"0202000\",\"commercialValue\":10,\"taxValue\":10,\"_id\":\"6681c3d1907dc26c8f6a35ae\",\"createdAt\":\"2024-06-30T20:45:05.547Z\",\"updatedAt\":\"2024-06-30T20:45:05.547Z\",\"id\":\"6681c3d1907dc26c8f6a35ae\"}],\"cnpjStore\":\"26732707000186\",\"sale\":\"6681c3d1907dc26c8f6a34e2\",\"cashier\":\"6664d919750e9adf9e6c69f2\",\"status\":\"approved\",\"printNfce\":true,\"createdAt\":\"2024-06-30T20:45:05.547Z\",\"updatedAt\":\"2024-06-30T20:45:11.963Z\",\"_v\":0,\"externalId\":\"6681c3d29effc6d2982592ac\",\"sentDate\":\"30/06/2024\",\"authorizationDate\":\"30/06/2024\",\"cStat\":\"100\",\"message\":\"Autorizado o uso da NF-e\",\"pdfLink\":\"https://api.plugnotas.com.br/nfce/6681c3d29effc6d2982592ac/pdf\",\"token\":\"31240626732707000186650010000000591166683323\",\"value\":10,\"xmlLink\":\"https://api.plugnotas.com.br/nfce/6681c3d29effc6d2982592ac/xml\",\"id\":\"6681c3d1907dc26c8f6a35ac\",\"statusDescription\":\"Nota Emitida\"}}");

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
