using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Api.Models;

namespace Api.Storage
{
    /// <summary>
    /// Describe methods necessary to persist models.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IStorage<T>
        where T : class, IUpdatableFrom<T>
    {
        /// <summary>
        /// Get a single model from the storage.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>an instance or null</returns>
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Inserts a single model into the storage.
        /// </summary>
        /// <param name="newInstance"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>the inserted instance</returns>
        Task<T> InsertAsync(T newInstance, CancellationToken cancellationToken = default);

        /// <summary>
        /// Lists current stored models.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> predicate = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes a model from the storage.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RemoveAsync(T instance, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates a model currently stored.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="updated"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateAsync(Expression<Func<T, bool>> predicate, T updated, CancellationToken cancellationToken = default);
    }
}