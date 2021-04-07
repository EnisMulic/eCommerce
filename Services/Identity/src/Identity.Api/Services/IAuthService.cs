using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;

namespace Identity.Api.Services
{
    public interface IAuthService<T>
    {
        Task<T> FindByUsernameAsync(string email);
        Task<bool> ValidateCredentialsAsync(T user, string password);
        Task SignInAsync(T user, AuthenticationProperties properties, string authenticationMethod = null);
    }
}
