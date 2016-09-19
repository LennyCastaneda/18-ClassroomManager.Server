using Classroom.API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Classroom.API.Infrastructure
{
    public class ClassroomDataContext : DbContext
    {
        public ClassroomDataContext() : base("ClassroomManager")
        {

        }

        // Get the IDSet for all 3 classes to tell Entiy Framework to design these 3 tables
        public IDbSet<Assignment> Assignments { get; set; }
        public IDbSet<Project> Projects { get; set; }
        public IDbSet<Student> Students { get; set; }


        // Explicity model the relationships between these 3 entities (classes). Project has many Assignmennts, Student has many Assignments, etc.
        // Code-First Fluent API
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Compound Key (set up to find foreign key relationships.
            // explicitiy model compound key by selecting Assignment entity
            modelBuilder.Entity<Assignment>()
                        // Use .HasKey method with lambda expression creating a new annonimious object 
                        // (with values) containing both foreign keys (StudentId and ProjectId) that form the compound key.
                        .HasKey(a => new { a.StudentId, a.ProjectId});
            
            // A Project has many Assignments
            modelBuilder.Entity<Project>()
                        // A Project has many Assignments
                        .HasMany(p => p.Assignments)
                        // An Assignment is with a required Project object
                        .WithRequired(a => a.Project)
                        // And this Assignment has a foreign key of ProjectId
                        .HasForeignKey(a => a.ProjectId);

            // A Student has many Assignments
            modelBuilder.Entity<Student>()
                        // A Student has many Assignments
                        .HasMany(p => p.Assignments)
                        // An Assignment is with a required Student object 
                        .WithRequired(a => a.Student)
                        // And this Assignment has a foreign key of StudentId
                        .HasForeignKey(a => a.StudentId);

        }

    }
}