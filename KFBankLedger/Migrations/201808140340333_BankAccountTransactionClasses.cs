namespace KFBankLedger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BankAccountTransactionClasses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        BankAccountId = c.Guid(nullable: false),
                        BankAccountNumber = c.Int(nullable: false, identity: true),
                        BankAccountName = c.String(maxLength: 50),
                        BankAccountDateCreated = c.DateTimeOffset(nullable: false, precision: 7),
                        BankAccountType = c.Int(nullable: false),
                        BankAccountBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.BankAccountId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);

            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionId = c.Guid(nullable: false),
                        TransactionDateCreated = c.DateTimeOffset(nullable: false, precision: 7),
                        TransactionType = c.Int(nullable: false),
                        TransactionAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransactionDescription = c.String(maxLength: 400),
                        BankAccount_BankAccountId = c.Guid(),
                    })
                .PrimaryKey(t => t.TransactionId)
                .Index(t => t.BankAccount_BankAccountId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.BankAccounts", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transactions", "BankAccount_BankAccountId", "dbo.BankAccounts");
            DropIndex("dbo.Transactions", new[] { "BankAccount_BankAccountId" });
            DropIndex("dbo.BankAccounts", new[] { "User_Id" });
            DropTable("dbo.Transactions");
            DropTable("dbo.BankAccounts");
        }
    }
}

