using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Contracts.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel;
        public Task<int> Complete();

        public IVideoRepository VideoRepository {get; }

        public IStreamerRepository StreamerRepository { get; }
    }
}
