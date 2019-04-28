using ClinkedIn2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn2.Data
{
    public class UserServiceRepository
    {
        const string ConnectionString = "Server = localhost; Database = ClinkedIn; Trusted_Connection = True;";

        public UserService AddUserService(int userId, int serviceId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var insertUserServiceCommand = connection.CreateCommand();
                insertUserServiceCommand.CommandText = $@"Insert into userservices (userId, serviceId)
                                              Output inserted.*
                                              Values(@userId, @serviceId)";

                insertUserServiceCommand.Parameters.AddWithValue("userId", userId);
                insertUserServiceCommand.Parameters.AddWithValue("serviceId", serviceId);

                var reader = insertUserServiceCommand.ExecuteReader();

                if (reader.Read())
                {
                    var insertedUserId = (int)reader["userId"];
                    var insertedServiceId = (int)reader["serviceId"];

                    var insertedId = (int)reader["Id"];

                    var newUserService = new UserService(insertedUserId, insertedServiceId) { Id = insertedId };

                    return newUserService;
                }
            }

            throw new Exception("No userservice found");
        }

        public List<UserService> GetAll()
        {
            var userServices = new List<UserService>();

            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var getAllUserServicesCommand = connection.CreateCommand();
            getAllUserServicesCommand.CommandText = @"select userId, serviceId, id
                                               from userservices";

            var reader = getAllUserServicesCommand.ExecuteReader();

            while (reader.Read())
            {
                var id = (int)reader["Id"];
                var userId = (int)reader["userId"];
                var serviceId = (int)reader["serviceId"];

                var userservice = new UserService(userId, serviceId) { Id = id };

                userServices.Add(userservice);
            }

            connection.Close();

            return userServices;
        }

        public void DeleteUserService(int userServiceId)
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var deleteUserServiceCommand = connection.CreateCommand();
            deleteUserServiceCommand.Parameters.AddWithValue("Id", userServiceId);
            deleteUserServiceCommand.CommandText = @"Delete
                                                From UserServices
                                                Where Id = @Id";

            deleteUserServiceCommand.ExecuteNonQuery();

            connection.Close();
        }

        public bool UpdateUserService(int id, int userId, int serviceId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var updateUserServiceCommand = connection.CreateCommand();

                updateUserServiceCommand.Parameters.AddWithValue("@Id", id);

                updateUserServiceCommand.CommandText = $@"Update userservices
                                                            Set userId = @userId,
                                                                serviceId = @serviceId
                                                            Where Id = @Id";

                updateUserServiceCommand.Parameters.AddWithValue("userId", userId);
                updateUserServiceCommand.Parameters.AddWithValue("serviceId", serviceId);


                var numberOfRowsUpdated = updateUserServiceCommand.ExecuteNonQuery();

                connection.Close();

                if (numberOfRowsUpdated > 0)
                { return true; }
                return false;
            }
        }
    }
}
