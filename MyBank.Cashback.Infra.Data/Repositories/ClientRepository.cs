using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyBank.Cashback.Domain.Entities;
using MyBank.Cashback.Domain.Interface.Repositories;

namespace MyBank.Cashback.Infra.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private string connectionString = "";

        public ClientRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("MyBank_Cashback");
        }

        public IEnumerable<Client> ListAll()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    return connection.Query<Client>("select * from [Client]").ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public Client Get(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    return connection.QueryFirstOrDefault<Client>(
                        "select * from [Client] where ClientId = @ClientId",
                         new { ClientId = id }
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public bool Insert(Client client)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    var sql = @"insert into [Client]
                                  (Name, CPF)
                                values
                                  (@Name, @CPF);
                                SELECT CAST(SCOPE_IDENTITY() as int)";

                    client.ClientId = connection.QuerySingle<int>(sql, client);

                    return client.ClientId > 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}