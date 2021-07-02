using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PostgreSqlConcurrencyCheck.Database;
using PostgreSqlConcurrencyCheck.Entity;
using System;

namespace PostgreSqlConcurrencyCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            Startup startup = new Startup();
            startup.ConfigureServices(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var repo = serviceProvider.GetService<EmployeeRespository>();

            //for (int i = 0; i < 10; i++)
            //{
            //    var employee = repo.GetEmployee(1);
            //    employee.count++;
            //    repo.UpdateEmployee(employee);
            //    Console.WriteLine($"Save Completed {i}");
            //}

            //string[] strList = new string[] { "Rohit", "Kumar", "SG", "RKS" };

            //for (int i = 0; i < 10; i++)
            //{
            //    for (int j = 0; j < strList.Length; j++)
            //    {
            //        repo.InsertOrUpdateByName(strList[j]);
            //    }
            //}

            for (int i = 0; i < 10; i++)
            {
               repo.UpdateSertEmployee("RKUMAR");
            }

            Console.WriteLine("Completed");

            Console.Read();
        }
    }
}
