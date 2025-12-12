using BackendApi.Database;
using BackendApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Transactions;

namespace BackendApi.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DatabaseHelper _Db;
        public DepartmentRepository(DatabaseHelper Db)
        {
            _Db = Db;
        }
        public async Task<bool> AddDepartmentAsync(Department department)
        {
            string Query = @"INSERT INTO Department(DepartmentName,IsActive) VALUES(@DepartmentName,@IsActive)";
            SqlParameter[] param =
            {
                new SqlParameter("@DepartmentName",department.DepartmentName),
                new SqlParameter("@IsActive",department.IsActive)
            };

            try
            {
                int row = await _Db.ExecexuteNonQyeryAsync(Query, param);
                return row > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("AddDepartmentAsync Error: " + ex.ToString());
                throw;                
            }
            
        }

        public async Task<bool> DeleteDepartmentAsync(int departmentId)
        {
            string Query = @"DELETE FROM Department WHERE DepartmentId = @DepartmentId";
            SqlParameter param = new SqlParameter("@DepartmentId", departmentId);
            try
            {
                int row = await _Db.ExecexuteNonQyeryAsync(Query, param);
                return row > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DeleteDepartmentAsync Error: " + ex.ToString());
                throw;
            }
        }

        public async Task<List<Department>> GetAllDepartmentAsync()
        {
            List<Department> departments = new List<Department>();
            string Query = "SELECT DepartmentId, DepartmentName, IsActive, CreatedDAte FROM Department";            
            try
            {
                DataTable table = await _Db.ExecexuteReaderAsync(Query);
                foreach (DataRow row in table.Rows)
                {
                    Department dept = new Department();
                    dept.DepartmentId = Convert.ToInt32(row["DepartmentId"]);
                    dept.DepartmentName = row["DepartmentName"].ToString();
                    dept.IsActive = Convert.ToBoolean(row["IsActive"]);
                    dept.CreatedDate = Convert.ToDateTime(row["CreatedDAte"]);

                    departments.Add(dept);
                }
                return departments;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetAllDepartmentsAsync Error: " + ex.ToString());
                throw;
            }
        }

        public async Task<Department> GetDeparmentByIdAsync(int departmentId)
        {
            Department dept = null;
            string Query = "SELECT DepartmentId, DepartmentName, IsActive, CreatedDAte FROM Department WHERE DepartmentId=@departmentId";
            SqlParameter param = new SqlParameter("@departmentId",departmentId);
            try
            {
                DataTable table = await _Db.ExecexuteReaderAsync(Query,param);
                if (table.Rows.Count>0)
                {
                    DataRow row = table.Rows[0];

                    dept = new Department();
                    dept.DepartmentId = Convert.ToInt32(row["DepartmentId"]);
                    dept.DepartmentName = row["DepartmentName"].ToString();
                    dept.IsActive = Convert.ToBoolean(row["IsActive"]);
                    dept.CreatedDate = Convert.ToDateTime(row["CreatedDAte"]);
                }
                return dept;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetDeparmentByIdAsync Error: " + ex.ToString());
                throw;
            }
        }

        public async Task<bool> UpdateDepartmentAsync(Department department)
        {
            string Query = @"UPDATE Department SET DepartmentName = @DepartmentName, IsActive = @IsActive WHERE DepartmentId = @DepartmentId";
            SqlParameter[] param =
            {

                new SqlParameter("@DepartmentName",department.DepartmentName),
                new SqlParameter("@IsActive",department.IsActive)
            };
            try
            {
                int row = await _Db.ExecexuteNonQyeryAsync(Query,param);
                return row > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UpdateDepartmentAsync Error: " + ex.ToString());
                throw;
            }
        }
    }
}
