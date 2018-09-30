using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace GrowleriaPOS.Models
{
    [DataContract]
    public class CashierModel
    {
        [DataMember(Name ="createdAt")]
        public String CreatedAt { get; set;  }
        [DataMember(Name ="updatedAt")]
        public String UpdatedAt{ get; set;  }
        [DataMember(Name ="closeDate")]
        public String CloseDate { get; set;  }
        [DataMember(Name ="store")]
       public StoreModel Store{ get; set;  }
        [DataMember(Name = "userClosed")]
        public UserModel UserClosed { get; set;  }
        [DataMember(Name ="userEmailClosed")]
        public String UserEmailClosed { get; set;  }
        [DataMember(Name ="userNameClosed")]
        public String UserNameClosed { get; set;  }
        [DataMember(Name = "userOpened")]
        public UserModel UserOpened { get; set;  }
        [DataMember(Name ="userEmailOpened")]
        public String UserEmailOpened { get; set;  }
        [DataMember(Name ="userNameOpened")]
        public String UserNameOpened { get; set;  }
        [DataMember(Name ="totalAccountBalance")]
        public String TotalAccountBalance{ get; set;  }
        [DataMember(Name ="moneyBalance")]
        public String MoneyBalance{ get; set;  }
        [DataMember(Name ="justifiyCard")]
        public String JustifyCard{ get; set;  }
        [DataMember(Name ="balanceCard")]
        public String BalanceCard{ get; set;  }
        [DataMember(Name ="justifyMoney")]
        public String JustifyMoney { get; set;  }
        [DataMember(Name ="balanceMoney")]
        public String BalanceMoney{ get; set;  }
        [DataMember(Name ="cashierNumber")]
        public String CashierNumber{ get; set;  }
        [DataMember(Name ="positiveMoneyDifference")]
        public String PositiveMoneyDifference{ get; set;  }
        [DataMember(Name ="negativeMoneyDifference")]
        public String NegativeMoneyDifference{ get; set;  }
        [DataMember(Name ="positiveCardDifference")]
        public String PositiveCardDifference{ get; set;  }
        [DataMember(Name ="negativeCardDifference")]
        public String NegativeCardDifference{ get; set;  }
    }
}