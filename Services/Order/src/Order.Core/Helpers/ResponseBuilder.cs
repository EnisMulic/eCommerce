using AutoMapper;
using Order.Contracts.Requests;
using Order.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Order.Core.Helpers
{
    public class ResponseBuilder<T> : IResponseBuilder<T>
    {
        private readonly IMapper _mapper;

        public ResponseBuilder(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Response<TResponse> Create<TResponse>(T data)
        {
            var response = _mapper.Map<TResponse>(data);
            return new Response<TResponse>(response);
        }

        public Response<List<TResponse>> Create<TResponse>(List<T> data)
        {
            var response = _mapper.Map<List<TResponse>>(data);
            return new Response<List<TResponse>>(response);
        }

        public PagedResponse<TResponse> Create<TResponse>(List<T> data, int totalCount, PaginationQuery pagination)
        {
            var response = _mapper.Map<List<TResponse>>(data);

            int lastPageNumber = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(totalCount) / pagination.PageSize));

            var nextPage = pagination.PageNumber >= 1 && pagination.PageNumber < lastPageNumber
                ? pagination.PageNumber + 1
                : (int?)null;

            var previousPage = pagination.PageNumber - 1 >= 1
                ? pagination.PageNumber - 1
                : (int?)null;

            var firstPage = 1;

            var lastPage = lastPageNumber;

            return new PagedResponse<TResponse>
            {
                Data = response,
                PageNumber = pagination.PageNumber >= 1 ? pagination.PageNumber : null,
                PageSize = pagination.PageSize >= 1 ? pagination.PageSize : null,
                NextPage = response.Any() ? nextPage : null,
                PreviousPage = previousPage,
                FirstPage = firstPage,
                LastPage = lastPage
            };
        }
    }
}
