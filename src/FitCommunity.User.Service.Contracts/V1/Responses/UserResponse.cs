using System;

namespace FitCommunity.User.Service.Contracts.V1.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
