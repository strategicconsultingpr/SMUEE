namespace SMUEE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProfileImgPath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SM_USUARIO", "ProfileImgPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SM_USUARIO", "ProfileImgPath");
        }
    }
}
