using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostgreSqlConcurrencyCheck.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostgreSqlConcurrencyCheck
{
    public class Startup
    {
        IConfigurationRoot Configuration { get; }

        public Startup()
        {
            //var builder = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json");

            //Configuration = builder.Build();
        }
            
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = "host=batyr.db.elephantsql.com;database=ratuhumq;port=5432;user id=ratuhumq;password=kbmvaUoUIcf97tqjHhv9qWDnt0MjH0RN";

            services.AddDbContext<PostgresSqlDbContext>(options => options.UseNpgsql(connectionString));
            services.AddSingleton<EmployeeRespository>();
        }
    }
}
