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
    // TODO: delete Result with certain DateTime.
    // Add new result (if have less moves with win. 10 slots are max history).
    // Get all data from table.
    public class Result
    {
        public Player Winner { get; set; } // 1st or 2nd player
        public int Moves { get; set; }
        [Key]
        public DateTime GameDateTime { get; set; } // Endgame datetime
    }

    // TODO: add the result to 1 of 4 slots (or replace).
    // Delete certain slot.
    // Get data from certain slot.
    // Get data from all table.
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public required string FieldMatrix { get; set; } // JSON Serialized m_field
    }

    // Singleton DB
    /* Example of use
       var db = Database.Instance;
       var allResults = db.Results.ToList();
    */
    public class Database : DbContext
    {
        private static readonly Lazy<Database> instance = new(() => new Database());
        public static Database Instance => instance.Value;


        public DbSet<Result> Results { get; set; }
        public DbSet<Game> Games { get; set; }

        private Database()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=appdb1;Trusted_Connection=True;");
        }
    }
}
