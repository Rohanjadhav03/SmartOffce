using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace BackendApi.Database
{
    public class DatabaseHelper
    {
        private readonly IConfiguration _config;
        private readonly string _Connection;

        public DatabaseHelper(IConfiguration Config)
        {
            _config = Config;
            _Connection = _config.GetConnectionString("DefaultConnection");
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_Connection);
        }

        public async Task<DataTable> ExecexuteReaderAsync(string Query, params SqlParameter[] parameters)
        {
            DataTable table = new DataTable();
            SqlConnection Conn = GetConnection();
            SqlCommand Cmd = new SqlCommand(Query, Conn);
            try
            {
               
                if (parameters != null)
                {
                    Cmd.Parameters.AddRange(parameters);
                }
                await Conn.OpenAsync();

                SqlDataAdapter adapter = new SqlDataAdapter(Cmd);
                adapter.Fill(table);
                return table;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("ExecexuteReaderAsync Error : " + ex);
                throw;
            }
            finally
            {
                if (Conn.State==ConnectionState.Open)
                {
                    Conn.Close();
                }
                Conn.Dispose();
                Cmd.Dispose();

            }
            

        }
        public async Task<int> ExecexuteNonQyeryAsync(string Query, params SqlParameter[] parameters)
        {
            SqlConnection Conn = GetConnection();
            SqlCommand Cmd = new SqlCommand(Query, Conn);
            try
            {

                if (parameters != null)
                {
                    Cmd.Parameters.AddRange(parameters);
                }
                await Conn.OpenAsync();

                int rows = await Cmd.ExecuteNonQueryAsync();
                return rows;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("ExecexuteNonQyeryAsync Error : " + ex);
                throw;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
                Conn.Dispose();
                Cmd.Dispose();

            }


        }


        public async Task<object> ExecexuteScalerAsync(string Query, params SqlParameter[] parameters)
        {
            SqlConnection Conn = GetConnection();
            SqlCommand Cmd = new SqlCommand(Query, Conn);
            try
            {

                if (parameters != null)
                {
                    Cmd.Parameters.AddRange(parameters);
                }
                await Conn.OpenAsync();

                object value = await Cmd.ExecuteScalarAsync();
                return value;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("ExecexuteScalerAsync Error : " + ex);
                throw;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
                Conn.Dispose();
                Cmd.Dispose();

            }


        }

    }
}
