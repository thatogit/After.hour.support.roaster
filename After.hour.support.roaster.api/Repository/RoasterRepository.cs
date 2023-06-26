using After.hour.support.roaster.api.Data;
using After.hour.support.roaster.api.Model;
using After.hour.support.roaster.api.Repository.IRepository;
using System.Linq.Expressions;

namespace After.hour.support.roaster.api.Repository
{
    public class RoasterRepository : Repository<Roaster>, IRoasterRepository
    {
        
        private readonly ApplicationDbContext _applicationDbContext;

        public RoasterRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Roaster> UpdateAsync(Roaster roaster)
        {
            roaster.UpdateDate=DateTime.Now.Date;
            _applicationDbContext.Roasters.Update(roaster);
            await _applicationDbContext.SaveChangesAsync();

            return roaster;
        }
    }

}
