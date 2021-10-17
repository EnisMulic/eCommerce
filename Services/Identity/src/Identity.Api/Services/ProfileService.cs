using Identity.Api.Database;
using Identity.Api.Models;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;

        public ProfileService(
            UserManager<ApplicationUser> userManager, 
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var identityString = context.Subject.GetSubjectId();
            if (Guid.TryParse(identityString, out Guid id))
            {
                var user = await _userManager.FindByIdAsync(id.ToString());

                if(user == null)
                {
                    return;
                }

                var principal = await _claimsFactory.CreateAsync(user);
                var claims = principal.Claims.ToList();
                
                context.IssuedClaims = claims;
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = await Task.FromResult(context.Subject.GetSubjectId());
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
