using System.Collections.Generic;

namespace Order.Contracts.Responses
{
    public class ErrorResponse : IResponse
    {
        public List<ErrorModel> Errors { get; set; } = new();
    }
}
