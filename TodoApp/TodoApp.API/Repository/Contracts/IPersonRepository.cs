using ModeslLibrary;

namespace TodoApp.API.Repository.Contracts
{
    public interface IPersonRepository
    {
        Task<PersonModel> InsertPerson(PersonModel person);
        Task<IEnumerable<PersonModel>> GetPeople();
    }
}
