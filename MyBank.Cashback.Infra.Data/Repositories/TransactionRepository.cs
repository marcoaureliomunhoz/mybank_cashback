using Dapper;
using Microsoft.Extensions.Configuration;
using MyBank.Cashback.Domain.Entities;
using MyBank.Cashback.Domain.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MyBank.Cashback.Infra.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private string connectionString = "";

        public TransactionRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("MyBank_Cashback");
        }

        public IEnumerable<Transaction> ListAll()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    return connection.Query<Transaction>("select * from [Transaction]").ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public bool Insert(Transaction transaction)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    var sql = @"insert into [Transaction]
                                  (Description, Value, ClientId, RegisterDate)
                                values
                                  (@Description, @Value, @ClientId, getdate());
                                SELECT CAST(SCOPE_IDENTITY() as int)";

                    transaction.TransactionId = connection.QuerySingle<int>(sql, transaction);

                    return transaction.TransactionId > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}
