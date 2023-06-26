using After.hour.support.roaster.api.Model;

namespace After.hour.support.roaster.api.Repository.IRepository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee> UpdateAsync(Employee employee);
    }
}
