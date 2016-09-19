namespace Classroom.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TookOutGrade : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Assignments", "Grade");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Assignments", "Grade", c => c.String());
        }
    }
}
