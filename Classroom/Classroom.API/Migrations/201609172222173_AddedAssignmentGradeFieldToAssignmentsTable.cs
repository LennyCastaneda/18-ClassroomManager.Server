namespace Classroom.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAssignmentGradeFieldToAssignmentsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assignments", "AssignmentGrade", c => c.String());
            DropColumn("dbo.Assignments", "Grade");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Assignments", "Grade", c => c.String());
            DropColumn("dbo.Assignments", "AssignmentGrade");
        }
    }
}
