using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace After.hour.support.roaster.api.Model
{
    public class Team
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }
        public string TeamName { get; set; }
        public Employee employee { get; set; }     
        public string TeamLeader { get; set; }
    }
}
