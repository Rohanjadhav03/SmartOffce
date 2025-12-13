using BackendApi.Database;
using BackendApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace BackendApi.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DatabaseHelper _Db;
        public AuthRepository(DatabaseHelper Db)
        {
            _Db = Db;
        }
        public async Task<User> ValidateUserAsync(string username, string password)
        {
            string Query = @"SELECT UserId , Username, Password, Role, IsActive,CreatedDate FROM Users WHERE Username=@Username AND Password=@Password AND IsActive=1 ";
            SqlParameter[] param = {
                new SqlParameter("@Username", username),
                new SqlParameter("@Password", password)
                };


            try
            {
                DataTable table = await _Db.ExecexuteReaderAsync(Query,param);
                if (table.Rows.Count==0)
                {
                    return null;
                }
                DataRow row = table.Rows[0];
                User user = new User();
                user.UserId = Convert.ToInt32(row["UserId"]);
                user.Username = row["Username"].ToString();
                user.Password = row["Password"].ToString();
                user.Role = row["Role"].ToString();
                user.IsActive = Convert.ToBoolean(row["IsActive"]);
                user.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);

                return user;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in AuthRepository ValidateUserAsync: " + ex.ToString());
                throw;
            }
        }
    }
}
