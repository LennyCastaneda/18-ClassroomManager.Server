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
    public class DashboardController : ApiController
    {
        private ClassroomDataContext _db;

        public DashboardController()
        {
            _db = new ClassroomDataContext();
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(new
            {
                StudentCount = _db.Students.Count(),
                ProjectCount = _db.Projects.Count(),
                AssignmentCount = _db.Assignments.Count()

            });
        }
    }
}