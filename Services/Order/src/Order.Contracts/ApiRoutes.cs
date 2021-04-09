namespace Order.Contracts
{
    public static class ApiRoutes
    {
        public const string Base = "api";

        public static class Order
        {
            public const string BaseRoute = Base + "/order";
            public const string GetById = BaseRoute + "/{id}";
        }
    }
}
