using EmployeeSever.Interfaces;
using EmployeeSever.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace EmployeeSever.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {


        public IConfiguration Configuration { get; }
        public string connectionString;
        private readonly ILogger<EmployeeRepository> _logger;
        public EmployeeRepository(IConfiguration configuration, ILogger<EmployeeRepository> logger)
        {
            this.Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            _logger = logger;
        }


        public Employee AddEmployee(Employee employee)


        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spInsertIntoEmployee]", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                    cmd.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                    cmd.Parameters.AddWithValue("@EmployeeName", employee.EmployeeSurname);
                    cmd.Parameters.AddWithValue("@EmployeeEmail", employee.EmployeeEmail);
                
                    // cmd.Parameters.AddWithValue("@ret", ParameterDirection.Output);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    //ex.Message.ToString();
                    _logger.LogError(ex, "Error at AddEmployee() :(");
                    employee = null;
                }

            }
            return employee;
        }

        public void DeleteEmployee(int? id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spDeleteEmployee]", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@EmployeeId", id);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    //ex.Message.ToString();
                    _logger.LogError(ex, "Error at DeleteEmployee() :(");

                }

            }
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spSelectEmployee]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Employee employee = new Employee();

                        employee.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                        employee.EmployeeName = rdr["EmployeeName"].ToString();
                        employee.EmployeeName = rdr["EmployeeName"].ToString();
                        employee.EmployeeEmail = rdr["EmployeeEmail"].ToString();
                        employees.Add(employee);

                    }
                    rdr.Close();
                }
                catch (Exception ex)
                {
                    //ex.Message.ToString();
                    _logger.LogError(ex, "Error at GetAllEmployees() :(");
                    employees = null;
                }
            }
            return employees;
        }

        public Employee GetEmployeeById(int id)
        {
            Employee employee = new Employee();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spSelectEmployeeById]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@EmployeeId", id);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        employee.EmployeeId = id;
                        employee.EmployeeName = rdr["Name"].ToString();
                        employee.EmployeeSurname = rdr["Address"].ToString();
                        employee.EmployeeEmail = rdr["Telephone"].ToString();
                     
                    }


                    rdr.Close();
                }
                catch (Exception ex)
                {
                    //ex.Message.ToString();
                    _logger.LogError(ex, "Error at GetEmployeeById() :(");
                    employee = null;
                }
            }
            return employee;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spUpdateEmployee]", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                    cmd.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                    cmd.Parameters.AddWithValue("@EmployeeName", employee.EmployeeSurname);
                    cmd.Parameters.AddWithValue("@EmployeeEmail", employee.EmployeeEmail);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    //ex.Message.ToString();
                    _logger.LogError(ex, "Error at UpdateEmployee() :(");
                    employee = null;
                }
            }

            return employee;
        }
    }
}
