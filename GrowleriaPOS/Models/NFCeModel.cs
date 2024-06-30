using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace GrowleriaPOS.Models
{
    [DataContract]
    public class NFCeModel
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "payments")]
        public List<NFCePaymentModel> Payments { get; set; }

        [DataMember(Name = "itens")]
        public List<NFCeItemModel> Items { get; set; }

        [DataMember(Name = "cnpjStore")]
        public string CnpjStore { get; set; }

        [DataMember(Name = "sale")]
        public string Sale { get; set; }

        [DataMember(Name = "cashier")]
        public string Cashier { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "sentDate")]
        public string SentDate { get; set; }

        [DataMember(Name = "authorizationDate")]
        public string AuthorizationDate { get; set; }

        [DataMember(Name = "cStat")]
        public string CStat { get; set; }

    }
}