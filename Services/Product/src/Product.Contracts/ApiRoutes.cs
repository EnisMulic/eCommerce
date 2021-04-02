namespace Product.Contracts
{
    public static class ApiRoutes
    {
        public const string Base = "api";

        public static class Product
        {
            public const string BaseRoute = Base + "/product";
            public const string Get = BaseRoute;
            public const string GetById = BaseRoute + "/{id}";
            public const string Post = BaseRoute;
            public const string Put = BaseRoute + "/{id}";
            public const string Delete = BaseRoute + "/{id}";
            public const string AddAttribute = BaseRoute + "/{id}/product-attribute";
            public const string PatchAttribute = BaseRoute + "/{id}/product-attribute/{attributeValueId}";
            public const string DeleteAttribute = BaseRoute + "/{id}/product-attribute";
            public const string AddCategories = BaseRoute + "/{id}/category";
        }

        public static class ProductAttributeGroup                              
        {                                                                      
            public const string BaseRoute = Base + "/product-attribute-group"; 
            public const string Get = BaseRoute;                               
            public const string GetById = BaseRoute + "/{id}";                 
            public const string Post = BaseRoute;                              
            public const string Put = BaseRoute + "/{id}";                     
            public const string Delete = BaseRoute + "/{id}";                  
        }

        public static class ProductAttribute
        {
            public const string BaseRoute = Base + "/product-attribute";
            public const string Get = BaseRoute;
            public const string GetById = BaseRoute + "/{id}";
            public const string Post = BaseRoute;
            public const string Put = BaseRoute + "/{id}";
            public const string Delete = BaseRoute + "/{id}";
        }

        public static class Category
        {
            public const string BaseRoute = Base + "/category";
            public const string Get = BaseRoute;
            public const string GetById = BaseRoute + "/{id}";
            public const string Post = BaseRoute;
            public const string Put = BaseRoute + "/{id}";
            public const string Delete = BaseRoute + "/{id}";
        }
        
        public static class ProductOption
        {
            public const string BaseRoute = Base + "/product-option";
            public const string Get = BaseRoute;
            public const string GetById = BaseRoute + "/{id}";
            public const string Post = BaseRoute;
            public const string Put = BaseRoute + "/{id}";
            public const string Delete = BaseRoute + "/{id}";
        }
    }
}
