using System.ComponentModel.DataAnnotations;

namespace After.hour.support.roaster.api.Model.Login
{
    public class UserDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
