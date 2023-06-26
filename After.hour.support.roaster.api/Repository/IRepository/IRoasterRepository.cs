using After.hour.support.roaster.api.Model;

namespace After.hour.support.roaster.api.Repository.IRepository
{
    public interface IRoasterRepository : IRepository<Roaster>
    {
        Task<Roaster> UpdateAsync(Roaster roaster);
    }
}
