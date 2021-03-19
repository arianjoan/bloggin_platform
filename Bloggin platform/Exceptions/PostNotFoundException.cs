using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggin_platform.Exceptions
{
    public class PostNotFoundException : Exception
    {
        public PostNotFoundException()
        {

        }

        public PostNotFoundException(string id) : base(String.Format("User with id {0} not found.", id))
        {

        }

        public PostNotFoundException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
