using BackendApi.Models;
using BackendApi.Repositories;
using System.Diagnostics;

namespace BackendApi.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _Repository;
        public DepartmentService(IDepartmentRepository Repository)
        {
            _Repository = Repository;   
        }

        public async Task<bool> AddDepartmentAsync(Department department)
        {
            try
            {
                return await _Repository.AddDepartmentAsync(department);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("AddDepartmentAsync Error In Servies : ", ex.ToString());
                throw;
            }
        }

        public async Task<bool> DeleteDepartmentAsync(int departmentId)
        {
            try
            {
                return await _Repository.DeleteDepartmentAsync(departmentId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DeleteDepartmentAsync Error In Servies : ", ex.ToString());
                throw;
            }
        }

        public async Task<List<Department>> GetAllDepartmentAsync()
        {
            try
            {
                return await _Repository.GetAllDepartmentAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetAllDepartmentAsync Error In Servies : ", ex.ToString());
                throw;
            }
        }

        public async Task<Department> GetDeparmentByIdAsync(int departmentId)
        {
            try
            {
                return await _Repository.GetDeparmentByIdAsync(departmentId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetDeparmentByIdAsync Error In Servies : ", ex.ToString());
                throw;
            }
        }

        public async Task<bool> UpdateDepartmentAsync(Department department)
        {
            try
            {
                return await _Repository.UpdateDepartmentAsync(department);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UpdateDepartmentAsync Error In Servies : ", ex.ToString());
                throw;
            }
        }
    }
}
