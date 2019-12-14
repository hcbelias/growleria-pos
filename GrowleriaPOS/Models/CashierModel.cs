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
        [DataMember(Name = "createdAt")]
        public String CreatedAt { get; set; }
        [DataMember(Name = "updatedAt")]
        public String UpdatedAt { get; set; }
        [DataMember(Name = "closeDate")]
        public String CloseDate { get; set; }
        [DataMember(Name = "store")]
        public StoreModel Store { get; set; }
        [DataMember(Name = "userClosed")]
        public UserModel UserClosed { get; set; }
        [DataMember(Name = "userEmailClosed")]
        public String UserEmailClosed { get; set; }
        [DataMember(Name = "userNameClosed")]
        public String UserNameClosed { get; set; }
        [DataMember(Name = "userOpened")]
        public UserModel UserOpened { get; set; }
        [DataMember(Name = "userEmailOpened")]
        public String UserEmailOpened { get; set; }
        [DataMember(Name = "userNameOpened")]
        public String UserNameOpened { get; set; }
        [DataMember(Name = "userRoleOpened")]
        public String UserRoleOpened { get; set; }
        [DataMember(Name = "totalAccountBalance")]
        public double TotalAccountBalance { get; set; }
        [DataMember(Name = "moneyBalance")]
        public double MoneyBalance { get; set; }
        [DataMember(Name = "calculatedMoneyBalance")]
        public double CalculatedMoneyBalance { get; set; }
        [DataMember(Name = "moneyBalanceDifference")]
        public double MoneyBalanceDifference { get; set; }
        [DataMember(Name = "balanceCard")]
        public double BalanceCard { get; set; }
        [DataMember(Name = "balanceMoney")]
        public double BalanceMoney { get; set; }
        [DataMember(Name = "balanceUber")]
        public double BalancUber { get; set; }
        [DataMember(Name = "cashierNumber")]
        public String CashierNumber { get; set; }
        [DataMember(Name = "positiveMoneyDifference")]
        public double PositiveMoneyDifference { get; set; }
        [DataMember(Name = "negativeMoneyDifference")]
        public double NegativeMoneyDifference { get; set; }
        [DataMember(Name = "positiveCardDifference")]
        public double PositiveCardDifference { get; set; }
        [DataMember(Name = "negativeCardDifference")]
        public double NegativeCardDifference { get; set; }
        [DataMember(Name = "positiveUberDifference")]
        public double? PositiveUberDifference { get; set; }
        [DataMember(Name = "negativeUberDifference")]
        public double? NegativeUberDifference { get; set; }
        [DataMember(Name = "totalPaymentCard")]
        public double TotalPaymentCard { get; internal set; }
        [DataMember(Name = "totalPaymentUber")]
        public double? TotalPaymentUber { get; internal set; }
        [DataMember(Name = "totalPaymentMoney")]
        public double TotalPaymentMoney { get; internal set; }
        [DataMember(Name = "totalPayment")]
        public double TotalPayment { get; internal set; }
        [DataMember(Name = "moneyDifference")]
        public double MoneyDifference { get; internal set; }
        [DataMember(Name = "uberDifference")]
        public double? UberDifference { get; internal set; }
        [DataMember(Name = "cardDifference")]
        public double CardDifference { get; internal set; }
        [DataMember(Name = "employeeCommission")] //recebimento por comissao
        public double EmployeeComission { get; internal set; }
        [DataMember(Name = "employeePayment")] //recebimento por horas trabalhasdas
        public double EmployeePayment { get; internal set; }
        [DataMember(Name = "employeeTotalPayment")] //recebimento total
        public double EmployeeTotalPayment { get; internal set; }
        [DataMember(Name = "moneyOverWithdrawLimit")] //retirada para malote
        public double MoneyOverWithdrawLimit { get; internal set; }
        [DataMember(Name = "nextCashierBalance")] //SAldo para prox caixa
        public double NextCashierBalance { get; internal set; }
        [DataMember(Name = "cashierDepositTotal")]
        public double CashierDepositTotal { get; internal set; }
        [DataMember(Name = "paymentStandardHours")]
        public double PaymentStandardHours{ get; internal set; }
        [DataMember(Name = "standardHoursDate")]
        public string StandardHoursDate{ get; internal set; }
        [DataMember(Name = "paymentAdditionalHours")]
        public double PaymentAdditionalHours{ get; internal set; }
        [DataMember(Name = "additionalHoursDate")]
        public string AdditionalHoursDate{ get; internal set; }
        [DataMember(Name = "busTicketValue")]
        public double BusTicketValue{ get; internal set; }
    }

}