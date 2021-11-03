using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Salvo.Models
{
    public static class DbInitializer
    {
        public static void Initialize(SalvoContext context)
        {
            if (!context.Players.Any())
            {
                var players = new Player[]
                {
                    new Player
                    {
                        Email = "j.bauer@ctu.gov",
                        Name = "Jack Bauer",
                        Password = "24"
                    },
                    new Player
                    {
                        Email = "c.obrain@ctu.gov",
                        Name = "Chloe O'Brian",
                        Password = "42"
                    },
                    new Player
                    {
                        Email = "kim_bauer@gmail.com",
                        Name = "Kim Bauer",
                        Password = "kb"
                    },
                    new Player
                    {
                        Email = "t.almeida@ctu.gov",
                        Name = "Tony Almeida",
                        Password = "mole"
                    }
                };
                // Recorremos los players
                foreach(Player player in players)
                {
                    context.Players.Add(player);
                }
                // Guardamos los cambios
                context.SaveChanges();

            }
            if (!context.Games.Any())
            {
                var games = new Game[]
                {
                    new Game {CreationDate = DateTime.Now },
                    new Game {CreationDate = DateTime.Now.AddHours(1) },
                    new Game {CreationDate = DateTime.Now.AddHours(2) },
                    new Game {CreationDate = DateTime.Now.AddHours(3) },
                    new Game {CreationDate = DateTime.Now.AddHours(4) },
                    new Game {CreationDate = DateTime.Now.AddHours(5) },
                    new Game {CreationDate = DateTime.Now.AddHours(6) },
                    new Game {CreationDate = DateTime.Now.AddHours(7) }
                };
                // Recorremos los games
                foreach (Game game in games)
                {
                    context.Games.Add(game);
                }
                // Guardamos los cambios
                context.SaveChanges();
            }
            
        }
    }
}
