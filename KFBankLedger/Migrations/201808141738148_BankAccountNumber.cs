namespace KFBankLedger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BankAccountNumber : DbMigration
    {
        public override void Up()
        {
            //TODO: This did not remove IDENTITY
            AlterColumn("dbo.BankAccounts", "BankAccountNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BankAccounts", "BankAccountNumber", c => c.Int(nullable: false, identity: true));
        }
    }
}
