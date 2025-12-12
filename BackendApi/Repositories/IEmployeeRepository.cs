using BackendApi.Models;

namespace BackendApi.Repositories
{
    public interface IEmployeeRepository
    {
        public Task<List<Employee>> GetAllEmployeesAsync();
        public Task<Employee> GetEmployeeByIdAsync(int id);
        public Task<bool> AddEmployeeAsync(Employee employee);
        public Task<bool> UpdateEmployeeAsync(Employee employee);
        public Task<bool> DeleteEmployeeAsync(int id);
    }
}
