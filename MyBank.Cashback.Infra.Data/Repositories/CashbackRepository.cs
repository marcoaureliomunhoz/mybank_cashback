using Dapper;
using Microsoft.Extensions.Configuration;
using MyBank.Cashback.Domain.Entities;
using MyBank.Cashback.Domain.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace MyBank.Cashback.Infra.Data.Repositories
{
  public class CashbackRepository : ICashbackRepository
    {
        private string connectionString = "";

        public CashbackRepository(IConfiguration configuration)
        {
            connectionString = configuration?.GetConnectionString("MyBank_Cashback") ?? "";
        }

        public CashbackConfiguration GetConfiguration()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    return connection.QueryFirstOrDefault<CashbackConfiguration>("select * from [CashbackConfiguration]");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public bool InsertAccount(CashbackAccount account)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    var sql = @"insert into [CashbackAccount]
                                  (ClientId, TransactionId, Value, RegisterDate, Active)
                                values
                                  (@ClientId, @TransactionId, @Value, getdate(), 1)
                                SELECT CAST(SCOPE_IDENTITY() as int)";

                    account.CashbackAccountId = connection.QuerySingle<int>(sql, account);

                    return account.CashbackAccountId > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public bool ExistsAccountByTransaction(int transactionId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    var sql = @"select count(*) from [CashbackAccount] where TransactionId = @TransactionId";

                    return connection.ExecuteScalar<int>(
                        sql,
                        new { TransactionId = transactionId }
                    ) > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return true;
        }

        public decimal GetBalance(int clientId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    var sql = @"select sum(Value) from [CashbackAccount] where ClientId = @ClientId";

                    return connection.ExecuteScalar<decimal>(
                        sql,
                        new { ClientId = clientId }
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return -1;
        }

        public IEnumerable<CashbackAccount> ListAll(int clientId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    return connection.Query<CashbackAccount>(
                        "select * from [CashbackAccount] where ClientId = @ClientId",
                        new { ClientId = clientId }
                    ).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
