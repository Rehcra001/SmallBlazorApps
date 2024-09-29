using ModeslLibrary;

namespace TodoApp.Web.Services.Contracts
{
    public interface ITasksService
    {
        Task<IEnumerable<TaskModel>> GetTasks();
        Task<TaskModel> InsertTask(TaskModel task);
        Task<TaskModel> UpdateTask(TaskModel task);
    }
}
