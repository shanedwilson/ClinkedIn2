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

            foreach (User user in users)
            {
                user.Services = (GetServices(user.Id));
            }

            foreach (User user in users)
            {
                user.Interests = (GetInterests(user.Id));
            }

            return users;
        }

        //public List<object> GetUserWithInterests()
        //{
        //    var usersWithInterests = new List<User>();

        //    var connection = new SqlConnection(ConnectionString);
        //    connection.Open();

        //    var getUserWithInterestsCommand = connection.CreateCommand();
        //    getUserWithInterestsCommand.CommandText = @"Select u.*, I.Name InterestName
        //                                                From Users u
        //                                                Join UserInterests ui
        //                                                On u.Id = ui.UserId
        //                                                Join Interests i
        //                                                On i.Id = ui.InterestId               
        //                                                Order by u.Id";

        //    var reader = getUserWithInterestsCommand.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        var id = (int)reader["Id"];
        //        var name = reader["Name"].ToString();
        //        var releaseDate = (DateTime)reader["ReleaseDate"];
        //        var age = (int)reader["Age"];
        //        var isPrisoner = (bool)reader["IsPrisoner"];
        //        var interest = reader["InterestName"].ToString();

        //        var user = new User(name, releaseDate, age, isPrisoner, interest) { Id = id };

        //        usersWithInterests.Add(user);
        //    }

        //    connection.Close();

        //    var UsersInterests = new List<object>();
        //    var Interests = new List<string>();

        //    var userInterests = usersWithInterests.GroupBy(
        //                       u => new { u.Id, u.Name, u.ReleaseDate, u.Age, u.IsPrisoner },
        //                       u => u.Interest,
        //                       (user, interest) => new { User = user, Interests = interest.ToList() }
        //        ).ToList();

        //    UsersInterests.Add(userInterests);

        //    return UsersInterests;
        //}

        //public List<object> GetUserWithServices()
        //{
        //    var usersWithServices = new List<User>();

        //    var connection = new SqlConnection(ConnectionString);
        //    connection.Open();

        //    var getUserWithServicesCommand = connection.CreateCommand();
        //    getUserWithServicesCommand.CommandText = @"Select u.*, s.Name ServiceName
        //                                                From Users u
        //                                                Join UserServices us
        //                                                On u.Id = us.UserId
        //                                                Join Services s
        //                                                On s.Id = us.ServiceId
        //                                                Order by u.Id";

        //    var reader = getUserWithServicesCommand.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        var id = (int)reader["Id"];
        //        var name = reader["Name"].ToString();
        //        var releaseDate = (DateTime)reader["ReleaseDate"];
        //        var age = (int)reader["Age"];
        //        var isPrisoner = (bool)reader["IsPrisoner"];
        //        var service = reader["ServiceName"].ToString();

        //        var user = new User(name, releaseDate, age, isPrisoner, service) { Id = id };

        //        usersWithServices.Add(user);
        //    }

        //    connection.Close();

        //    var UsersServices = new List<object>();
        //    var Services = new List<string>();

        //    var userServices = usersWithServices.GroupBy(
        //                       u => new { u.Id, u.Name, u.ReleaseDate, u.Age, u.IsPrisoner },
        //                       u => u.Interest,
        //                       (user, service) => new { User = user, Services = service.ToList() }
        //        ).ToList();

        //    UsersServices.Add(userServices);

        //    return UsersServices;
        //}

        List<string> GetServices(int userId)
        {
            var userServices = new List<string>();

            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var getUserWithServicesCommand = connection.CreateCommand();
            getUserWithServicesCommand.Parameters.AddWithValue("@UId", userId);
            getUserWithServicesCommand.CommandText = @"Select s.Name ServiceName
                                                        From Services s
														join UserServices us
														On us.ServiceId = s.Id
                                                        Where us.UserId = @UId
                                                        Order by ServiceName";

            var reader = getUserWithServicesCommand.ExecuteReader();

            while (reader.Read())
            {
                var service = reader["ServiceName"].ToString();

                userServices.Add(service);
            }

            connection.Close();

            return userServices;
        }

        List<string> GetInterests(int userId)
        {
            var userInterests = new List<string>();

            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var getUserWithInterestsCommand = connection.CreateCommand();
            getUserWithInterestsCommand.Parameters.AddWithValue("@UId", userId);
            getUserWithInterestsCommand.CommandText = @"Select i.Name InterestName
                                                        From Interests i
														join UserInterests ui
														On ui.InterestId = i.Id
                                                        Where ui.UserId = @UId
                                                        Order by InterestName";

            var reader = getUserWithInterestsCommand.ExecuteReader();

            while (reader.Read())
            {
                var interest = reader["InterestName"].ToString();

                userInterests.Add(interest);
            }

            connection.Close();

            return userInterests;
        }
    }
}
