using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace After.hour.support.roaster.api.Model.Dto
{
    public class TeamDto
    {
        public int Id { get; set; }
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        public string TeamName { get; set; }
        [Required]
        public string TeamLeader { get; set; }
    }
}
