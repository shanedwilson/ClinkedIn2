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
    }
}
