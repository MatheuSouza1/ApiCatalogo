using ApiCatalogo.Context;
using ApiCatalogo.Entities;
using ApiCatalogo.Pagination;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApiCatalogoContext Context) : base(Context)
        {
        }

        public async Task<PagedList<Category>> GetCategories(CategoryParameters categoryParameters)
        {
            return await PagedList<Category>.ToPagedList(Get().OrderBy(c => c.Name), categoryParameters.pageNumber, categoryParameters.pageSize);
        }

        //retorna as categorias e os seus produtos
        public async Task<IEnumerable<Category>> GetAll()
        {
            return await Get().Include(x => x.Products).ToListAsync();
        }
    }
}
