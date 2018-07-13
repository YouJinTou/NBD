using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBD.Tracker.DAL
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(Guid id);

        Task AddAsync(T entity);
    }
}
