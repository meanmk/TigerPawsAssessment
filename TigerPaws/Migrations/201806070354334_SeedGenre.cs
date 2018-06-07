namespace TigerPaws.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedGenre : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Id, Name) VALUES (1, 'Teddy')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (2, 'Meanie Kids')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (3, 'Animal With Joints')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (4, 'Animal With No Joints')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (5, 'Human')");
        }
        
        public override void Down()
        {
        }
    }
}
