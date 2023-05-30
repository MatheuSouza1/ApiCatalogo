using ApiCatalogo.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiCatalogo.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [StringLength(80)]
        [PrimeiraLetraAttribute]
        public string? Name { get; set; }
        [Required]
        [StringLength(300)]
        public string? Description { get; set; }
        [Required]
        [Column(TypeName ="decimal(10,2)")]
        public float Price { get; set; }
        [Required]
        [StringLength(300)]
        public string? ImageUrl { get; set; }
        public float Quantity { get; set; } //quantidade no estoque
        public DateTime DateRegister { get; set; }

        [JsonIgnore]
        public Category? Category { get; set; }
        public int CategoryId { get; set; }
    }
}
