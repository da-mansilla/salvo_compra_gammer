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
            
            if (!context.GamePlayers.Any())
            {
               Game game1 = context.Games.Find(1L);
                Game game2 = context.Games.Find(2L);
                Game game3 = context.Games.Find(3L);
                Game game4 = context.Games.Find(4L);
                Game game5 = context.Games.Find(5L);
                Game game6 = context.Games.Find(6L);
                Game game7 = context.Games.Find(7L);
                Game game8 = context.Games.Find(8L);

                Player jbauer = context.Players.Find(1L);
                Player obrian = context.Players.Find(2L);
                Player kbauer = context.Players.Find(3L);
                Player almeida = context.Players.Find(4L);

                var gamesPlayers = new GamePlayer[]
                {
                    new GamePlayer {JoinDate=DateTime.Now, Game = game1, Player = jbauer },
                    new GamePlayer {JoinDate=DateTime.Now, Game = game1, Player = obrian },
                    new GamePlayer {JoinDate=DateTime.Now, Game = game2, Player = jbauer },
                    new GamePlayer {JoinDate=DateTime.Now, Game = game2, Player = obrian },
                    new GamePlayer {JoinDate=DateTime.Now, Game = game3, Player = obrian },
                    new GamePlayer {JoinDate=DateTime.Now, Game = game3, Player = almeida },
                    new GamePlayer {JoinDate=DateTime.Now, Game = game4, Player = obrian },
                    new GamePlayer {JoinDate=DateTime.Now, Game = game4, Player = jbauer },
                    new GamePlayer {JoinDate=DateTime.Now, Game = game5, Player = jbauer },
                    new GamePlayer {JoinDate=DateTime.Now, Game = game5, Player = almeida },
                    new GamePlayer {JoinDate=DateTime.Now, Game = game6, Player = kbauer },
                    new GamePlayer {JoinDate=DateTime.Now, Game = game7, Player = almeida },
                    new GamePlayer {JoinDate=DateTime.Now, Game = game8, Player = kbauer },
                    new GamePlayer {JoinDate=DateTime.Now, Game = game8, Player = almeida },
                };

                foreach (GamePlayer gp in gamesPlayers)
                {
                    context.GamePlayers.Add(gp);
                }
                context.SaveChanges();

            }
            

        }
    }
}
