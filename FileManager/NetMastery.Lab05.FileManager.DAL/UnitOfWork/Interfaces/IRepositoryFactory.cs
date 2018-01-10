namespace NetMastery.Lab05.FileManager.DAL.UnitOfWork.Factory
{
    public interface IRepositoryFactory
    {
        TEntity Get<TEntity>(object[] parameters) where TEntity : class;
    }
}
