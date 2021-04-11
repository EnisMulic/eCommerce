using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Order.Api.Extensions
{
    public static class HttpContextExtension
    {
        public static Guid GetBuyerId(this HttpContext httpContext)
        {
            if (httpContext.User == null)
            {
                return Guid.Empty;
            }

            return new Guid(httpContext.User.Claims.Single(x => x.Type == "id").Value);
        }
    }
}
