using Microsoft.EntityFrameworkCore;
using Salvo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Salvo.Repositories
{
    public class GamePlayerRepository : RepositoryBase<GamePlayer>, IGamePlayerRepository
    {
        public GamePlayerRepository(SalvoContext repositoryContext): base(repositoryContext) { }

        public GamePlayer FindById(long idGamePlayer)
        {
            return FindByCondition(gp => gp.Id == idGamePlayer)
                    .Include(gp => gp.Game)
                        .ThenInclude(game => game.GamePlayers)
                            .ThenInclude(gp => gp.Salvos)
                                .ThenInclude(salvos => salvos.Locations)
                    .Include(gp => gp.Game)
                        .ThenInclude(game => game.GamePlayers)
                            .ThenInclude(gp => gp.Ships)
                                .ThenInclude(ships => ships.Locations)
                    .Include(gp => gp.Player)
                    .FirstOrDefault();
        }

        public GamePlayer GetGamePlayerView(long idGamePlayer)
        {
            
            return FindAll(source => source.Include(gamePlayer => gamePlayer.Game)
                                                .ThenInclude(game => game.GamePlayers)
                                                    .ThenInclude(gp => gp.Player)
                                            .Include(gamePlayer => gamePlayer.Game)
                                                .ThenInclude(game => game.GamePlayers)
                                                    .ThenInclude(gp => gp.Salvos)
                                                        .ThenInclude(salvo => salvo.Locations)
                                            .Include(gamePlayer => gamePlayer.Game)
                                                .ThenInclude(game => game.GamePlayers)
                                                    .ThenInclude(gp => gp.Ships)
                                                        .ThenInclude(ship => ship.Locations)
                                            )
                .Where(gamePlayer => gamePlayer.Id == idGamePlayer)
                .OrderBy(game => game.JoinDate)
                .FirstOrDefault();
            /*
                return FindByCondition(gp => gp.Id == idGamePlayer)
                                                    .Include(gp => gp.Game)
                                                    .Include(gp => gp.Player)
                                                    .Include(gp => gp.Salvos)
                                                        .ThenInclude(salvo => salvo.Locations)
                                                    .Include(gp => gp.Ships)
                                                        .ThenInclude(ship => ship.Locations)
            
            
            return FindByCondition(gp => gp.Id == idGamePlayer)
                                            .Include(gamePlayer => gamePlayer.Game)
                                                .ThenInclude(game => game.GamePlayers)
                                                    .ThenInclude(gp => gp.Player)
                                            .Include(gamePlayer => gamePlayer.Game)
                                                .ThenInclude(game => game.GamePlayers)
                                                    .ThenInclude(gp => gp.Salvos)
                                                        .ThenInclude(salvo => salvo.Locations)
                                            .Include(gamePlayer => gamePlayer.Game)
                                                .ThenInclude(game => game.GamePlayers)
                                                    .ThenInclude(gp => gp.Ships)
                                                        .ThenInclude(ship => ship.Locations)
              */
            /*
            return FindAll(source2 => source2.Include(gamePlayer => gamePlayer.Ships)
                                                .ThenInclude(ship => ship.Locations)
                                            .Include(gamePlayer => gamePlayer.Salvos)
                                                .ThenInclude(salvo => salvo.Locations)
                                            .Include(gamePlayer => gamePlayer.Game)
                                                .ThenInclude(game => game.GamePlayers)
                                                    .ThenInclude(gp => gp.Player)
                                            .Include(gamePlayer => gamePlayer.Game)
                                                .ThenInclude(game => game.GamePlayers)
                                                    .ThenInclude(gp => gp.Salvos)
                                                    .ThenInclude(salvo => salvo.Locations)
                                            .Include(gamePlayer => gamePlayer.Game)
                                                .ThenInclude(game => game.GamePlayers)
                                                    .ThenInclude(gp => gp.Ships)
                                                    .ThenInclude(ship => ship.Locations)
                                            )
                .Where(gamePlayer => gamePlayer.Id == idGamePlayer)
                .OrderBy(game => game.JoinDate)
                .FirstOrDefault();
             */
        }

        public void Save(GamePlayer gamePlayer)
        {
            if (gamePlayer.Id == 0)
                Create(gamePlayer);
            else
                Update(gamePlayer);
            SaveChanges();
        }


    }
}
