using BackendApi.Database;
using BackendApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace BackendApi.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DatabaseHelper _Db;
        public EmployeeRepository(DatabaseHelper Db)
        {
            _Db = Db;
        }
        public async Task<bool> AddEmployeeAsync(Employee employee)
        {
            string Query = @"INSERT INTO Employee (EmployeeName, Email, Salary, DepartmentId) VALUES(@EmployeeName, @Email, @Salary, @DepartmentId)";
            SqlParameter[] param =
            {
                new SqlParameter("@EmployeeName",employee.EmployeeName),
                new SqlParameter("@Email",employee.Email),
                new SqlParameter("@Salary",employee.Salary),
                new SqlParameter("@DepartmentId",employee.DepartmentId),
            };
            try
            {
                int rows = await _Db.ExecexuteNonQyeryAsync(Query,param);
                return rows > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("AddEmployeeAsync Error: " + ex.ToString());
                throw;
            }

        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            string Query = "DELETE FROM Employee WHERE EmployeeId=@EmployeeId";
            SqlParameter param = new SqlParameter("@EmployeeId",id);
            try
            {
                int row = await _Db.ExecexuteNonQyeryAsync(Query,param);
                return row > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeleteEmployeeAsync Error: " + ex.ToString());
                throw;
            }
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            List<Employee> employees = new List<Employee>();
            string Query = @"SELECT 
                                   e.EmployeeId,
                                e.EmployeeName,
                                e.Email,
                                e.Salary,
                                e.DepartmentId,
                                e.CreatedDate,
                                d.DepartmentName  FROM Employee e INNER JOIN Department d ON e.DepartmentId = d.DepartmentId";
            try
            {
                DataTable table = await _Db.ExecexuteReaderAsync(Query);
                foreach (DataRow row in table.Rows)
                {
                    Employee emp = new Employee();
                    emp.EmployeeId = Convert.ToInt32(row["EmployeeId"]);
                    emp.EmployeeName = row["EmployeeName"].ToString();
                    emp.Email = row["Email"].ToString();
                    emp.Salary = Convert.ToDecimal(row["Salary"]);
                    emp.DepartmentId = Convert.ToInt32(row["DepartmentId"]);
                    emp.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                    emp.DepartmentName = row["DepartmentName"].ToString();

                    employees.Add(emp);
                }
                return employees;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllEmployeesAsync Error: " + ex.ToString());
                throw;
            }

        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            Employee employee = null;
            string Query = @"SELECT 
                                   e.EmployeeId,
                                e.EmployeeName,
                                e.Email,
                                e.Salary,
                                e.DepartmentId,
                                e.CreatedDate,
                                d.DepartmentName  FROM Employee e INNER JOIN Department d ON e.DepartmentId = d.DepartmentId WHERE e.EmployeeId = @EmployeeId";

            SqlParameter param = new SqlParameter("@EmployeeId", employeeId);
            try
            {
                DataTable table = await _Db.ExecexuteReaderAsync(Query,param);
                if (table.Rows.Count>0)
                {
                    DataRow row = table.Rows[0];
                
                    employee = new Employee();
                    employee.EmployeeId = Convert.ToInt32(row["EmployeeId"]);
                    employee.EmployeeName = row["EmployeeName"].ToString();
                    employee.Email = row["Email"].ToString();
                    employee.Salary = Convert.ToDecimal(row["Salary"]);
                    employee.DepartmentId = Convert.ToInt32(row["DepartmentId"]);
                    employee.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                    employee.DepartmentName = row["DepartmentName"].ToString();
                }
                return employee;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetEmployeeByIdAsync Error: " + ex.ToString());
                throw;
            }
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            string Query = @"UPDATE Employee SET EmployeeName=@EmployeeName, Email=@Email, Salary=@Salary, DepartmentId=@DepartmentId WHERE EmployeeId=@EmployeeId";
            SqlParameter[] param =
            {
                new SqlParameter("@EmployeeId", employee.EmployeeId),
                new SqlParameter("@EmployeeName",employee.EmployeeName),
                new SqlParameter("@Email",employee.Email),
                new SqlParameter("@Salary",employee.Salary),
                new SqlParameter("@DepartmentId",employee.DepartmentId),
            };
            try
            {
                int row = await _Db.ExecexuteNonQyeryAsync(Query,param);
                return row > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpdateEmployeeAsync Error: " + ex.ToString());
                throw;
            }
        }
    }
}
