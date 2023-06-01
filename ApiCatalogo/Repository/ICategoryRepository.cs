using ApiCatalogo.Entities;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<PagedList<Category>> GetCategories(CategoryParameters categoryParameters);
        Task<IEnumerable<Category>> GetAll();
    }
}
