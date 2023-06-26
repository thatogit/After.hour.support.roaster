using System.ComponentModel.DataAnnotations;

namespace After.hour.support.roaster.api.Model.Dto
{
    public class RoasterUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string firstLine { get; set; }
        [Required]
        [MaxLength(25)]
        public string secondLine { get; set; }
        [Required]
        public DateTime supportDueDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
