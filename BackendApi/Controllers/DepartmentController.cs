using BackendApi.Models;
using BackendApi.Repositories;
using BackendApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _Service;
        public DepartmentController(IDepartmentService Service)
        {
            _Service = Service;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetallDepartments()
        {
            try
            {
                List<Department> list = await _Service.GetAllDepartmentAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllDepartments Controller Error: " + ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("get-by-id/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            try
            {
                Department dep = await _Service.GetDeparmentByIdAsync(id);
                if (dep==null)
                {
                    return NotFound("Department Not Found");
                }
                return Ok(dep);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetDepartmentById Controller Error: " + ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddDepartments([FromBody] Department Dep)
        {
            try
            {
                bool result = await _Service.AddDepartmentAsync(Dep);
                if (result)
                {
                    return Ok("Department Added Successfully");
                }
                return BadRequest("Failed to add department");
            }
            catch (Exception ex)
            {
                Console.WriteLine("AddDepartment Controller Error: " + ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateDepartments([FromBody] Department Dep)
        {
            try
            {
                bool result = await _Service.UpdateDepartmentAsync(Dep);
                if (result)
                {
                    return Ok("Department Updated Successfully");
                }
                return BadRequest("Department not found or update failed");
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpdateDepartments Controller Error: " + ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteDepartments(int id)
        {
            try
            {
                bool result = await _Service.DeleteDepartmentAsync(id);
                if (result)
                {
                    return Ok("Department Deleted Successfully");
                }
                return BadRequest("Department not found or failed to Delete");
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeleteDepartments Controller Error: " + ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
