using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace After.hour.support.roaster.api.Model
{
    public class Roaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string firstLine { get; set; }
        public string secondLine { get; set; }
        public DateTime supportDueDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
