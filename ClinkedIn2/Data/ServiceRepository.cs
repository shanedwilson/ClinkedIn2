using ClinkedIn2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn2.Data
{
    public class ServiceRepository
    {
        const string ConnectionString = "Server = localhost; Database = ClinkedIn; Trusted_Connection = True;";

        public Service AddService(string name, string description, decimal price)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var insertServiceCommand = connection.CreateCommand();
                insertServiceCommand.CommandText = $@"Insert into services (name, description, price)
                                              Output inserted.*
                                              Values(@name, @description, @price)";

                insertServiceCommand.Parameters.AddWithValue("name", name);
                insertServiceCommand.Parameters.AddWithValue("description", description);
                insertServiceCommand.Parameters.AddWithValue("price", price);

                var reader = insertServiceCommand.ExecuteReader();

                if (reader.Read())
                {
                    var insertedName = reader["name"].ToString();
                    var insertedDescription = reader["description"].ToString();
                    var insertedPrice = (decimal)reader["price"];


                    var insertedId = (int)reader["Id"];

                    var newService = new Service(insertedName, insertedDescription, insertedPrice) { Id = insertedId };

                    return newService;
                }
            }

            throw new Exception("No service found");
        }

        public List<Service> GetAll()
        {
            var services = new List<Service>();

            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var getAllServicesCommand = connection.CreateCommand();
            getAllServicesCommand.CommandText = @"select name, description, price, id
                                               from services";

            var reader = getAllServicesCommand.ExecuteReader();

            while (reader.Read())
            {
                var id = (int)reader["Id"];
                var name = reader["Name"].ToString();
                var description = reader["Description"].ToString();
                var price = (decimal)reader["Price"];

                var service = new Service(name, description, price) { Id = id };

                services.Add(service);
            }

            connection.Close();

            return services;
        }
    }
}
