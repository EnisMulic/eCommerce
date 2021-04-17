using Identity.Api.Database;
using Identity.Api.Models;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.Api.Services
{
    public class ProfileService : IProfileService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var identityString = context.Subject.GetSubjectId();
            if (Guid.TryParse(identityString, out Guid id))
            {
                var user = await _context.Users.FindAsync(id);

                if(user == null)
                {
                    return;
                }

                var claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.Id, user.Id.ToString(), ClaimValueTypes.String),
                    new Claim(JwtClaimTypes.PreferredUserName, user.UserName, ClaimValueTypes.String)
                };
                
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
