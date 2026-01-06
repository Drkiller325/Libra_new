using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extentions
{
    public static class ClaimExtention
    {
        public static string GetSpecificClaim(this ClaimsIdentity claimsIdentity, string claimType)
        {
            Claim claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == claimType);

            return claim != null ? claim.Value : string.Empty;
        }
    }
}
