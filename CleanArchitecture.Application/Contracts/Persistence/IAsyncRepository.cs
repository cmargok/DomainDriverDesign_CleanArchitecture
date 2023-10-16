using CleanArchitecture.Domain.Common;
using System.Linq.Expressions;

namespace CleanArchitecture.Application.Contracts.Persistence
{
    public interface IAsyncRepository <T> where T : BaseDomainModel
    {
        public Task<IReadOnlyList<T>> GetAllAsync();

        public Task<IReadOnlyList<T>> GetAsync(Expression<Func<T,bool>> predicate);

        public Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null!,
                                               Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!,
                                               string includeString = null!,
                                               bool disableTracking = true
                                               );

        public Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null!,
                                               Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!,
                                               List<Expression<Func<T,object>>> includes = null!,
                                               bool disableTracking = true
                                               );

        public Task<T> GetByIdAsync(int id);
        public Task<T> AddAsync(T entity);
        public Task<T> UpdateAsync(T entity);
        public Task DeleteAsync(T entity);
    }


}