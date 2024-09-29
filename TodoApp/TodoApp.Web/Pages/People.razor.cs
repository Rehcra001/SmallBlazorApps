using Microsoft.AspNetCore.Components;
using ModeslLibrary;
using TodoApp.Web.Services.Contracts;

namespace TodoApp.Web.Pages
{
    public partial class People
    {
        [Inject]
        public required IPersonService PersonService { get; set; }

        private PersonModel _newPerson = new PersonModel();

        private List<PersonModel> _people = new List<PersonModel>();

        protected override async Task OnInitializedAsync()
        {
            _people = (List<PersonModel>)await PersonService.GetPeople();
        }

        private async void InsertPerson()
        {
            if (_newPerson.Name != "")
            {
                _newPerson = await PersonService.InsertPerson(_newPerson);

                if (_newPerson is not null && _newPerson.Id > 0)
                {
                    _people.Add(_newPerson);
                    StateHasChanged();
                }

                _newPerson = new PersonModel();
            }
            
        }
    }
}
