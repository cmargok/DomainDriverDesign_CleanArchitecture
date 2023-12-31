﻿using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace CleanArchitecture.Infrastructure.Repositories
{

    public class BaseRepository<T> : IAsyncRepository<T> where T : BaseDomainModel
    {
        private readonly StreamerDbContext _context;
        public BaseRepository(StreamerDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

      

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

     

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null!, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!, string includeString = null!, bool disableTracking = true)
        {
            
            IQueryable<T> query = _context.Set<T>();

            if(disableTracking)
                query = query.AsNoTracking(); 

            if(!string.IsNullOrWhiteSpace(includeString)) 
                query = query.Include(includeString);

            if(predicate is not null)
                query = query.Where(predicate);

            if(orderBy is not null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();


        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null!, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!, List<Expression<Func<T, object>>> includes = null!, bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();

            if (disableTracking)
                query = query.AsNoTracking();

            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate is not null)
                query = query.Where(predicate);

            if (orderBy is not null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Set<T>().FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;

        }
        public void AddEntity(T entity)
        {
            _context.Set<T>().Add(entity);
        }
        public void DeleteEntity(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public void UpdateEntity(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
