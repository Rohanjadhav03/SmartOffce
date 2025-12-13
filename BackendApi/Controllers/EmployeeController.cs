using BackendApi.Models;
using BackendApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Diagnostics;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _Service;
        public EmployeeController(IEmployeeService Service)
        {
            _Service = Service;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                List<Employee> list = await _Service.GetAllEmployeesAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error In EmployeeController GetAllEmployees : "+ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("get-by-id/{id}")]
        public async Task<IActionResult> GetEmployeebyId(int id)
        {
            try
            {
                if (id<=0)
                {
                    return BadRequest();
                }
                Employee emp = await _Service.GetEmployeeByIdAsync(id);
                if (emp==null)
                {
                    return NotFound("Employee Not Found");
                }
                return Ok(emp);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error In EmployeeController GetEmployeebyId : " + ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest("Invalid employee data");
                }
                bool result  = await _Service.AddEmployeeAsync(employee);
                if (result)
                {
                    return Ok("Employee Added Successfully");
                }
                return BadRequest("Failed to add employee");

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error In EmployeeController AddEmployee : " + ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateEmployee([FromBody] Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest("Invalid employee data");
                }
                bool result = await _Service.UpdateEmployeeAsync(employee);
                if (result)
                {
                    return Ok("Employee Updated Successfully");
                }
                return BadRequest("Failed to update employee");

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error In EmployeeController UpdateEmployee : " + ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                bool result = await _Service.DeleteEmployeeAsync(id);
                if (result)
                {
                    return Ok("Employee Deleted Successfully");
                }
                return BadRequest("Failed to delete employee");

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error In EmployeeController DeleteEmployee : " + ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
