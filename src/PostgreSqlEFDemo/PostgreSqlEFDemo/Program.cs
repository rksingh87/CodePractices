using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PostgreSqlEFDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            //var command = sg_PocContext.Database.ExecuteSqlRaw("public.get_row_count()");

            //using var con = new NpgsqlConnection(cs);
            //con.Open();

            //var sql = "SELECT public.get_row_count()";

            //using var cmd = new NpgsqlCommand(sql, con);

            //var version = cmd.ExecuteScalar().ToString();

            //con.Close();
            //Console.WriteLine($"PostgreSQL version: {version}");

            Console.WriteLine("Process ID: {0}", System.Diagnostics.Process.GetCurrentProcess().Id);

            while (true)
            {

                Console.WriteLine("Fetching Item....");

                PostgreSqlHelper helper = new PostgreSqlHelper();

                var list = helper.GetAccountQueue(5);

                Console.WriteLine("Total Item Found: {0}", list.Count);

                foreach (var item in list)
                {
                    Console.WriteLine(string.Format("Account ID: {0} Amount {1}", item.AccountId, item.Amount));
                }

                Console.WriteLine("=====================Hello World!=====================");

                Thread.Sleep(1000);
            }


            

            Console.ReadLine();
        }
    }
}
