namespace TigerPaws.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'7d03a138-a2c5-4530-ad5e-9f90ce0d1eee', N'guest@vidly.com', 0, N'AAp+/fkIpcHimlR8rd/yFv4+EU9DHxKL+rHBhSpbG8j+cyNmZuO1nUBJF5KE+7mEDw==', N'8c546bcd-7e40-4b2d-80f2-f3f9ad9e7027', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'bf12e7a2-0306-41eb-a0fa-3e1325eca044', N'admin@tigerpaws.com', 0, N'AF1elTND+rgcfGU2HBTLOoAxVsD9td4VAg64hx1tCUwdL20KNBghqGDKUNVaVaeEIA==', N'3a9672ac-3725-42a5-bedb-210720d7d40a', NULL, 0, 0, NULL, 1, 0, N'admin@tigerpaws.com')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'd45d6b84-3c80-4360-acdf-b384359d287a', N'CanManageProducts')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'bf12e7a2-0306-41eb-a0fa-3e1325eca044', N'd45d6b84-3c80-4360-acdf-b384359d287a')

");
        }
        
        public override void Down()
        {
        }
    }
}
