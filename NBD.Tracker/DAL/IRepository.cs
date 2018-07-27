using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NBD.Tracker.DAL
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(object id);

        Task AddAsync(T entity);

        Task EditAsync(T entity);

        IEnumerable<T> Where(Func<T, bool> predicate);
    }
}
