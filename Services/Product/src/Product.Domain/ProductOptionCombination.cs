using System;

namespace Product.Domain
{
    public class ProductOptionCombination
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid ProductOptionId { get; set; }
        public ProductOption ProductOption { get; set; }
        public string Value { get; set; }
        public Guid ImageId { get; set; }
        public Image Image { get; set; }
    }
}
