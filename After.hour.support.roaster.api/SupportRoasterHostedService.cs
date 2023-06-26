using After.hour.support.roaster.api.Repository.IRepository;

namespace After.hour.support.roaster.api
{
    public sealed class SupportRoasterHostedService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITeamRepository _teamReposioty;
        private readonly IRoasterRepository _roasterReposioty;
        public SupportRoasterHostedService(IEmployeeRepository employeeRepository, ITeamRepository teamReposioty, IRoasterRepository roasterReposioty)
        {
            _employeeRepository = employeeRepository;
            _teamReposioty = teamReposioty;
            _roasterReposioty = roasterReposioty;
        }

        public void Start()
        {
          var emps = _employeeRepository.GetAllAsync().Result;

            Console.WriteLine(emps);
        }
    }
}
