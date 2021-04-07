using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Identity.Api.Services
{
    public interface IAuthService<T>
    {
        Task<T> FindByUsernameAsync(string email);
        Task<bool> ValidateCredentialsAsync(T user, string password);
        Task SignInAsync(T user, AuthenticationProperties properties, string authenticationMethod = null);
        Task<IdentityResult> SignUpAsync(T user, string password);
    }
}
