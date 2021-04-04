using System.Collections.Generic;

namespace Product.Contracts.Responses
{
    public class ErrorResponse : IResponse
    {
        public List<ErrorModel> Errors { get; set; } = new();
    }
}
