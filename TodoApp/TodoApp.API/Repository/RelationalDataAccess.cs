using Microsoft.Data.SqlClient;
using TodoApp.API.Repository.Contracts;

namespace TodoApp.API.Repository
{
    public class RelationalDataAccess : IRelationalDataAccess
    {
        private readonly IConfiguration _config;

        public RelationalDataAccess(IConfiguration config)
        {
            _config = config;
        }
        public SqlConnection SQLConnection()
        {
            SqlConnection connection = new SqlConnection(_config.GetConnectionString("TodoAppDB"));

            return connection;
        }
    }
}
