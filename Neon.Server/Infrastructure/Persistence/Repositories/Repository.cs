using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Neon.Application;
using Neon.Domain;

namespace Neon.Infrastructure;

internal class Repository<T> : IRepository<T> where T : Entity
{
    private readonly DbSet<T> _entities;

    public Repository( DbSet<T> entities )
    {
        _entities = entities ?? throw new ArgumentNullException( nameof( entities ) );
    }

    public virtual IQueryable<T> Query() => _entities;

    public virtual async Task<IEnumerable<T>> AllAsync( CancellationToken cancellationToken = default )
        => await Query().ToArrayAsync( cancellationToken );

    public virtual async Task<T> OneAsync( Guid id, CancellationToken cancellationToken = default )
    {
        var entity = await Query().SingleOrDefaultAsync( x => x.Id == id, cancellationToken );
        if( entity is null )
            throw new ArgumentException( $"Entity '{id}' not found." );

        return entity;
    }

    public virtual async Task AddAsync( T entity, CancellationToken cancellationToken = default ) 
        => await _entities.AddAsync( entity ?? throw new ArgumentNullException( nameof( entity ) ), cancellationToken );
    public virtual async Task AddRangeAsync( IEnumerable<T> entities, CancellationToken cancellationToken = default )
        => await _entities.AddRangeAsync( entities ?? throw new ArgumentNullException( nameof( entities ) ), cancellationToken );

    public virtual async Task RemoveAsync( T entity, CancellationToken cancellationToken = default )
        => _entities.Remove( entity ?? throw new ArgumentNullException( nameof( entity ) ) );
    public virtual async Task RemoveRangeAsync( IEnumerable<T> entities, CancellationToken cancellationToken = default )
        => _entities.RemoveRange( entities ?? throw new ArgumentNullException( nameof( entities ) ) );
}