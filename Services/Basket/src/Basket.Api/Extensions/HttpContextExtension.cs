using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace Basket.Api.Extensions
{
    public static class HttpContextExtension
    {
        public static Guid GetCustomerId(this HttpContext httpContext)
        {
            var id = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (string.IsNullOrEmpty(id))
            {
                return Guid.Empty;
            }

            return new Guid(id);
        }
    }
}
