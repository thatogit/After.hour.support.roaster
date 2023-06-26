using After.hour.support.roaster.api.Data;
using After.hour.support.roaster.api.Model;
using After.hour.support.roaster.api.Repository.IRepository;

namespace After.hour.support.roaster.api.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EmployeeRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            _applicationDbContext.Employees.Update(employee);
            await _applicationDbContext.SaveChangesAsync();

            return employee;
        }
    }
}
