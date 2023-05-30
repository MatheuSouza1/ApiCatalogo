using ApiCatalogo.Entities;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repository
{
    public interface IProdutoRepository : IRepository<Product>
    {
        PagedList<Product> GetProducts(ProductParameters productParameters);
        IEnumerable<Product> GetProductsPorPreco();
    }
}
