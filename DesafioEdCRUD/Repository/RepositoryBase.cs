using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext RepositoryContext { get; set; }

        public RepositoryBase(RepositoryContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }

        public IQueryable<T> FindAll()
        {
            return RepositoryContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return RepositoryContext.Set<T>().Where(expression);
        }       

        public async Task CreateAsync(T entity)
        {
             await RepositoryContext.Set<T>().AddAsync(entity);
             await RepositoryContext.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(T entity)
        {
            RepositoryContext.Entry(entity).State = EntityState.Modified;
            await RepositoryContext.SaveChangesAsync();
            
        }

        public async Task DeleteAsync(T entity)
        {
            RepositoryContext.Set<T>().Remove(entity);
            await RepositoryContext.SaveChangesAsync();
        }        
    }
}