using EmployeeClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;




namespace EmployeeClient.Controllers
{
    public class EmployeeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Employee> employeeList = new List<Employee>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:43224/api/Employee"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    employeeList = JsonConvert.DeserializeObject<List<Employee>>(apiResponse);
                }
            }
            return View(employeeList);
        }
        public ViewResult GetEmployee() => View();




        [HttpPost]
        public async Task<IActionResult> GetEmployee(int id)
        {
            Employee employee = new Employee();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:43224/api/Employee/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    employee = JsonConvert.DeserializeObject<Employee>(apiResponse);
                }
            }
            return View(employee);
        }
        [HttpGet]

        public ViewResult AddEmployee() => View();

        [HttpPost]

        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("http://localhost:43224/api/Employee", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        employee = JsonConvert.DeserializeObject<Employee>(apiResponse);
                    }
                }
                return View(employee);
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateEmployee(int id)
        {
            Employee employee = new Employee();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:43224/api/Employee/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    employee = JsonConvert.DeserializeObject<Employee>(apiResponse);
                }
            }
            return View(employee);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(Employee employee)
        {
            Employee receivedEmployee = new Employee();
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(employee.EmployeeId.ToString()), "EmployeeId");
                    content.Add(new StringContent(employee.EmployeeName), "EmployeeName");
                    content.Add(new StringContent(employee.EmployeeSurname), "EmployeeSurname");
                    content.Add(new StringContent(employee.EmployeeEmail), "EmployeeEmail");

                    using (var response = await httpClient.PutAsync("http://localhost:43224/api/Employee", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ViewBag.Result = "Success";
                        receivedEmployee = JsonConvert.DeserializeObject<Employee>(apiResponse);
                    }
                }
            }
            return View(receivedEmployee);
        }



        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(int EmployeeId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:43224/api/Employee/" + EmployeeId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }



    }

}

