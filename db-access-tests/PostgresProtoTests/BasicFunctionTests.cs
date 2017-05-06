using System.Linq;
using Dapper;
using FluentAssertions;
using Npgsql;
using Xunit;

namespace PostgresProtoTests
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class BasicFunctionTests
    {
        [Fact]
        public void Can_Insert_And_Retrieve_Row_From_Person_Table()
        {
            string connectionString = "Host=localhost;Username=postgres;Password=mypassword;Database=mydb";

            int result;
            string firstName = "fn";
            string lastName = "ln";

            using (var dbConnection = new NpgsqlConnection(connectionString))
            {
                dbConnection.Open();

                var sql = string.Format(
                    $"insert into person (firstname, lastname) values ('{firstName}', '{lastName}')");
                result = dbConnection.Execute(sql);
                result.Should().BeGreaterThan(0);

                dbConnection.Close();
            }

            var personJustAdded = new Person()
            {
                Id = result,
                FirstName = firstName,
                LastName = lastName
            };

            using (var dbConnection = new NpgsqlConnection(connectionString))
            {
                dbConnection.Open();

                Person person = dbConnection.Query<Person>("select * from person").FirstOrDefault();
                person.ShouldBeEquivalentTo(personJustAdded);

                dbConnection.Close();
            }
        }
    }
}
