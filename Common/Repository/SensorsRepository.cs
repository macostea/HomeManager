using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Common.Repository
{
    public class SensorsRepository<T>: IRepository<T> where T : EntityBase
    {
        private readonly SensorsContext dbContext;

        public SensorsRepository(SensorsContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> Add(T obj)
        {
            await this.dbContext.Set<T>().AddAsync(obj);
            return await this.dbContext.SaveChangesAsync() != 0;
        }

        public async Task<bool> Delete(T entity)
        {
            this.dbContext.Set<T>().Remove(entity);
            return await this.dbContext.SaveChangesAsync() != 0;
        }

        public async Task<bool> Edit(T entity)
        {
            this.dbContext.Entry(entity).State = EntityState.Modified;
            return await this.dbContext.SaveChangesAsync() != 0;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await this.dbContext.Set<T>().ToArrayAsync();
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate)
        {
            return await this.dbContext.Set<T>()
                       .Where(predicate)
                       .ToArrayAsync();
        }

        public async Task<IEnumerable<T>> GetAll(ISpecification<T> spec)
        {
            var queryableResultWithIncludes = spec.Includes.Aggregate(this.dbContext.Set<T>().AsQueryable(),
                                                                      (current, include) => current.Include(include));
            var secondaryResult = spec.IncludeStrings.Aggregate(queryableResultWithIncludes,
                                                                (current, include) => current.Include(include));

            return spec.Criteria == null ? await secondaryResult.ToArrayAsync() : await secondaryResult.Where(spec.Criteria).ToArrayAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await this.dbContext.Set<T>().FindAsync(id);
        }
    }
}
