using Microsoft.EntityFrameworkCore;
using System;

namespace MediamakerTechTest
{
    public class RequestDbContext : DbContext
    {


        public DbSet<RequestLog> RequestLogs { get; set; }

        public RequestDbContext(DbContextOptions<RequestDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=requests.db");
            }
        }



    }
}
