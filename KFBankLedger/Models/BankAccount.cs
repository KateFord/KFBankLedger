using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KFBankLedger.Models
{
    public enum AccountType {Checking = 0, Savings = 1, Loan = 3  }

    public class BankAccount
    {
        [Key]
        public Guid BankAccountId { get; set; }

        [Display(Name = "Account Name")]
        [MaxLength(50)]
        public string BankAccountName { get; set; }

        // TODO: Add validation
        [Display(Name = "Account Number")]
        public int BankAccountNumber { get; set; }

        [Display(Name = "Opened On")]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}")]
        public DateTimeOffset BankAccountDateCreated { get; set; }

        [Display(Name = "Account Type")]
        public AccountType BankAccountType { get; set; }

        // TODO: Add validation
        [Display(Name = "Balance")]
        [DataType(DataType.Currency)]
        public decimal BankAccountBalance { get; set; }

        public BankAccount()
        {
        }

        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}