namespace WebApplication2.ViewModels
{
    public class ProductVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public ProductCategoryDto Category { get; set; }
        public List<ProductColorDto> ProductColors { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class ProductCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class ProductColorDto
    {
        public string ColorName { get; set; }
    }
}
