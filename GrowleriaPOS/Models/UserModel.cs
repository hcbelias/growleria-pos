using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GrowleriaPOS.Models
{
    [DataContract]
    public class UserModel
    {
        [DataMember(Name="name")]
        public String Name { get; set; }
        [DataMember(Name="cpf")]
        public String CPF{ get; set; }
        [DataMember(Name="email")]
        public String Email{ get; set; }
        [DataMember(Name="hourlyRate")]
        public String HourlyRate{ get; set; }
    
    }
}
