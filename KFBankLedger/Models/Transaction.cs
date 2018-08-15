using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KFBankLedger.Models
{
    public enum TransactionType
    {
        [Display(Name = "Deposit Cash")]
        DepositCash = 0,
        [Display(Name = "Deposit Cheque")]
        DepositCheque = 1,
        [Display(Name = "Withdraw Cash")]
        WithdrawCash = 2,
        [Display(Name = "Withdraw Cashier's Cheque")]
        WithdrawalCashiersCheque = 3
    }

    public class Transaction
    {
        [Key]
        public Guid TransactionId { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}")]
        public DateTimeOffset TransactionDateCreated { get; set; }

        [Display(Name = "Type")]
        public TransactionType TransactionType { get; set; }

        // TODO: Add validation
        [Display(Name = "Amount")]
        [DataType(DataType.Currency)]
         public decimal TransactionAmount { get; set; }

        [Display(Name = "Description")]
        [MaxLength(400)]
        public string TransactionDescription { get; set; }

        public Transaction()
        {
            TransactionDateCreated = DateTime.Today;
        }

        public virtual BankAccount BankAccount { get; set; }

    }
}