using ClinkedIn2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn2.Data
{
    public class InterestRepository
    {
        const string ConnectionString = "Server = localhost; Database = ClinkedIn; Trusted_Connection = True;";

        public Interest AddInterest(string name)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var insertInterestCommand = connection.CreateCommand();
                insertInterestCommand.CommandText = $@"Insert into interests (name)
                                              Output inserted.*
                                              Values(@name)";

                insertInterestCommand.Parameters.AddWithValue("name", name);



                var reader = insertInterestCommand.ExecuteReader();

                if (reader.Read())
                {
                    var insertedName = reader["name"].ToString();

                    var insertedId = (int)reader["Id"];

                    var newInterest = new Interest(insertedName) { Id = insertedId };

                    return newInterest;
                }
            }

            throw new Exception("No interest found");
        }

        public List<Interest> GetAll()
        {
            var interests = new List<Interest>();

            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var getAllInterestsCommand = connection.CreateCommand();
            getAllInterestsCommand.CommandText = @"select name , id
                                               from interests";

            var reader = getAllInterestsCommand.ExecuteReader();

            while (reader.Read())
            {
                var id = (int)reader["Id"];
                var name = reader["Name"].ToString();

                var interest = new Interest(name) { Id = id };

                interests.Add(interest);
            }

            connection.Close();

            return interests;
        }

        public void DeleteInterest(int interestId)
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var deleteInterestCommand = connection.CreateCommand();
            deleteInterestCommand.Parameters.AddWithValue("Id", interestId);
            deleteInterestCommand.CommandText = @"Delete
                                                From Interests
                                                Where Id = @Id";

            deleteInterestCommand.ExecuteNonQuery();

            connection.Close();
        }

        public bool UpdateInterest(int id, string name)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var updateInterestCommand = connection.CreateCommand();

                updateInterestCommand.Parameters.AddWithValue("@Id", id);

                updateInterestCommand.CommandText = $@"Update interests
                                                    Set name = @name
                                                    Where Id = @Id";

                updateInterestCommand.Parameters.AddWithValue("name", name);

                var numberOfRowsUpdated = updateInterestCommand.ExecuteNonQuery();

                connection.Close();

                if (numberOfRowsUpdated > 0)
                { return true; }
                return false;
            }
        }
    }
}
