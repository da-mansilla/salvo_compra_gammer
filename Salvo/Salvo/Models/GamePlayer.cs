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
                    .Select(SalvoLocation => SalvoLocation.Location).ToList()
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
            .All(shipLocation => shipLocation != null ? salvoLocations.Any(salvoLocation => salvoLocation == shipLocation) : false))
            .Select(ship => ship.Type).ToList();
        }
    }
}
