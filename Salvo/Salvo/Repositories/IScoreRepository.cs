using Salvo.Models;

namespace Salvo.Repositories
{
    public interface IScoreRepository
    {
        void Save(Score score);
    }
}
