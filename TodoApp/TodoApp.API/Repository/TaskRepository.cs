using Dapper;
using Microsoft.Data.SqlClient;
using ModeslLibrary;
using System.Data;
using TodoApp.API.Repository.Contracts;

namespace TodoApp.API.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private IRelationalDataAccess _sqlConnection;

        public TaskRepository(IRelationalDataAccess sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public async Task<IEnumerable<TaskModel>> GetTasks()
        {
            IEnumerable<TaskModel> tasks = new List<TaskModel>();

            using (SqlConnection connection = _sqlConnection.SQLConnection())
            {
                tasks = await connection.QueryAsync<TaskModel>("dbo.usp_GetTasks", commandType: CommandType.StoredProcedure);
            }

            return tasks;
        }

        public async Task<TaskModel> InsertTask(TaskModel task)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@PersonId", task.PersonId, DbType.Int32);
            parameters.Add("@Task", task.Task, DbType.String);
            parameters.Add("@PercentageComplete", task.PercentageComplete, DbType.Int32);
            parameters.Add("@DateToBeCompleted", task.DateToBeCompleted, DbType.DateTime2);

            using (SqlConnection connection = _sqlConnection.SQLConnection())
            {
                var result = await connection.QuerySingleAsync<int>("dbo.usp_InsertTask", parameters, commandType: CommandType.StoredProcedure);

                if (result == 0)
                {
                    task = new TaskModel();
                }
                else
                {
                    task.Id = result;
                }
            }

            return task;
        }

        public async Task<TaskModel> UpdateTask(TaskModel task)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", task.Id, DbType.Int32);
            parameters.Add("@PersonId", task.PersonId, DbType.Int32);
            parameters.Add("@Task", task.Task, DbType.String);
            parameters.Add("@PercentageComplete", task.PercentageComplete, DbType.Int32);
            parameters.Add("@DateToBeCompleted", task.DateToBeCompleted, DbType.DateTime2);

            using (SqlConnection connection = _sqlConnection.SQLConnection())
            {
                try
                {
                    var result = await connection.QuerySingleAsync<TaskModel>("dbo.usp_UpdateTask", parameters, commandType: CommandType.StoredProcedure);

                    task = result;
                }
                catch (Exception e)
                {
                    task = new TaskModel();
                }
                
            }

            return task;
        }
    }
}
