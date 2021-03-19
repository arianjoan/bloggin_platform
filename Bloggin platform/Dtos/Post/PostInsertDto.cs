
using Bloggin_platform.Utils;

namespace Bloggin_platform.Dtos.Post
{
    public class PostInsertDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public EState State { get; set; }

    }
}
