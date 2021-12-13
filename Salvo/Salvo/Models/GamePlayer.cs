using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Salvo.Models
{
    public class GamePlayer
    {
        public long Id { get; set; }
        public DateTime? JoinDate{ get; set; }

        public long PlayerId { get; set; }
        public Player Player { get; set; }

        public long GameId { get; set; }
        public Game Game { get; set; }

        public ICollection<Ship> Ships { get; set; }

        public ICollection<Salvo> Salvos { get; set; }

        public Score GetScore()
        {
            return Player.GetScore(Game);
        }

        public GamePlayer getOpponent()
        {
            return Game.GamePlayers.FirstOrDefault(gp => gp.Id != Id);
        }

        public ICollection<SalvoHitDTO> GetHits()
        {
            return Salvos.Select(salvo => new SalvoHitDTO
            {
                Turn = salvo.Turn,
                Hits = getOpponent()?.Ships.Select(ship => new ShipHitDTO
                {
                    Type = ship.Type,
                    Hits = salvo.Locations.Where
                    (salvoLocation => ship.Locations.Any(shipLocation => shipLocation.Location == salvoLocation.Location))
                    .Select(salvoLocation => salvoLocation.Location).ToList()
                }).ToList()
            }).ToList();
        }

        public ICollection<string> GetSunks()
        {
            int lastTurn = Salvos.Count;

            List<string> salvoLocations =
                getOpponent()?.Salvos
                .Where(salvo => salvo.Turn <= lastTurn)
                .SelectMany(salvo => salvo.Locations.Select(location => location.Location)).ToList();

            return Ships?
            .Where(ship => ship.Locations.Select(shipLocation => shipLocation.Location)
            .All(salvoLocation => salvoLocations != null ? salvoLocations.Any(shipLocation => shipLocation == salvoLocation) : false))
            .Select(ship => ship.Type).ToList();
        }

        public GameState GetGameState()
        {
            GameState gameState = GameState.ENTER_SALVO;
            GamePlayer opponent = getOpponent();
            // Evaluar los ships
            if (Ships == null || Ships?.Count() == 0)
                gameState = GameState.PLACE_SHIPS;
            else if(opponent == null)
            {
                if (Salvos != null && Salvos?.Count() > 0)
                    gameState = GameState.WAIT;
            }
            else
            {   
                int turn = Salvos != null ? Salvos.Count() : 0;
                int opponentTurn = opponent.Salvos != null ? opponent.Salvos.Count() : 0;

                if (turn > opponentTurn)
                    gameState = GameState.WAIT;
                else if(turn == opponentTurn && turn != 0)
                {
                    int playerSunks = GetSunks().Count();
                    int opponentSunks = opponent.GetSunks().Count();

                    if (playerSunks == Ships.Count() && opponentSunks == opponent.Ships.Count())
                        gameState = GameState.TIE;
                    else if (playerSunks == Ships.Count())
                        gameState = GameState.LOSS;
                    else if (opponentSunks == opponent.Ships.Count())
                        gameState = GameState.WIN;
                }
            }

            return gameState;
        }
    }
}
