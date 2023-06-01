using ApiCatalogo.Context;
using ApiCatalogo.Entities;
using ApiCatalogo.Pagination;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repository
{
    public class ProductRepository : Repository<Product>, IProdutoRepository
    {
        public ProductRepository(ApiCatalogoContext context) : base(context)
        {
        }

        public async Task<PagedList<Product>> GetProducts(ProductParameters productParameter)
        {
            //return Get().OrderBy(p => p.Name).Skip((productParameter.pageNumber - 1) * productParameter.pageSize).Take(productParameter.pageSize).ToList();
            return await PagedList<Product>.ToPagedList(Get().OrderBy(p => p.Name), productParameter.pageNumber, productParameter.pageSize);
        }

        public async Task<IEnumerable<Product>> GetProductsPorPreco()
        {
            return await Get().OrderBy(c => c.Price).ToListAsync();
        }
    }
}
