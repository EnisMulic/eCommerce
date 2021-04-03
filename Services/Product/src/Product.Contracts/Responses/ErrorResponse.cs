using System.Collections.Generic;

namespace Product.Contracts.Responses
{
    public class ErrorResponse
    {
        public List<ErrorModel> Errors { get; set; } = new();
    }
}
