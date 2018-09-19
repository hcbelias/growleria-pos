
using GrowleriaPOS.Controllers;
using GrowleriaPOS.Models;
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

        // POST api/values
        public IHttpActionResult Post([FromBody]CashierModel value)
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
    }
}
