using Bloggin_platform.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggin_platform.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public User Author { get; set; }

        public int AuthorId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public EState State { get; set; } = EState.Public;

    }
}
