using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace GrowleriaPOS.Models
{
    [DataContract]
    public class NFCeItemModel
    {
        [DataMember(Name = "product")]
        public string Product { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "ncm")]
        public string Ncm { get; set; }

        [DataMember(Name = "cfop")]
        public string Cfop { get; set; }

        [DataMember(Name = "cest")]
        public string Cest { get; set; }

        [DataMember(Name = "commercialValue")]
        public decimal CommercialValue { get; set; }

        [DataMember(Name = "taxValue")]
        public decimal TaxValue { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "createdAt")]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
}