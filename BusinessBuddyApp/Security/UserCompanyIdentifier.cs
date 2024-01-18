using BusinessBuddyApp.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BusinessBuddyApp.Security
{
    public class UserCompanyIdentifier
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserCompanyIdentifier(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public ClaimsPrincipal User => _httpContextAccessor.HttpContext.User;
        public int companyId => GetCompanyId(User);

        public int GetCompanyId(ClaimsPrincipal user)
        {
            var companyIdClaim = user.FindFirst(c => c.Type == "CompanyId")?.Value;

            if (companyIdClaim == null || !int.TryParse(companyIdClaim, out var companyId))
            {
                throw new NotFoundException("CompanyId claim is missing or invalid.");
            }
            return companyId;
        }
    }
}
