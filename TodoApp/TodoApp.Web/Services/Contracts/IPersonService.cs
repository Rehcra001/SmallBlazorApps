using ModeslLibrary;

namespace TodoApp.Web.Services.Contracts
{
    public interface IPersonService
    {
        Task<PersonModel?> InsertPerson(PersonModel person);
        Task<IEnumerable<PersonModel>> GetPeople();
    }
}
