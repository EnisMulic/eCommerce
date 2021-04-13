using Microsoft.AspNetCore.Http;
using System;

namespace Basket.Api.Extensions
{
    public static class HttpContextExtension
    {
        public static Guid GetCustomerId(this HttpContext httpContext)
        {
            var id = httpContext.User.Identity.Name;

            if (string.IsNullOrEmpty(id))
            {
                return Guid.Empty;
            }

            return new Guid(id);
        }
    }
}
