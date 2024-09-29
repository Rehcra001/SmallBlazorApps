using ModeslLibrary;
using System.Net;
using System.Net.Http.Json;
using TodoApp.Web.Services.Contracts;

namespace TodoApp.Web.Services
{
    public class TasksService : ITasksService
    {
        private readonly HttpClient _httpClient;

        public TasksService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<IEnumerable<TaskModel>> GetTasks()
        {
            try
            {
                HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("api/task");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    if (httpResponseMessage.StatusCode == HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<TaskModel>();
                    }

                    IEnumerable<TaskModel>? tasks = await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<TaskModel>>();

                    return tasks!;
                }
                else
                {
                    var message = await httpResponseMessage.Content.ReadAsStringAsync();
                    throw new Exception($"Http status: {httpResponseMessage.StatusCode} Message -{message}");
                }
            }
            catch (Exception)
            {
                // Log error
                throw;
            }
        }

        public async Task<TaskModel> InsertTask(TaskModel task)
        {
            try
            {
                HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync<TaskModel>("api/task", task);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    if (httpResponseMessage.StatusCode == HttpStatusCode.NoContent)
                    {
                        var message = await httpResponseMessage.Content.ReadAsStringAsync();
                        throw new Exception($"Http status: {httpResponseMessage.StatusCode} Message -{message}");
                    }

                    return task;
                }
                else
                {
                    var message = await httpResponseMessage.Content.ReadAsStringAsync();
                    throw new Exception($"Http status: {httpResponseMessage.StatusCode} Message -{message}");
                }
            }
            catch (Exception)
            {

                throw;
            }
            

            
        }

        public async Task<TaskModel> UpdateTask(TaskModel task)
        {
            try
            {
                HttpResponseMessage httpResponseMessage = await _httpClient.PutAsJsonAsync<TaskModel>("api/task", task);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    if (httpResponseMessage.StatusCode == HttpStatusCode.NoContent)
                    {
                        var message = await httpResponseMessage.Content.ReadAsStringAsync();
                        throw new Exception($"Http status: {httpResponseMessage.StatusCode} Message -{message}");
                    }

                    task = await httpResponseMessage.Content.ReadFromJsonAsync<TaskModel>();

                    return task;
                }
                else
                {
                    var message = await httpResponseMessage.Content.ReadAsStringAsync();
                    throw new Exception($"Http status: {httpResponseMessage.StatusCode} Message -{message}");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
