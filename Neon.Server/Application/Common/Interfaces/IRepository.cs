using System.Linq;
using Neon.Domain;

namespace Neon.Application;

public interface IRepository<T> where T : Entity {
    IQueryable<T> Query();

    Task<IEnumerable<T>> AllAsync(CancellationToken cancellationToken = default);
    Task<T> OneAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    Task RemoveAsync(T entity, CancellationToken cancellationToken = default);
}