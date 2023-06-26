using System.ComponentModel.DataAnnotations;

namespace After.hour.support.roaster.api.Model.Dto
{
    public class EmployeeCreateDto
    {
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string? cellNumber { get; set; }
    }
}
