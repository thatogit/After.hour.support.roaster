using System.ComponentModel.DataAnnotations;

namespace After.hour.support.roaster.api.Model.Dto
{
    public class EmployeeDto
    {
        public int EmployeeID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string? cellNumber { get; set; }
    }
}
