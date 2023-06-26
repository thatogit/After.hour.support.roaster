using After.hour.support.roaster.api.Data;
using After.hour.support.roaster.api.Model;
using After.hour.support.roaster.api.Repository.IRepository;

namespace After.hour.support.roaster.api.Repository
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TeamRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Team> UpdateAsync(Team team)
        {
            _applicationDbContext.Update(team);
            await _applicationDbContext.SaveChangesAsync();

            return team;
        }
    }
}
