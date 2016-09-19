namespace Classroom.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBackGradeToAssignmentClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assignments", "Grade", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Assignments", "Grade");
        }
    }
}
