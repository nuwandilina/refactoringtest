﻿using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LegacyApp
{
    public class ClientRepository
    {
        public Client GetById(int id)
        {
            Client client = null;
            string connectionString = ConfigurationManager.ConnectionStrings["appDatabase"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "uspGetClientById"
                };

                var parameter = new SqlParameter("@ClientId", SqlDbType.Int) { Value = id };
                command.Parameters.Add(parameter);

                connection.Open();
                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    client = new Client
                                      {
                                          Id = int.Parse(reader["ClientId"].ToString()),
                                          Name = reader["Name"].ToString().Trim(),
                                          ClientStatus = (ClientStatus)int.Parse(reader["ClientStatusId"].ToString())
                                      };
                }
            }

            return client;
        }
    }
}
