using Common.Basket.Authorization;
using Common.Order.Authorization;
using Common.Product.Authorization;
using Identity.Api.Models;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.Api.Services
{
    public class AuthService : IAuthService<ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ApplicationUser> FindByUsernameAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task SignInAsync(ApplicationUser user, AuthenticationProperties properties, string authenticationMethod = null)
        {
            await _signInManager.SignInAsync(user, properties, authenticationMethod);
        }

        public async Task<IdentityResult> SignUpAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            await _userManager.AddClaimAsync(user, new Claim("api", ProductApi.Scope.Read.Name));
            await _userManager.AddClaimAsync(user, new Claim("api", OrderApi.Scope.Read.Name));
            await _userManager.AddClaimAsync(user, new Claim("api", BasketApi.Resource.Name));
            
            return result;
        }

        public async Task<bool> ValidateCredentialsAsync(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
    }
}
