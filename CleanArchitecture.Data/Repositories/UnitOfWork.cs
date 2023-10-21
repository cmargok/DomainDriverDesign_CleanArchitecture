using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Contracts.UnitOfWork;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Infrastructure.Persistence;
using System.Collections;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly StreamerDbContext _dbContext;

        public UnitOfWork(StreamerDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel
        {
            if(_repositories == null) 
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity);

            if(!_repositories.ContainsKey(type) ) 
            {
                var repositoryType = typeof(BaseRepository<>);
                var repossitoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)),_dbContext);

                _repositories.Add(type, repossitoryInstance);
                    
            }

            return (IAsyncRepository<TEntity>)_repositories[type]!;
        }


        public async Task<int> Complete()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

    }
}
