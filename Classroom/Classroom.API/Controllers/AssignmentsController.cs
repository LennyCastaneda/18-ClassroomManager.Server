using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Classroom.API.Infrastructure;
using Classroom.API.Models;

namespace Classroom.API.Controllers
{
    public class AssignmentsController : ApiController
    {
        private ClassroomDataContext db = new ClassroomDataContext();

        // GET: api/Assignments
        public IQueryable<Assignment> GetAssignments()
        {
            return db.Assignments;
        }

        // GET: api/Assignments/5
        [ResponseType(typeof(Assignment))]
        // Add the {studentId}/{projectId} to ending URL to connect composite keys to grades
        [HttpGet, Route("api/assignments/{studentId}/{projectId}")]
        public IHttpActionResult GetAssignment(int studentId, int projectId)
        {
            // Assignment assignment = db.Assignments.Find(id);

            var assignment = db.Assignments.Find(studentId, projectId);

            if (assignment == null)
            {
                return NotFound();
            }

            return Ok(assignment);
        }

        // PUT: api/Assignments/5
        [ResponseType(typeof(void))]
        // Add the {studentId}/{projectId} to ending URL to connect composite keys to grades
        [HttpPut, Route("api/assignments/{studentId}/{projectId}")]
        //Changed from int id
        public IHttpActionResult PutAssignment(int studentId, int projectId, Assignment assignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (id != assignment.StudentId)
            if (studentId != assignment.StudentId || projectId != assignment.ProjectId)
            {
                return BadRequest();
            }

            db.Entry(assignment).State = EntityState.Modified;

            // var assignmentToBeUpdated = db.Assignments.FirstOrDefault(a => a.StudentId == studentId && a.ProjectId == projectId);

            // db.Entry(assignmentToBeUpdated).CurrentValues.SetValues(assignment);
            // db.Entry(assignmentToBeUpdated).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                // if (!AssignmentExists(id)
                if (!AssignmentExists(studentId, projectId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Assignments
        [ResponseType(typeof(Assignment))]
        public IHttpActionResult PostAssignment(Assignment assignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Assignments.Add(assignment);

            try
            {
                db.SaveChanges();

                // Allowing auto refresh data on front end 
                assignment.Project = db.Projects.Find(assignment.ProjectId);
                assignment.Student = db.Students.Find(assignment.StudentId);
            }
            catch (DbUpdateException)
            {
                if (AssignmentExists(assignment.StudentId, assignment.ProjectId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            // Problem was not having project and student names populating in the assignment object.

            // Explicity assigned the incoming assignment to the corresponding Projects and Students names from the database.
            // Allowing auto refresh data on front end 
            //assignment.Project = db.Projects.Find(assignment.ProjectId);
            //assignment.Student = db.Students.Find(assignment.StudentId);
            // Added grades
            //assignment.Student = db.Students.Find(assignment.AssignmentGrade);

            // Take the info from the APi post and the added assignment student and project names with associated grade and return it back to the assignment object in the front-end.
            return CreatedAtRoute("DefaultApi", new { id = assignment.StudentId }, assignment);
        }

        // DELETE: api/Assignments/5
        [ResponseType(typeof(Assignment))]
        [HttpDelete, Route("api/assignments/{studentId}/{projectId}")]
        public IHttpActionResult DeleteAssignment(int studentId, int projectId)
        {
            Assignment assignment = db.Assignments.Find(studentId, projectId);
            if (assignment == null)
            {
                return NotFound();
            }

            db.Assignments.Remove(assignment);
            db.SaveChanges();

            return Ok(assignment);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // private bool AssignmentExists(int Id)
        private bool AssignmentExists(int studentId, int projectId)
        {
            //return db.Assignments.Count( e => e.StudentId == id)
            return db.Assignments.Count(e => e.StudentId == studentId && e.ProjectId == projectId) > 0;
        }
    }
}