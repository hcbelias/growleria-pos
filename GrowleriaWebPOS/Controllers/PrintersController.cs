
using GrowleriaPOS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GrowleriaWebPOS.Controllers
{
    public class PrintersController : ApiController
    {
        // GET api/values
        public bool Get()
        {
            PrinterController controller = PrinterController.Instance;
            var connect = controller.OpenConnection();
            if (!connect)
                return false;
            var print = controller.PrintTest();
            if (!print)
                return false;
            var close = controller.CloseConnection();
            return close;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
