namespace Basket.Contracts.Responses
{
    public class Response<T> : IResponse
    {
        public Response() { }

        public Response(T response)
        {
            Data = response;
        }
        public T Data { get; set; }
    }
}
