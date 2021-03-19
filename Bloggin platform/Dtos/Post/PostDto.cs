using Bloggin_platform.Dtos.User;

namespace Bloggin_platform.Dtos.Post
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public UserDto Author { get; set; }
    }
}
