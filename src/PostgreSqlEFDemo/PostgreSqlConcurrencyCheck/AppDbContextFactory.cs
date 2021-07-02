using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PostgreSqlConcurrencyCheck.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostgreSqlConcurrencyCheck
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<PostgresSqlDbContext>
    {
        public PostgresSqlDbContext CreateDbContext(string[] args)
        {

            string connectionString = "host=batyr.db.elephantsql.com;database=ratuhumq;port=5432;user id=ratuhumq;password=kbmvaUoUIcf97tqjHhv9qWDnt0MjH0RN";
            var optionsBuilder = new DbContextOptionsBuilder<PostgresSqlDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new PostgresSqlDbContext(optionsBuilder.Options);
        }
    }
}
