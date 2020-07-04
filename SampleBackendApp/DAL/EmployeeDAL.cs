using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using SampleBackendApp.Models;
using Dapper;
using System.Security.AccessControl;
using System.Runtime.Serialization;

namespace SampleBackendApp.DAL
{
    public class EmployeeDAL
    {
        private string GetConn()
        {
            return WebConfigurationManager.ConnectionStrings["MyConnectionString"]
                .ConnectionString;
        }

        public IEnumerable<Employee> GetAll()
        {
            List<Employee> lstEmployee = new List<Employee>();
            using (SqlConnection conn = new SqlConnection(GetConn()))
            {
                string strSql = @"select * from Employees order by EmpName asc";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        lstEmployee.Add(new Employee
                        {
                            EmpId = Convert.ToInt32(dr["EmpId"]),
                            EmpName = dr["EmpName"].ToString(),
                            Department = dr["Department"].ToString(),
                            Designation = dr["Designation"].ToString(),
                            Qualification = dr["Qualification"].ToString()
                        });
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return lstEmployee;
        }


        public IEnumerable<Employee> GetAllDapper()
        {
            using (SqlConnection conn = new SqlConnection(GetConn()))
            {
                string strSql = @"select * from Employees order by EmpName asc";
                var results = conn.Query<Employee>(strSql);
                return results;
            }
        }

        public Employee GetById(int empId)
        {
            using (SqlConnection conn = new SqlConnection(GetConn()))
            {
                string strSql = @"select * from Employees 
                                  where EmpId=@EmpId";
                var param = new { EmpId = empId };
                var result = conn.QuerySingleOrDefault<Employee>(strSql, param);
                return result;
            }
        }

        public IEnumerable<Employee> GetByName(string name)
        {
            using (SqlConnection conn = new SqlConnection(GetConn()))
            {
                string strSql = @"select * from Employees where EmpName like @EmpName";
                var param = new { EmpName = '%'+name+'%' };
                return conn.Query<Employee>(strSql, param);
            }
        }

        public void Insert(Employee emp)
        {
            using (SqlConnection conn = new SqlConnection(GetConn()))
            {
                string strSql = @"insert into 
                                  Employees(EmpName,Designation,Department,Qualification) 
                                  values(@EmpName,@Designation,@Department,@Qualification)";
                var param = new { EmpName=emp.EmpName,Designation=emp.Designation,
                    Department=emp.Department,Qualification=emp.Qualification};
                try
                {
                    conn.Execute(strSql, param);
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception(sqlEx.Message);
                }
            }
        }

        public void Update(Employee emp)
        {
            using (SqlConnection conn = new SqlConnection(GetConn()))
            {
                string strSql = @"update Employees set EmpName=@EmpName,
                Designation=@Designation,Department=@Department,Qualification=@Qualification 
                where EmpId=@EmpId";
                var param = new
                {
                    EmpName = emp.EmpName,
                    Designation = emp.Designation,
                    Department = emp.Department,
                    Qualification = emp.Qualification,
                    EmpId = emp.EmpId
                };
                try
                {
                    conn.Execute(strSql, param);
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception(sqlEx.Message);   
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(GetConn()))
            {
                string strSql = @"delete from Employees where EmpId=@EmpId";
                var param = new { EmpId = id };
                try
                {
                    conn.Execute(strSql, param);
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception(sqlEx.Message);
                }
            }
        }
    }
}