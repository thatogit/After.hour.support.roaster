using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace After.hour.support.roaster.api.Model
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int EmployeeID { get; set; }
            [Required]
            public string firstName { get; set; }
            [Required]
            public string lastName { get; set; }
            [Required]
            public string email { get; set; }
            public string? cellNumber { get; set; }
    }
}
