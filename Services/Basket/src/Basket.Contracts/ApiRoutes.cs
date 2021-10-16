namespace Basket.Contracts
{
    public static class ApiRoutes
    {
        public const string Base = "api";
        public static class Basket
        {
            public const string BaseRoute = Base + "/basket";
            public const string Get = BaseRoute;
            public const string Add = BaseRoute;
            public const string Update = BaseRoute;
            public const string Delete = BaseRoute;
            public const string Checkout = BaseRoute + "/checkout";
        }
    }
}
