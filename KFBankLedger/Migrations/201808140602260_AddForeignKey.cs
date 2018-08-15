namespace KFBankLedger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FK : DbMigration
    {
        public override void Up()
        {

           AddForeignKey("dbo.Transactions", "BankAccount_BankAccountId", "dbo.BankAccounts", "BankAccountId");
  
        }
        
        public override void Down()
        {
             
           DropForeignKey("dbo.Transactions", "BankAccount_BankAccountId", "dbo.BankAccounts");
  
        }
    }
}
