using System;

namespace Bloggin_platform.Exceptions
{
    public class UserHasNotPermissionException : Exception
    {
        public UserHasNotPermissionException() : base("The user does not have permission to do this action")
        {

        }

        public UserHasNotPermissionException(string id) : base(String.Format("User has not permission."))
        {

        }

        public UserHasNotPermissionException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}