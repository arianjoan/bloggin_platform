using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggin_platform.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException()
        {

        }

        public UserNotFoundException(string id) : base(String.Format("User with id {0} not found.", id))
        {

        }

        public UserNotFoundException (string message, Exception inner) : base(message,inner)
        {

        }
    }
}
