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
        public void CanAddToATable()
        {
            string connectionString = "Host=localhost;Username=postgres;Password=mypassword;Database=mydb";

            using (var dbConnection = new NpgsqlConnection(connectionString))
            {
                dbConnection.Open();

                string sql = string.Format("insert into person (firstname, lastname) values ('fn', 'ln')");
                int result = dbConnection.Execute(sql);
                result.Should().BeGreaterThan(0);

                dbConnection.Close();
            }
        }
        
        [Fact]
        public void CanRetrieveFromATable()
        {
            string connectionString = "Host=localhost;Username=postgres;Password=mypassword;Database=mydb";
            using (var dbConnection = new NpgsqlConnection(connectionString))
            {
                dbConnection.Open();

                Person person = dbConnection.Query<Person>("select * from person").FirstOrDefault();
                person.ShouldBeEquivalentTo(new Person() {Id = 1, FirstName = "fn", LastName = "ln"});

                dbConnection.Close();
            }
        }
    }
}
