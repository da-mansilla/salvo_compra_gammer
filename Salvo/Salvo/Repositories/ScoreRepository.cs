using Salvo.Models;

namespace Salvo.Repositories
{
    public class ScoreRepository : RepositoryBase<Score>, IScoreRepository
    {
        public ScoreRepository(SalvoContext repositoryContext) : base(repositoryContext)
        {

        }
        public void Save(Score score)
        {
            Create(score);
            SaveChanges();
        }
    }
}
