using Order.Contracts.Requests;
using Order.Contracts.Responses;
using System.Collections.Generic;

namespace Order.Core.Helpers
{
    public interface IResponseBuilder<T>
    {
        Response<TResponse> Create<TResponse>(T data);
        Response<List<TResponse>> Create<TResponse>(List<T> data);
        PagedResponse<TResponse> Create<TResponse>(List<T> data, int totalCount, PaginationQuery pagination);
    }
}
