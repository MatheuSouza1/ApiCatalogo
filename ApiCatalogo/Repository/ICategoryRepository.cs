using ApiCatalogo.Entities;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        PagedList<Category> GetCategories(CategoryParameters categoryParameters);
        IEnumerable<Category> GetAll();
    }
}
