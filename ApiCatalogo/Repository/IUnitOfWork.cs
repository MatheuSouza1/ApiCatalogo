namespace ApiCatalogo.Repository
{
    public interface IUnitOfWork
    {
        IProdutoRepository ProdutoRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        void Commit();
    }
}
