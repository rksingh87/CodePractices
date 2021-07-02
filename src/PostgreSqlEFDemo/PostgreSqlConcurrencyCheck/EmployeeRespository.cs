using Microsoft.EntityFrameworkCore;
using Npgsql;
using PostgreSqlConcurrencyCheck.Database;
using PostgreSqlConcurrencyCheck.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PostgreSqlConcurrencyCheck
{
    public class EmployeeRespository
    {
        private readonly PostgresSqlDbContext dbContext = null;

        public EmployeeRespository(PostgresSqlDbContext postgresSqlDbContext)
        {
            dbContext = postgresSqlDbContext;
        }

        public bool SaveEmployee(Employee employee)
        {
            dbContext.Employees.Add(employee);

            int res = dbContext.SaveChanges();

            return res >= 0;
        }

        public bool UpdateEmployee(Employee employee)
        {
            bool saveFailed;
            do
            {
                try
                {
                    saveFailed = false;

                    Console.WriteLine($"Count {employee.count}");

                    dbContext.Entry(employee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    int res = dbContext.SaveChanges();
                    return res >= 0;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;
                    ex.Entries.Single().Reload();

                    Console.WriteLine("Concurency Error");
                }
            }
            while (saveFailed);

            return true;

            //dbContext.Employees.Attach(employee);
            //dbContext.Entry(employee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //int res = dbContext.SaveChanges();

            //return res >= 0;
        }

        public bool InsertOrUpdateByName(string name)
        {
            var employee = dbContext.Employees.FirstOrDefault(t => t.name == name);
            if (employee == null)
                dbContext.Employees.Add(new Employee() { name = name, count = 1 });
            else
                employee.count++;

            dbContext.SaveChanges();
            return true;
        }


        public Employee GetEmployee(int id)
        {
            Employee emp = dbContext.Employees.Find(id);

            return emp;
        }


        public bool UpdateEmployeeCount(string name)
        {
            string connectionString = "host=batyr.db.elephantsql.com;database=ratuhumq;port=5432;user id=ratuhumq;password=kbmvaUoUIcf97tqjHhv9qWDnt0MjH0RN";

            using var con = new NpgsqlConnection(connectionString);
            con.Open();
            using var cmd = new NpgsqlCommand("select * from public.upsertEmployee(@name1)", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("name1", name);
            var reader = cmd.ExecuteScalar();

            con.Close();

            return true;
        }


        public bool UpdateSertEmployee(string name)
        {
            dbContext.Upsert(new Employee() { name = name, count = 1 })
                .On(c => c.name)
                .WhenMatched(h => new Employee() { count = h.count + 1 }).Run();

            return true;
        }


    }
}
