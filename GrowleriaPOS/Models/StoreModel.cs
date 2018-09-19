using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GrowleriaPOS.Models
{
    [DataContract]
    public class StoreModel
    {
        [DataMember(Name="name")]
        public String Name { get; set; }
        [DataMember(Name="address")]
        public String Address{ get; set; }
        [DataMember(Name="cnpjNumber")]
        public String CNPJ{ get; set; }
        [DataMember(Name="phoneNumber")]
        public String PhoneNumber{ get; set; }
    
    }
}
