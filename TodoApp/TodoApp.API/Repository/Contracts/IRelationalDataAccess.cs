using Microsoft.Data.SqlClient;

namespace TodoApp.API.Repository.Contracts
{
    public interface IRelationalDataAccess
    {
        SqlConnection SQLConnection();
    }
}
