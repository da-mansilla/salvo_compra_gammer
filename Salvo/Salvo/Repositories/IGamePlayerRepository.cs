using Salvo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Salvo.Repositories
{
    public interface IGamePlayerRepository
    {
        GamePlayer GetGamePlayerView(long idGamePlayer);
        void Save(GamePlayer gamePlayer);
        GamePlayer FindById(long idGamePlayer);
        
    }
}
