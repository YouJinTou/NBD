using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBD.DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext context;

        public Repository(DbContext context)
        {
            this.context = context;
        }

        public async Task<T> GetAsync(object id)
        {
            return await this.context.FindAsync<T>(id);
        }

        public async Task AddAsync(T entity)
        {
            this.context.Add(entity);

            await this.context.SaveChangesAsync();
        }

        public async Task EditAsync(T entity)
        {
            this.context.Entry(entity).State = EntityState.Modified;

            await this.context.SaveChangesAsync();
        }

        public async Task DeleteManyAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                this.context.Entry(entity).State = EntityState.Deleted;
            }

            await this.context.SaveChangesAsync();
        }

        public IEnumerable<T> Where(Func<T, bool> predicate)
        {
            return this.context.Set<T>().Where(predicate).ToList();
        }
    }
}
