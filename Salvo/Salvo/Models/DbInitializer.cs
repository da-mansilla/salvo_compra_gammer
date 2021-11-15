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
            if (!context.Ships.Any())
            {
                GamePlayer gamePlayer1 = context.GamePlayers.Find(1L);
                GamePlayer gamePlayer2 = context.GamePlayers.Find(2L);
                GamePlayer gamePlayer3 = context.GamePlayers.Find(3L);
                GamePlayer gamePlayer4 = context.GamePlayers.Find(4L);
                GamePlayer gamePlayer5 = context.GamePlayers.Find(5L);
                GamePlayer gamePlayer6 = context.GamePlayers.Find(6L);
                GamePlayer gamePlayer7 = context.GamePlayers.Find(7L);
                GamePlayer gamePlayer8 = context.GamePlayers.Find(8L);
                GamePlayer gamePlayer9 = context.GamePlayers.Find(9L);
                GamePlayer gamePlayer10 = context.GamePlayers.Find(10L);
                GamePlayer gamePlayer11 = context.GamePlayers.Find(11L);
                GamePlayer gamePlayer12 = context.GamePlayers.Find(12L);
                GamePlayer gamePlayer13 = context.GamePlayers.Find(13L);

                var ships = new Ship[]
                {
                    //esta es solo la primera linea de los datos del pdf
                    new Ship
                    {
                        Type = "Destroyer",
                        GamePlayer = gamePlayer1,
                        Locations = new ShipLocation[]
                        {
                            new ShipLocation{Location = "H2"},
                            new ShipLocation{Location = "H3"},
                            new ShipLocation{Location = "H4"}
                        }
                    },
                    new Ship{Type = "Submarine", GamePlayer = gamePlayer1, Locations = new ShipLocation[] {
                            new ShipLocation { Location = "E1" },
                            new ShipLocation { Location = "F1" },
                            new ShipLocation { Location = "G1" }
                        }
                    },
                    new Ship{Type = "PatroalBoat", GamePlayer = gamePlayer1, Locations = new ShipLocation[] {
                            new ShipLocation { Location = "B4" },
                            new ShipLocation { Location = "B5" }
                        }
                    },

                    //obrian gp2
                    new Ship{Type = "Destroyer", GamePlayer = gamePlayer2, Locations = new ShipLocation[] {
                            new ShipLocation { Location = "B5" },
                            new ShipLocation { Location = "C5" },
                            new ShipLocation { Location = "D5" }
                        }
                    },
                    new Ship{Type = "PatroalBoat", GamePlayer = gamePlayer2, Locations = new ShipLocation[] {
                            new ShipLocation { Location = "F1" },
                            new ShipLocation { Location = "F2" }
                        }
                    },

                    //jbauer gp3
                    new Ship{Type = "Destroyer", GamePlayer = gamePlayer3, Locations = new ShipLocation[] {
                            new ShipLocation { Location = "B5" },
                            new ShipLocation { Location = "C5" },
                            new ShipLocation { Location = "D5" }
                        }
                    },
                    new Ship{Type = "PatroalBoat", GamePlayer = gamePlayer3, Locations = new ShipLocation[] {
                            new ShipLocation { Location = "C6" },
                            new ShipLocation { Location = "C7" }
                        }
                    },

                    //obrian gp4
                    new Ship{Type = "Submarine", GamePlayer = gamePlayer4, Locations = new ShipLocation[] {
                            new ShipLocation { Location = "A2" },
                            new ShipLocation { Location = "A3" },
                            new ShipLocation { Location = "A4" }
                        }
                    },

                    new Ship{Type = "PatroalBoat", GamePlayer = gamePlayer4, Locations = new ShipLocation[] {
                            new ShipLocation { Location = "G6" },
                            new ShipLocation { Location = "H6" }
                        }
                    },

                    //obrian gp5
                    new Ship{Type = "Destroyer", GamePlayer = gamePlayer5, Locations = new ShipLocation[] {
                            new ShipLocation { Location = "B5" },
                            new ShipLocation { Location = "C5" },
                            new ShipLocation { Location = "D5" }
                        }
                    },

                    new Ship{Type = "PatroalBoat", GamePlayer = gamePlayer5, Locations = new ShipLocation[] {
                            new ShipLocation { Location = "C6" },
                            new ShipLocation { Location = "C7" }
                        }
                    },

                    //talmeida gp6
                    new Ship{Type = "Submarine", GamePlayer = gamePlayer6, Locations = new ShipLocation[] {
                            new ShipLocation { Location = "A2" },
                            new ShipLocation { Location = "A3" },
                            new ShipLocation { Location = "A4" }
                        }
                    },
                    new Ship{Type = "PatroalBoat", GamePlayer = gamePlayer6, Locations = new ShipLocation[] {
                            new ShipLocation { Location = "G6" },
                            new ShipLocation { Location = "H6" }
                        }
                    },

                    //obrian gp7
                    new Ship{Type = "Destroyer", GamePlayer = gamePlayer7, Locations = new ShipLocation[] {
                            new ShipLocation { Location = "B5" },
                            new ShipLocation { Location = "C5" },
                            new ShipLocation { Location = "D5" }
                        }
                    },
                    new Ship{Type = "PatroalBoat", GamePlayer = gamePlayer7, Locations = new ShipLocation[] {
                            new ShipLocation { Location = "C6" },
                            new ShipLocation { Location = "C7" }
                        }
                    },

                    //jbauer gp8
                    new Ship{Type = "Submarine", GamePlayer = gamePlayer8, Locations = new ShipLocation[] {
                            new ShipLocation { Location = "A2" },
                            new ShipLocation { Location = "A3" },
                            new ShipLocation { Location = "A4" }
                        }
                    },
                    new Ship{Type = "PatroalBoat", GamePlayer = gamePlayer8, Locations = new ShipLocation[] {
                            new ShipLocation { Location = "G6" },
                            new ShipLocation { Location = "H6" }
                        }
                    },

                    //talmeida gp9
                    new Ship{Type = "Destroyer", GamePlayer = gamePlayer9, Locations = new ShipLocation[] {
                            new ShipLocation { Location = "B5" },
                            new ShipLocation { Location = "C5" },
                            new ShipLocation { Location = "D5" }
                        }
                    },
                    new Ship{Type = "PatroalBoat", GamePlayer = gamePlayer9, Locations = new ShipLocation[] {
                            new ShipLocation { Location = "C6" },
                            new ShipLocation { Location = "C7" }
                        }
                    },

                    //jbauer gp10
                    new Ship{Type = "Submarine", GamePlayer = gamePlayer10, Locations = new ShipLocation[] {
                            new ShipLocation { Location = "A2" },
                            new ShipLocation { Location = "A3" },
                            new ShipLocation { Location = "A4" }
                        }
                    },
                    new Ship{Type = "PatroalBoat", GamePlayer = gamePlayer10, Locations = new ShipLocation[] {
                            new ShipLocation { Location = "G6" },
                            new ShipLocation { Location = "H6" }
                        }
                    },

                    //kbauer gp11
                    new Ship{Type = "Destroyer", GamePlayer = gamePlayer11, Locations = new ShipLocation[] {
                            new ShipLocation { Location = "B5" },
                            new ShipLocation { Location = "C5" },
                            new ShipLocation { Location = "D5" }
                        }
                    },
                    new Ship{Type = "PatroalBoat", GamePlayer = gamePlayer11, Locations = new ShipLocation[] {
                            new ShipLocation { Location = "C6" },
                            new ShipLocation { Location = "C7" }
                        }
                    },

                    //kbauer gp12
                    new Ship{Type = "Destroyer", GamePlayer = gamePlayer12, Locations = new ShipLocation[] {
                            new ShipLocation { Location = "B5" },
                            new ShipLocation { Location = "C5" },
                            new ShipLocation { Location = "D5" }
                        }
                    },
                    new Ship{Type = "PatroalBoat", GamePlayer = gamePlayer12, Locations = new ShipLocation[] {
                            new ShipLocation { Location = "C6" },
                            new ShipLocation { Location = "C7" }
                        }
                    },

                    //talmeida gp13
                    new Ship{Type = "Submarine", GamePlayer = gamePlayer13, Locations = new ShipLocation[] {
                            new ShipLocation { Location = "A2" },
                            new ShipLocation { Location = "A3" },
                            new ShipLocation { Location = "A4" }
                        }
                    },
                    new Ship{Type = "PatroalBoat", GamePlayer = gamePlayer13, Locations = new ShipLocation[] {
                            new ShipLocation { Location = "G6" },
                            new ShipLocation { Location = "H6" }
                        }
                    },

                };

                foreach (Ship ship in ships)
                {
                    context.Ships.Add(ship);
                }

                context.SaveChanges();

            }
            if (!context.Salvos.Any())
            {
                GamePlayer gamePlayer1 = context.GamePlayers.Find(1L);
                GamePlayer gamePlayer2 = context.GamePlayers.Find(2L);
                GamePlayer gamePlayer3 = context.GamePlayers.Find(3L);
                GamePlayer gamePlayer4 = context.GamePlayers.Find(4L);
                GamePlayer gamePlayer5 = context.GamePlayers.Find(5L);
                GamePlayer gamePlayer6 = context.GamePlayers.Find(6L);
                GamePlayer gamePlayer7 = context.GamePlayers.Find(7L);
                GamePlayer gamePlayer8 = context.GamePlayers.Find(8L);
                GamePlayer gamePlayer9 = context.GamePlayers.Find(9L);
                GamePlayer gamePlayer10 = context.GamePlayers.Find(10L);


                var salvos = new Salvo[]
                {
                    //gp 1
                    new Salvo
                    {
                        Turn = 1, GamePlayer = gamePlayer1,
                        Locations = new SalvoLocation[]
                        {
                            new SalvoLocation{ Location = "B5"},
                            new SalvoLocation{ Location = "C5"},
                            new SalvoLocation { Location = "F1"}
                        }
                    },
                    new Salvo{Turn = 2, GamePlayer = gamePlayer1, Locations = new SalvoLocation[] {
                            new SalvoLocation { Location = "F2" },
                            new SalvoLocation { Location = "D5" }
                        }
                    },

                    //cobrian gp2
                    new Salvo{Turn = 1, GamePlayer = gamePlayer2, Locations = new SalvoLocation[] {
                            new SalvoLocation { Location = "B4" },
                            new SalvoLocation { Location = "B5" },
                            new SalvoLocation { Location = "B6" }
                        }
                    },
                    new Salvo{Turn = 2, GamePlayer = gamePlayer2, Locations = new SalvoLocation[] {
                            new SalvoLocation { Location = "E1" },
                            new SalvoLocation { Location = "H3" },
                            new SalvoLocation { Location = "A2" }
                        }
                    },

                    //jbauer gp3
                    new Salvo{Turn = 1, GamePlayer = gamePlayer3, Locations = new SalvoLocation[] {
                            new SalvoLocation { Location = "A2" },
                            new SalvoLocation { Location = "A4" },
                            new SalvoLocation { Location = "G6" }
                        }
                    },
                    new Salvo{Turn = 2, GamePlayer = gamePlayer3, Locations = new SalvoLocation[] {
                            new SalvoLocation { Location = "A3" },
                            new SalvoLocation { Location = "H6" }
                        }
                    },

                    //obrian gp4
                    new Salvo{Turn = 1, GamePlayer = gamePlayer4, Locations = new SalvoLocation[] {
                            new SalvoLocation { Location = "B5" },
                            new SalvoLocation { Location = "D5" },
                            new SalvoLocation { Location = "C7" }
                        }
                    },
                    new Salvo{Turn = 2, GamePlayer = gamePlayer4, Locations = new SalvoLocation[] {
                            new SalvoLocation { Location = "C5" },
                            new SalvoLocation { Location = "C6" }
                        }
                    },

                    //obrian gp5
                    new Salvo{Turn = 1, GamePlayer = gamePlayer5, Locations = new SalvoLocation[] {
                            new SalvoLocation { Location = "G6" },
                            new SalvoLocation { Location = "H6" },
                            new SalvoLocation { Location = "A4" }
                        }
                    },
                    new Salvo{Turn = 2, GamePlayer = gamePlayer5, Locations = new SalvoLocation[] {
                            new SalvoLocation { Location = "A2" },
                            new SalvoLocation { Location = "A3" },
                            new SalvoLocation { Location = "D8" }
                        }
                    },

                    //talmeida gp6
                    new Salvo{Turn = 1, GamePlayer = gamePlayer6, Locations = new SalvoLocation[] {
                            new SalvoLocation { Location = "H1" },
                            new SalvoLocation { Location = "H2" },
                            new SalvoLocation { Location = "H3" }
                        }
                    },
                    new Salvo{Turn = 2, GamePlayer = gamePlayer6, Locations = new SalvoLocation[] {
                            new SalvoLocation { Location = "E1" },
                            new SalvoLocation { Location = "F2" },
                            new SalvoLocation { Location = "G3" }
                        }
                    },

                    //obrian gp7
                    new Salvo{Turn = 1, GamePlayer = gamePlayer7, Locations = new SalvoLocation[] {
                            new SalvoLocation { Location = "A3" },
                            new SalvoLocation { Location = "A4" },
                            new SalvoLocation { Location = "F7" }
                        }
                    },
                    new Salvo{Turn = 2, GamePlayer = gamePlayer7, Locations = new SalvoLocation[] {
                            new SalvoLocation { Location = "A2" },
                            new SalvoLocation { Location = "G6" },
                            new SalvoLocation { Location = "H6" }
                        }
                    },

                    //jbauer gp8
                    new Salvo{Turn = 1, GamePlayer = gamePlayer8, Locations = new SalvoLocation[] {
                            new SalvoLocation { Location = "B5" },
                            new SalvoLocation { Location = "C6" },
                            new SalvoLocation { Location = "H1" }
                        }
                    },
                    new Salvo{Turn = 2, GamePlayer = gamePlayer8, Locations = new SalvoLocation[] {
                            new SalvoLocation { Location = "C5" },
                            new SalvoLocation { Location = "C7" },
                            new SalvoLocation { Location = "D5" }
                        }
                    },

                    //talmeida gp9
                    new Salvo{Turn = 1, GamePlayer = gamePlayer9, Locations = new SalvoLocation[] {
                            new SalvoLocation { Location = "A1" },
                            new SalvoLocation { Location = "A2" },
                            new SalvoLocation { Location = "A3" }
                        }
                    },
                    new Salvo{Turn = 2, GamePlayer = gamePlayer9, Locations = new SalvoLocation[] {
                            new SalvoLocation { Location = "G6" },
                            new SalvoLocation { Location = "G7" },
                            new SalvoLocation { Location = "G8" }
                        }
                    },

                    //jbauer gp10
                    new Salvo{Turn = 1, GamePlayer = gamePlayer10, Locations = new SalvoLocation[] {
                            new SalvoLocation { Location = "B5" },
                            new SalvoLocation { Location = "B6" },
                            new SalvoLocation { Location = "C7" }
                        }
                    },
                    new Salvo{Turn = 2, GamePlayer = gamePlayer10, Locations = new SalvoLocation[] {
                            new SalvoLocation { Location = "C6" },
                            new SalvoLocation { Location = "D6" },
                            new SalvoLocation { Location = "E6" }
                        }
                    },
                    new Salvo{Turn = 3, GamePlayer = gamePlayer10, Locations = new SalvoLocation[] {
                            new SalvoLocation { Location = "H1" },
                            new SalvoLocation { Location = "H8" }
                        }
                    },

                };

                foreach (Salvo salvo in salvos)
                {
                    context.Salvos.Add(salvo);
                }

                context.SaveChanges();
            }
            if (!context.Scores.Any())
            {
                Game game1 = context.Games.Find(1L);
                Game game2 = context.Games.Find(2L);
                Game game3 = context.Games.Find(3L);
                Game game4 = context.Games.Find(4L);


                Player jbauer = context.Players.Find(1L);
                Player obrian = context.Players.Find(2L);
                Player almeida = context.Players.Find(4L);

                var scores = new Score[]
                {
                    //game 1
                    new Score{ Game = game1, Player = jbauer, FinishDate = DateTime.Now, Point = 1 },
                    new Score{ Game = game1, Player = obrian, FinishDate = DateTime.Now, Point = 0 },

                    //game 2
                    new Score{ Game = game2, Player = jbauer, FinishDate = DateTime.Now.AddMinutes(1), Point = 0.5 },
                    new Score{ Game = game2, Player = obrian, FinishDate = DateTime.Now.AddMinutes(1), Point = 0.5 },

                    //obrian gp5
                    new Score {
                        Game = game3,
                        Player = obrian,
                        FinishDate = DateTime.Now.AddMinutes(2),
                        Point = 0
                    },

                    //almeida gp6
                    new Score {
                        Game = game3,
                        Player = almeida,
                        FinishDate = DateTime.Now.AddMinutes(2),
                        Point = 1
                    },

                    //obrian gp7
                    new Score {
                        Game = game4,
                        Player = obrian,
                        FinishDate = DateTime.Now.AddMinutes(3),
                        Point = 0.5
                    },

                    //jbauer gp8
                    new Score {
                        Game = game4,
                        Player = jbauer,
                        FinishDate = DateTime.Now.AddMinutes(3),
                        Point = 0.5
                    },
                };

                foreach (Score score in scores)
                {
                    context.Scores.Add(score);
                }

                context.SaveChanges();
            }


        }
    }
}
