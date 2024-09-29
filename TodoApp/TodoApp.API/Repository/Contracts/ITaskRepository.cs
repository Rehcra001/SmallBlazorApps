using ModeslLibrary;

namespace TodoApp.API.Repository.Contracts
{
    public interface ITaskRepository
    {
        Task<TaskModel> InsertTask(TaskModel task);
        Task<IEnumerable<TaskModel>> GetTasks();
        Task<TaskModel> UpdateTask(TaskModel task);
    }
}
