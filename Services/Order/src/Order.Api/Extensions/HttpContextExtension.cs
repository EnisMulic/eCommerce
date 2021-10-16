using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace Order.Api.Extensions
{
    public static class HttpContextExtension
    {
        public static Guid GetBuyerId(this HttpContext httpContext)
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
