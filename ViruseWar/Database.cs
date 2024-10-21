using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;

namespace ViruseWar
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
    public class Database : DbContext
    {
        public DbSet<User> Users { get; set; }
        public Database()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=appdb1;Trusted_Connection=True;");
        }
    }
}
