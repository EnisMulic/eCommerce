namespace Common.Order.Authorization
{
    public static class OrderApi
    {
        public static class Resource
        {
            public const string Name = "order-api";
            public const string DisplayName = "Order API";
        }

        public static class Scope
        {
            public static class Read
            {
                public const string Name = Resource.Name + ".read";
                public const string DisplayName = "Read your data.";
            }

            public static class Write
            {
                public const string Name = Resource.Name + ".write";
                public const string DisplayName = "Write your data.";
            }
        }
    }
}
