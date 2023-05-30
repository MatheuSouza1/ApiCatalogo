namespace ApiCatalogo.DTOs
{
    public class ProductsDTO
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
    }
}
