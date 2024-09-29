using ModeslLibrary;
using TodoApp.Web.Services.Contracts;
using System.Net;
using System.Net.Http.Json;

namespace TodoApp.Web.Services
{
    public class PersonService : IPersonService
    {
        private readonly HttpClient _httpClient;

        public PersonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PersonModel>> GetPeople()
        {
            try
            {
                HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("api/person");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    if (httpResponseMessage.StatusCode == HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<PersonModel>();
                    }
                    else
                    {
                        IEnumerable<PersonModel>? people = await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<PersonModel>>();
                        if (people is null || people.Count() == 0)
                        {
                            return Enumerable.Empty<PersonModel>();
                        }

                        return people;
                    }
                }
                else
                {
                    var message = await httpResponseMessage.Content.ReadAsStringAsync();
                    throw new Exception($"Http status: {httpResponseMessage.StatusCode} Message -{message}");
                }
            }
            catch (Exception)
            {
                // log exception here

                throw;
            }
        }

        public async Task<PersonModel?> InsertPerson(PersonModel person)
        {
            try
            {
                HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync<PersonModel>("api/person", person);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    if (httpResponseMessage.StatusCode == HttpStatusCode.NoContent)
                    {
                        return new PersonModel();
                    }
                    else
                    {
                        PersonModel? result = await httpResponseMessage.Content.ReadFromJsonAsync<PersonModel>();
                        return result;
                    }
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
