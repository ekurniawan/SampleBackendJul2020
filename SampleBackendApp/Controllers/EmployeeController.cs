using SampleBackendApp.DAL;
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
        private EmployeeDAL _empDAL;
        public EmployeeController()
        {
            _empDAL = new EmployeeDAL();
        }

        // GET: api/Employee
        public IEnumerable<Employee> Get()
        {
            return _empDAL.GetAllDapper();
        }

        [Route("api/Employee/GetByName")]
        public IEnumerable<Employee> GetByName(string name)
        {
            return _empDAL.GetByName(name);
        }

        // GET: api/Employee/5
        public Employee Get(int id)
        {
            var result = _empDAL.GetById(id);
            return result;
        }

        // POST: api/Employee
        public IHttpActionResult Post(Employee emp)
        {
            try
            {
                _empDAL.Insert(emp);
                return Ok($"Data Employee {emp.EmpName} berhasil ditambahkan");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // PUT: api/Employee/5
        public IHttpActionResult Put(Employee emp)
        {
            try
            {
                _empDAL.Update(emp);
                return Ok($"Data employee {emp.EmpName} berhasil diupdate");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // DELETE: api/Employee/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _empDAL.Delete(id);
                return Ok($"Data employee berhasil di delete");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
