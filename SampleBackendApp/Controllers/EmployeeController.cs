using SampleBackendApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SampleBackendApp.Controllers
{
    public class EmployeeController : ApiController
    {
        // GET: api/Employee
        public IEnumerable<Employee> Get()
        {
            List<Employee> lstEmp = new List<Employee>
            {
                new Employee{EmpId=1,EmpName="Erick",Qualification="Android Studio",
                    Department="IT",Designation="Mobile Dev"},
                new Employee{EmpId=2,EmpName="Roger",Qualification="Phonegap",
                    Department="IT",Designation="Mobile Dev"},
            };
            return lstEmp;
        }

        // GET: api/Employee/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Employee
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Employee/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Employee/5
        public void Delete(int id)
        {
        }
    }
}
