using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggin_platform.Services.Contracts
{
    public interface IAuthService
    {
        public string Authenticate(string username, string password);
    }
}
