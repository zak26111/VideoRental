namespace VideoRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class populateGenre : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Genres (id,Name) values (1,'Fiction')");
            Sql("Insert into Genres (id,Name) values (2,'Drama')");
            Sql("Insert into Genres (id,Name) values (3,'Comedy')");
            Sql("Insert into Genres (id,Name) values (4,'Suspense')");
        }
        
        public override void Down()
        {
        }
    }
}
