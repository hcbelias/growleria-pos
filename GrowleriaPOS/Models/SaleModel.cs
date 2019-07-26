using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using System.Web;

namespace GrowleriaPOS.Models
{
    [DataContract]

    public class PriceModel
    {
        [DataMember(Name = "price")]
        public long Price { get; set; }
    }

    [DataContract]
    public class SaleModel
    {
        [DataMember(Name = "storeName")]
        public string StoreName { get; set; }

        [DataMember(Name = "userName")]
        public string UserName { get; set; }

        [DataMember(Name = "userEmail")]
        public string UserEmail { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "updatedAt")]
        public string UpdatedAt { get; set; }

        [DataMember(Name = "createdAt")]
        public string CreatedAt { get; set; }

        [DataMember(Name = "discountTokenList")]
        public List<PriceModel> DiscountTokenList { get; set; }

        [DataMember(Name = "employeeDiscountPercentage")]
        public double EmployeeDiscountPercentage { get; set; }

        [DataMember(Name = "employeeDiscount")]
        public bool EmployeeDiscount { get; set; }

        [DataMember(Name = "uberUser")]
        public string UberUser { get; set; }

        [DataMember(Name = "uberCode")]
        public string UberCode { get; set; }

        [DataMember(Name = "isDelivery")]
        public bool IsDelivery { get; set; }

        [DataMember(Name = "paymentUber")]
        public long PaymentUber { get; set; }

        [DataMember(Name = "paymentMoney")]
        public long PaymentMoney { get; set; }

        [DataMember(Name = "paymentToken")]
        public long PaymentToken { get; set; }

        [DataMember(Name = "paymentCard")]
        public long PaymentCard { get; set; }

        [DataMember(Name = "total")]
        public long Total { get; set; }

        [DataMember(Name = "product")]
        public List<ProductModel> ProductList
        {
            get; set;
        }

        [DataMember(Name = "error")]
        public Exception Error { get; set; }

    }
}