using System.Threading.Tasks;

namespace IfCommerce.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
