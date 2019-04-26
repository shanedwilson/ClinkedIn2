using ClinkedIn2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn2.Data
{
    public class UserRepository
    {
        const string ConnectionString = "Server = localhost; Database = ClinkedIn; Trusted_Connection = True;";

        public User AddUser(string name, DateTime releaseDate, int age, bool isPrisoner)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var insertUserCommand = connection.CreateCommand();
                insertUserCommand.CommandText = $@"Insert into users (name, releaseDate, age, isPrisoner)
                                              Output inserted.*
                                              Values(@name, @releaseDate, @age, @isPrisoner)";

                insertUserCommand.Parameters.AddWithValue("name", name);
                insertUserCommand.Parameters.AddWithValue("releasedate", releaseDate);
                insertUserCommand.Parameters.AddWithValue("age", age);
                insertUserCommand.Parameters.AddWithValue("isPrisoner", isPrisoner);


                var reader = insertUserCommand.ExecuteReader();

                if (reader.Read())
                {
                    var insertedName = reader["name"].ToString();
                    var insertedReleaseDate = (DateTime) reader["releaseDate"];
                    var insertedAge = (int) reader["age"];
                    var insertedIsPrisoner = (bool) reader["isPrisoner"];


                    var insertedId = (int)reader["Id"];

                    var newUser = new User(insertedName, insertedReleaseDate, insertedAge, insertedIsPrisoner ) { Id = insertedId };

                    return newUser;
                }
            }

            throw new Exception("No user found");
        }

        public List<User> GetAll()
        {
            var users = new List<User>();

            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var getAllUsersCommand = connection.CreateCommand();
            getAllUsersCommand.CommandText = @"select name ,releaseDate, age, isPrisoner, id
                                               from users";

            var reader = getAllUsersCommand.ExecuteReader();

            while (reader.Read())
            {
                var id = (int)reader["Id"];
                var name = reader["Name"].ToString();
                var releaseDate = (DateTime) reader ["ReleaseDate"];
                var age = (int)reader["Age"];
                var isPrisoner = (bool)reader["IsPrisoner"];

                var user = new User(name, releaseDate, age, isPrisoner) { Id = id };

                users.Add(user);
            }

            connection.Close();

            return users;
        }

        public List<User> GetUserWithInterests()
        {
            var userWithInterests = new List<User>();

            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var getUserWithInterestsCommand = connection.CreateCommand();
            getUserWithInterestsCommand.CommandText = @"Select u.*, I.Name InterestName
                                                        From Users u
                                                        Join UserInterests ui
                                                        On u.Id = ui.UserId
                                                        Join Interests i
                                                        On i.Id = ui.InterestId
                                                        Order by u.Id";

            var reader = getUserWithInterestsCommand.ExecuteReader();

            while (reader.Read())
            {
                var id = (int)reader["Id"];
                var name = reader["Name"].ToString();
                var releaseDate = (DateTime)reader["ReleaseDate"];
                var age = (int)reader["Age"];
                var isPrisoner = (bool)reader["IsPrisoner"];
                var interest = reader["InterestName"].ToString();

                var user = new User(name, releaseDate, age, isPrisoner, interest) { Id = id };

                userWithInterests.Add(user);
            }

            connection.Close();

            return userWithInterests;
        }
    }
}
