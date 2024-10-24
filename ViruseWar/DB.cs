using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.Xml;

namespace ViruseWar
{
    // TODO: add the result to 1 of 4 slots (or replace).
    // Delete certain slot.
    // Get data from certain slot.
    // Get data from all table.
    public class Game
    {
        [Key]
        public required string Name { get; set; }
        public required string FieldMatrix { get; set; } // JSON Serialized m_field
    }

    // Singleton DB
    public class DB : DbContext
    {
        public static DB Instance = new();
        public DbSet<Game> Games { get; set; }

        private DB()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ViruseWar;Trusted_Connection=True;Encrypt=False");
        }

        // LINQ Requests
        // Games
        // TODO: recode, name is unique
        public void AddGameToSlot(string name, string fieldMatrix)
        {
            var game = Games.SingleOrDefault(g => g.Name == name);
            if (game != null)
            {
                game.FieldMatrix = fieldMatrix;
                SaveChanges();
                return;
            }

            Games.Add(new Game { Name = name, FieldMatrix = fieldMatrix });
            SaveChanges();

            /*
            if (Games.Count() < 5) {
                Games.Add(new Game { Name = name, FieldMatrix = fieldMatrix });
            } else {
                var gameToReplace = Games.First();
                gameToReplace.FieldMatrix = fieldMatrix;
            }
            SaveChanges();
            */
        }
        public bool DeleteGameByName(string name)
        {
            var game = Games.SingleOrDefault(g => g.Name == name);
            if (game == null)
                return false;
        
            Games.Remove(game);
            SaveChanges();
            return true;
        }
        public string GetGameByName(string name)
        {
            var game = Games.SingleOrDefault(g => g.Name == name);
            if (game == null)
                throw new Exception("Game with this name doesn't exist in database.");
            return game.FieldMatrix;
        }
        public string[]? GetGamesName()
        {
            return Games.Select(g => g.Name).ToArray();
        }
    }
}
