using Bloggin_platform.Utils;
using System.ComponentModel.DataAnnotations;


namespace Bloggin_platform.Dtos.User
{
    public class UserInsertDto
    {
        [Required]
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public ERole Role { get; set; }     
    }
}
