using System.Threading.Tasks;

namespace NBD.Tracker.DAL
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(object id);

        Task AddAsync(T entity);
    }
}
