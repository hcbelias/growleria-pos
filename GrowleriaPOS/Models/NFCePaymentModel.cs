using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace GrowleriaPOS.Models
{
    [DataContract]
    public class NFCePaymentModel
    {
        [DataMember(Name = "value")]
        public decimal Value { get; set; }

        [DataMember(Name = "paymentMethod")]
        public string PaymentMethod { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "createdAt")]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [DataMember(Name = "paymentDescription")]
        public string PaymentDescription { get; set; }
    }
}