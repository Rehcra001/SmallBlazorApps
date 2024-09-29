using Dapper;
using Microsoft.Data.SqlClient;
using ModeslLibrary;
using TodoApp.API.Repository.Contracts;
using System.Data;

namespace TodoApp.API.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IRelationalDataAccess _sqlConnection;

        public PersonRepository(IRelationalDataAccess sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public async Task<IEnumerable<PersonModel>> GetPeople()
        {
            IEnumerable<PersonModel> people = new List<PersonModel>();

            using (SqlConnection connection = _sqlConnection.SQLConnection())
            {
                people = await connection.QueryAsync<PersonModel>("dbo.usp_GetPeople", commandType: CommandType.StoredProcedure);
            }

            return people;
        }

        public async Task<PersonModel> InsertPerson(PersonModel person)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Name", person.Name, DbType.String);

            using (SqlConnection connection = _sqlConnection.SQLConnection())
            {
                try
                {
                    int? result = await connection.QuerySingleOrDefaultAsync<int>("dbo.usp_InsertPerson", parameters, commandType: CommandType.StoredProcedure);

                    if (result is null || result == 0)
                    {
                        return new PersonModel();
                    }
                    else
                    {
                        person.Id = (int)result;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return person;
        }
    }
}
