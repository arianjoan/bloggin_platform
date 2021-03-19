using Bloggin_platform.Utils;
using System.Collections.Generic;

namespace Bloggin_platform.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public ERole Role { get; set; }
        public IList<Post> Posts { get; set; } = new List<Post>();
        public bool IsDeleted { get; set; } = false;

    }
}
