using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModeslLibrary;
using TodoApp.API.Repository.Contracts;

namespace TodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskModel>>> Get()
        {
            try
            {
                IEnumerable<TaskModel> tasks = await _taskRepository.GetTasks();

                if (tasks is null || tasks.Count() == 0)
                {
                    return NoContent();
                }

                return Ok(tasks);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TaskModel>> Post([FromBody] TaskModel task)
        {
            try
            {
                task = await _taskRepository.InsertTask(task);

                if (task is null || task.Id == 0)
                {
                    return NoContent();
                }

                return Ok(task);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System Error");
            }
        }

        [HttpPut]
        public async Task<ActionResult<TaskModel>> Put([FromBody] TaskModel task)
        {
            try
            {
                task = await _taskRepository.UpdateTask(task);

                if (task is null || task.Id == 0)
                {
                    return NoContent();
                }

                return Ok(task);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System Error");
            }
        }
    }
}
