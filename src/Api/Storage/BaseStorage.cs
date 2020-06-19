using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Api.Models;

using Microsoft.Extensions.Logging;

namespace Api.Storage
{
    /// <summary>
    /// Abstraction for Storages within the API.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseStorage<T> : IStorage<T>
        where T : class, IUpdatableFrom<T>
    {
        protected BaseStorage(ILogger<InMemoryStorage<T>> logger = null)
        {
            Logger = logger;
        }

        protected ILogger<InMemoryStorage<T>> Logger { get; }
        public abstract Task<T> InsertAsync(T newInstance, CancellationToken cancellationToken = default);
        public abstract Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> predicate = null, CancellationToken cancellationToken = default);
        public abstract Task<T> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        public abstract Task RemoveAsync(T instance, CancellationToken cancellationToken = default);
        public abstract Task UpdateAsync(Expression<Func<T, bool>> predicate, T updated, CancellationToken cancellationToken = default);
    }
}