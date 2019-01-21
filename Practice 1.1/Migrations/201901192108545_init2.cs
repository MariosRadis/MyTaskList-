namespace Practice_1._1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.UserRoles", name: "User_Id", newName: "UserID");
            RenameColumn(table: "dbo.UserRoles", name: "Role_Id", newName: "RoleID");
            RenameIndex(table: "dbo.UserRoles", name: "IX_User_Id", newName: "IX_UserID");
            RenameIndex(table: "dbo.UserRoles", name: "IX_Role_Id", newName: "IX_RoleID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.UserRoles", name: "IX_RoleID", newName: "IX_Role_Id");
            RenameIndex(table: "dbo.UserRoles", name: "IX_UserID", newName: "IX_User_Id");
            RenameColumn(table: "dbo.UserRoles", name: "RoleID", newName: "Role_Id");
            RenameColumn(table: "dbo.UserRoles", name: "UserID", newName: "User_Id");
        }
    }
}
