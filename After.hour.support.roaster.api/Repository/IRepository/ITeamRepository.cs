using After.hour.support.roaster.api.Model;

namespace After.hour.support.roaster.api.Repository.IRepository
{
    public interface ITeamRepository : IRepository<Team>
    {
        Task<Team> UpdateAsync(Team team);
    }
}
