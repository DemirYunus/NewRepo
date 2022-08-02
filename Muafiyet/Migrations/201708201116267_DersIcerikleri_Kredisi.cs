namespace Muafiyet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DersIcerikleri_Kredisi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DersIcerikleri", "Kredisi", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DersIcerikleri", "Kredisi");
        }
    }
}
