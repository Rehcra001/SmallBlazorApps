using Microsoft.AspNetCore.Mvc;
using ModeslLibrary;
using TodoApp.API.Repository.Contracts;

namespace TodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonModel>>> Get()
        {
            try
            {
                IEnumerable<PersonModel> people = await _personRepository.GetPeople();

                if (people is null || people.Count() == 0)
                {
                    return NoContent();
                }

                return Ok(people);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PersonModel>> Post([FromBody] PersonModel personModel)
        {
            try
            {
                personModel = await _personRepository.InsertPerson(personModel);

                if (personModel is null || personModel.Id == 0)
                {
                    return NoContent();
                }

                return Ok(personModel);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System Error");
            }

        }
    }
}
