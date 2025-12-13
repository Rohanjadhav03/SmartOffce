using BackendApi.Models;
using BackendApi.Repositories;
using System.Diagnostics;

namespace BackendApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _Repository;
        public EmployeeService(IEmployeeRepository Repository)
        {
            _Repository = Repository;
        }
        public async Task<bool> AddEmployeeAsync(Employee employee)
        {
            try
            {
                return await _Repository.AddEmployeeAsync(employee);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in Employee Service AddEmployeeAsync : " + ex.ToString());
                throw;
            }
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            try
            {
                return await _Repository.DeleteEmployeeAsync(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in Employee Service DeleteEmployeeAsync : " + ex.ToString());
                throw;
            }
            
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            try
            {
                return await _Repository.GetAllEmployeesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in Employee Service GetAllEmployeesAsync : " + ex.ToString());
                throw;
            }
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            try
            {
                return await _Repository.GetEmployeeByIdAsync(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in Employee Service GetEmployeeByIdAsync : " + ex.ToString());
                throw;
            }
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            try
            {
                return await _Repository.UpdateEmployeeAsync(employee);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in Employee Service UpdateEmployeeAsync : " + ex.ToString());
                throw;
            }            
        }
    }
}
