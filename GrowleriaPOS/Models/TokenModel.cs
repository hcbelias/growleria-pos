using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace GrowleriaPOS.Models
{
    [DataContract]
    public class TokenModel
    {
        [DataMember(Name ="_id")]
        public String Id { get; set;  }

        [DataMember(Name ="createdAt")]
        public String CreatedAt { get; set;  }

        [DataMember(Name ="updatedAt")]
        public String UpdatedAt{ get; set;  }

        [DataMember(Name ="storeName")]
        public String StoreName{ get; set;  }

        [DataMember(Name ="productName")]
        public String ProductName{ get; set;  }

        [DataMember(Name ="providerName")]
        public String ProviderName{ get; set;  }

        [DataMember(Name ="price")]
        public Double Price{ get; set;  }

        [DataMember(Name ="volume")]
        public Double? Volume{ get; set;  }

        [DataMember(Name ="timePrinted")]
        public String TimePrinted{ get; set;  }

        [DataMember(Name ="timeSendToPrinter")]
        public DateTime? TimeSendToPrinter{ get; set;  }

        [DataMember(Name ="error")]
        public Exception Error { get; set; }
    }
}