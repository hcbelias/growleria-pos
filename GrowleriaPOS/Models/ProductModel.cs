using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace GrowleriaPOS.Models
{
    [DataContract]
    public class ProductModel
    {

        [DataMember(Name = "productName")]
        public String ProductName { get; set; }

        [DataMember(Name = "provider")]
        public String Provider { get; set; }

        [DataMember(Name = "totalUnits")]
        public double? TotalUnits { get; set; }

        [DataMember(Name = "totalVolume")]
        public double? TotalVolume { get; set; }

        [DataMember(Name = "isKeg")]
        public Boolean IsKeg { get; set; }
              
   
        [DataMember(Name = "totalSale")]
        public long TotalSale { get; set; }

    }
}