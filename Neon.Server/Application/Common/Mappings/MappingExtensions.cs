using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore; // TODO: Allowed b.c. of onion?

namespace Neon.Application;

public static class MappingExtensions
{
    public static Task<List<TDestination>> ProjectToListAsync<TDestination>( this IQueryable queryable, IConfigurationProvider configuration )
        => queryable.ProjectTo<TDestination>( configuration ).ToListAsync();
}