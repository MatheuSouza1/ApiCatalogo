using ApiCatalogo.Entities;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repository
{
    public interface IProdutoRepository : IRepository<Product>
    {
        Task<PagedList<Product>> GetProducts(ProductParameters productParameters);
        Task<IEnumerable<Product>> GetProductsPorPreco();
    }
}
