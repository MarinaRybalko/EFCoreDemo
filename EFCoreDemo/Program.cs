using System;
using System.IO;
using System.Linq;
using EFCoreDemo.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFCoreDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            var connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;

            using (var db = new ApplicationContext(options))
            {
                var companies = db.Companies.ToList();
                foreach (var company in companies)
                {
                    Console.WriteLine($"{company.Id}.{company.Name} - {company.FoundationDate:dd/MM/yyyy}");
                }

                db.Companies.Add(new Company() {FoundationDate = DateTime.Today, Name = "test"});
                db.SaveChanges();

            }
            Console.Read();
        }
    }
}
