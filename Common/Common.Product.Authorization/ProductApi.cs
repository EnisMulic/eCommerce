namespace Common.Product.Authorization
{
    public static class ProductApi
    {
        public static class Resource
        {
            public const string Name = "product-api";
            public const string DisplayName = "Product API";
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

            public static class Delete
            {
                public const string Name = Resource.Name + ".delete";
                public const string DisplayName = "Delete your data.";
            }
        }
    }
}
