using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bloggin_platform.Utils
{
    public static class ClaimResolver
    {
        public static int? getUserIdFromToken(ClaimsPrincipal userClaims)
        {
            var isLogged = userClaims.HasClaim(u => u.Type == "id");
            var idClaim = string.Empty;

            if (isLogged)
            {
                idClaim = userClaims.Claims.First(c => c.Type.Equals("id")).Value;
                int.TryParse(idClaim, out int id);
                return id;
            }

            return null;
        }
    }
}
