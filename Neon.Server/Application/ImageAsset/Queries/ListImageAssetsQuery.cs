using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Neon.Domain;

namespace Neon.Application;

public record GetImageAssetInfosQuery() : IRequest<IEnumerable<ImageAssetInfoDTO>>;
public class ListImageAssetsQueryHandler : IRequestHandler<GetImageAssetInfosQuery, IEnumerable<ImageAssetInfoDTO>> {

    private readonly IApplicationDbContext _database;
    private readonly IMapper _mapper;

    public ListImageAssetsQueryHandler(IApplicationDbContext database, IMapper mapper) {
        _database = database;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ImageAssetInfoDTO>> Handle(GetImageAssetInfosQuery query, CancellationToken cancellationToken) {
        return _mapper.Map<IEnumerable<ImageAssetInfoDTO>>(await _database.ImageAssetRepository.AllAsync(cancellationToken));
    }
}
