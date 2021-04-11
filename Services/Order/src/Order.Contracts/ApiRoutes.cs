namespace Order.Contracts
{
    public static class ApiRoutes
    {
        public const string Base = "api";

        public static class Order
        {
            public const string BaseRoute = Base + "/order";
            public const string Get = BaseRoute;
            public const string GetById = BaseRoute + "/{id}";
            public const string Cancel = BaseRoute + "/{id}/cancel";
            public const string Ship = BaseRoute + "/{id}/ship";
            public const string GetCardTypes = BaseRoute + "/card-types";
        }
    }
}
