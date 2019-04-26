using ClinkedIn2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn2.Data
{
    public class UserInterestRepository
    {
        const string ConnectionString = "Server = localhost; Database = ClinkedIn; Trusted_Connection = True;";

        public UserInterest AddUserInterest(int userId, int interestId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var insertUserInterestCommand = connection.CreateCommand();
                insertUserInterestCommand.CommandText = $@"Insert into userinterests (userId, interestId)
                                              Output inserted.*
                                              Values(@userId, @interestId)";

                insertUserInterestCommand.Parameters.AddWithValue("userId", userId);
                insertUserInterestCommand.Parameters.AddWithValue("interestId", interestId);

                var reader = insertUserInterestCommand.ExecuteReader();

                if (reader.Read())
                {
                    var insertedUserId = (int)reader["userId"];
                    var insertedInterestId = (int)reader["interestId"];

                    var insertedId = (int)reader["Id"];

                    var newUserInterest = new UserInterest(insertedUserId, insertedInterestId) { Id = insertedId };

                    return newUserInterest;
                }
            }

            throw new Exception("No userinterest found");
        }

        public List<UserInterest> GetAll()
        {
            var userInterests = new List<UserInterest>();

            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var getAllUserInterestsCommand = connection.CreateCommand();
            getAllUserInterestsCommand.CommandText = @"select userId, interestId, id
                                               from userinterests";

            var reader = getAllUserInterestsCommand.ExecuteReader();

            while (reader.Read())
            {
                var id = (int)reader["Id"];
                var userId = (int)reader["userId"];
                var interestId = (int)reader["interestId"];

                var userinterest = new UserInterest(userId, interestId) { Id = id };

                userInterests.Add(userinterest);
            }

            connection.Close();

            return userInterests;
        }
    }
}
