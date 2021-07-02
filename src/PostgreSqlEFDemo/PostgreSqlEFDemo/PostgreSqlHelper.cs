using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PostgreSqlEFDemo
{
    public class PostgreSqlHelper
    {
        public List<Account> GetAccountQueue(int queueSize)
        {
            List<Account> accountQueue = new List<Account>();

            var cs = "Host=postgresqlazure.postgres.database.azure.com;Database=sg_poc;Username=postgresqlazure@postgresqlazure;Password=PgsQL@87";

            using var con = new NpgsqlConnection(cs);
            con.Open();
            using var cmd = new NpgsqlCommand("select * from public.get_queue(@limitCount)", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("limitCount", queueSize);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Account account = new Account
                {
                    AccountId = reader.GetInt32(0),
                    Amount = reader.GetInt32(1)
                };

                accountQueue.Add(account);
            }
            con.Close();

            return accountQueue;
        }
    }
}
