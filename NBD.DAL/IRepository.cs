using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NBD.DAL
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(object id);

        Task AddAsync(T entity);

        Task EditAsync(T entity);

        Task DeleteManyAsync(IEnumerable<T> entities);

        IEnumerable<T> Where(Func<T, bool> predicate);
    }
}
