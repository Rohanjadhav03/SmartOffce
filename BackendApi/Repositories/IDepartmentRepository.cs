using BackendApi.Models;

namespace BackendApi.Repositories
{
    public interface IDepartmentRepository
    {
        public Task<List<Department>> GetAllDepartmentAsync();
        public Task<Department> GetDeparmentByIdAsync(int departmentId);
        public Task<bool> AddDepartmentAsync(Department department);
        public Task<bool> UpdateDepartmentAsync(Department department);

        public Task<bool> DeleteDepartmentAsync(int departmentId);

    }
}
