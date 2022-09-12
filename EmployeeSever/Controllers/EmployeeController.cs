using EmployeeSever.Interfaces;
using EmployeeSever.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSever.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeRepository employeeRepository;

        private IWebHostEnvironment webHostEnvironment;

        public EmployeeController(IEmployeeRepository repo, IWebHostEnvironment environment)
        {
            employeeRepository = repo;
            webHostEnvironment = environment;
        }

        //public ReservationController(IRepository repo) => repository = repo;
        [HttpGet]
        public IEnumerable<Employee> GetCustomers()
        {
            return employeeRepository.GetAllEmployees().ToList();
        }

        [HttpGet("{id}")]
        public Employee GetCustomerById(int id)
        {
            return employeeRepository.GetEmployeeById(id);
        }



        [HttpPost]
        public Employee Create([FromBody] Employee employee)
        {
            return employeeRepository.AddEmployee(employee);
        }



        [HttpPut]
        public Employee Update([FromForm] Employee employee)
        {
            return employeeRepository.UpdateEmployee(employee);
        }


        [HttpDelete("{id}")]
        public void Delete(int? id) => employeeRepository.DeleteEmployee(id);

    }
}

