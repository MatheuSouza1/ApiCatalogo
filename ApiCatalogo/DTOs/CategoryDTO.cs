using ApiCatalogo.Entities;

namespace ApiCatalogo.DTOs
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<ProductsDTO> Products { get; set; }
    }
}
