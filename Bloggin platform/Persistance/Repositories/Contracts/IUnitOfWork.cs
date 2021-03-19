using System.Threading.Tasks;

namespace Bloggin_platform.Persistance.Repositories.Contracts
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
