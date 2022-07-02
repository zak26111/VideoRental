namespace VideoRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class populateMemberShipType : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into MemberShipTypes (id,Name,SignUpFee,DurationInMonth,DiscountRate) values (1,'Pay As You Go',0,0,0)");
            Sql("Insert into MemberShipTypes (id,Name,SignUpFee,DurationInMonth,DiscountRate) values (2,'Monthly',15,1,10)");
            Sql("Insert into MemberShipTypes (id,Name,SignUpFee,DurationInMonth,DiscountRate) values (3,'Quaterly',40,3,15)");
            Sql("Insert into MemberShipTypes (id,Name,SignUpFee,DurationInMonth,DiscountRate) values (4,'Yearly',160,12,20)");

        }
        
        public override void Down()
        {
        }
    }
}
