using ApiCatalogo.Context;
using ApiCatalogo.Entities;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repository
{
    public class ProductRepository : Repository<Product>, IProdutoRepository
    {
        public ProductRepository(ApiCatalogoContext context) : base(context)
        {
        }

        public PagedList<Product> GetProducts(ProductParameters productParameter)
        {
            //return Get().OrderBy(p => p.Name).Skip((productParameter.pageNumber - 1) * productParameter.pageSize).Take(productParameter.pageSize).ToList();
            return PagedList<Product>.ToPagedList(Get().OrderBy(p => p.Name), productParameter.pageNumber, productParameter.pageSize);
        }

        public IEnumerable<Product> GetProductsPorPreco()
        {
            return Get().OrderBy(c => c.Price).ToList();
        }
    }
}
