using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace NBD.Tracker.DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext context;

        public Repository(DbContext context)
        {
            this.context = context;
        }

        public async Task<T> GetAsync(Guid id)
        {
            return await this.context.FindAsync<T>(id);
        }

        public async Task AddAsync(T entity)
        {
            this.context.Add(entity);

            await this.context.SaveChangesAsync();
        }
    }
}
