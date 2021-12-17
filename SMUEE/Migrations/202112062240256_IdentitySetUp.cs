namespace SMUEE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdentitySetUp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SM_ROLES",
                c => new
                    {
                        PK_Roles = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.PK_Roles)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.SM_USUARIO_ROLES",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.SM_ROLES", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.SM_USUARIO", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SM_USUARIO",
                c => new
                    {
                        PK_Usuario = c.String(nullable: false, maxLength: 128),
                        NB_Primero = c.String(),
                        NB_Segundo = c.String(),
                        AP_Primero = c.String(),
                        AP_Segundo = c.String(),
                        PasswordChanged = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        Telefono = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.PK_Usuario)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.SM_USUARIO_CLAIM",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SM_USUARIO", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.SM_USUARIO_LOGIN",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.SM_USUARIO", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SM_USUARIO_ROLES", "UserId", "dbo.SM_USUARIO");
            DropForeignKey("dbo.SM_USUARIO_LOGIN", "UserId", "dbo.SM_USUARIO");
            DropForeignKey("dbo.SM_USUARIO_CLAIM", "UserId", "dbo.SM_USUARIO");
            DropForeignKey("dbo.SM_USUARIO_ROLES", "RoleId", "dbo.SM_ROLES");
            DropIndex("dbo.SM_USUARIO_LOGIN", new[] { "UserId" });
            DropIndex("dbo.SM_USUARIO_CLAIM", new[] { "UserId" });
            DropIndex("dbo.SM_USUARIO", "UserNameIndex");
            DropIndex("dbo.SM_USUARIO_ROLES", new[] { "RoleId" });
            DropIndex("dbo.SM_USUARIO_ROLES", new[] { "UserId" });
            DropIndex("dbo.SM_ROLES", "RoleNameIndex");
            DropTable("dbo.SM_USUARIO_LOGIN");
            DropTable("dbo.SM_USUARIO_CLAIM");
            DropTable("dbo.SM_USUARIO");
            DropTable("dbo.SM_USUARIO_ROLES");
            DropTable("dbo.SM_ROLES");
        }
    }
}
