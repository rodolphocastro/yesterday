using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Api.Models;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Api.Storage
{
    /// <summary>
    /// An In-Memory Storage for Models.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>This should only be used for testing and prototyping</remarks>
    public class InMemoryStorage<T> : BaseStorage<T>
        where T : class, IUpdatableFrom<T>
    {
        protected ICollection<T> State { get; } = new HashSet<T>();

        public InMemoryStorage(ILogger<InMemoryStorage<T>> logger) : base(logger)
        {

        }

        public override Task<T> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return Task.FromResult(State.SingleOrDefault(predicate.Compile()));
        }

        public override Task<T> InsertAsync(T newInstance, CancellationToken cancellationToken = default)
        {
            if (newInstance is null)
            {
                throw new ArgumentNullException(nameof(newInstance));
            }

            State.Add(newInstance);
            Logger?.LogTrace("Object {@Instance} added to the State", newInstance);
            return Task.FromResult(newInstance);
        }

        public override Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> predicate = null, CancellationToken cancellationToken = default)
        {
            if (predicate is null)
            {
                predicate = (_) => true;
            }

            return Task.FromResult(State.Where(predicate.Compile()));
        }
        public override Task RemoveAsync(T instance, CancellationToken cancellationToken = default)
        {
            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            State.Remove(instance);
            Logger?.LogTrace("Object {@Instance} removed from the State", instance);
            return Task.CompletedTask;
        }

        public override async Task UpdateAsync(Expression<Func<T, bool>> predicate, T updated, CancellationToken cancellationToken = default)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (updated is null)
            {
                throw new ArgumentNullException(nameof(updated));
            }

            var current = await GetAsync(predicate, cancellationToken);
            Logger?.LogTrace("Object {@Instance} is being updated", current);
            current.ApplyChanges(updated);
            Logger?.LogTrace("Object {@Instance} updated", current);
        }
    }

    /// <summary>
    /// Extensions related to the InMemoryStorage.
    /// </summary>
    public static class InMemoryStorageExtensions
    {
        /// <summary>
        /// Adds the services for an InMemory Storage for objects of type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddStorageFor<T>(this IServiceCollection services)
            where T : class, IUpdatableFrom<T>
        {
            return services.AddSingleton<IStorage<T>, InMemoryStorage<T>>();
        }
    }
}
