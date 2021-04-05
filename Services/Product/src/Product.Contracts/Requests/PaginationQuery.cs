namespace Product.Contracts.Requests
{
    public class PaginationQuery
    {
        public PaginationQuery() { }
        public PaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
