using ApiCatalogo.Context;

namespace ApiCatalogo.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ICategoryRepository categoryRepository;
        private IProdutoRepository produtoRepository;
        private ApiCatalogoContext _context;

        public UnitOfWork(ApiCatalogoContext apiCatalogoContext)
        {
            _context = apiCatalogoContext;
        }

        public IProdutoRepository ProdutoRepository
        {
            get
            {
                return produtoRepository = produtoRepository ?? new ProductRepository(_context);
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                return categoryRepository = categoryRepository ?? new CategoryRepository(_context);
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
