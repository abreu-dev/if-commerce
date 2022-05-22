namespace IfCommerce.Core.Data
{
    public interface IContext : IUnitOfWork
    {
        bool IsAvailable();
    }
}
