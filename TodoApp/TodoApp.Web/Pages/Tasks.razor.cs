using Microsoft.AspNetCore.Components;
using ModeslLibrary;
using TodoApp.Web.Services.Contracts;

namespace TodoApp.Web.Pages
{
    public partial class Tasks
    {
        private List<PersonModel> _people = new List<PersonModel>();
        private List<TaskModel> _tasks = new List<TaskModel>();


        [Inject]
        public required IPersonService PersonService { get; set; }
        [Inject]
        public required ITasksService TasksService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _people = (List<PersonModel>)await PersonService.GetPeople();
            _tasks = (List<TaskModel>)await TasksService.GetTasks();
        }

        private async Task AddTask(TaskModel task)
        {
            await TasksService.InsertTask(task);
        }

        private List<TaskModel> GetPersonTasks(PersonModel person)
        {
            return _tasks.Where(x => x.PersonId == person.Id).ToList();
        }


        private async Task UpdateTask(TaskModel task)
        {
            int id = _tasks.FindIndex(x => x.Id == task.Id);
            task = await TasksService.UpdateTask(task);
            _tasks[id] = task;
        }
    }
}
