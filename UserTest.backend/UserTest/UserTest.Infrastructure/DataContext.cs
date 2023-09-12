using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTest.Domain.Entities;

namespace UserTest.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
         : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany(x => x.TheTests)
                .WithMany(x => x.AssignedUsers);
            base.OnModelCreating(builder);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<TheTest> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public DbSet<UserTestResult> Results { get; set; }

    }
}
