using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Neon.Domain;

namespace Neon.Application;

public record AssetListQuery() : IRequest<IEnumerable<Asset>>;
public class AssetsListQueryHandler : IRequestHandler<AssetListQuery, IEnumerable<Asset>> {

    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _database;

    public AssetsListQueryHandler(IMapper mapper, IApplicationDbContext database) {
        _mapper = mapper;
        _database = database;
    }

    public Task<IEnumerable<Asset>> Handle(AssetListQuery input, CancellationToken cancellationToken) {
        return Task.FromResult( _database.AssetRepository.Query().OrderBy( a => a.Order ).AsEnumerable() );
    }
}