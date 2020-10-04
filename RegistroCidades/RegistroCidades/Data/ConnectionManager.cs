using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RegistroCidades.Data
{
    public class ConnectionManager
    {
        private string _connectionString= "Data Source=localhost;Initial Catalog=desafiolinx;Integrated Security=True;Pooling=False";

        public ConnectionManager() { }
        public ConnectionManager(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void ExecuteNonQuery(string strSqlStatement)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(strSqlStatement, connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public SqlConnection GetConnection()
        {
            try 
            {
                return new SqlConnection(_connectionString);
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
