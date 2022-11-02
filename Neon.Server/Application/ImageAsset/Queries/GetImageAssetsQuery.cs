using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Neon.Domain;

namespace Neon.Application;

public record GetImageAssetsQuery(Guid Id) : IRequest<ImageAssetDTO>;
public class GetImageAssetsQueryHandler : IRequestHandler<GetImageAssetsQuery, ImageAssetDTO> {

    private readonly IApplicationDbContext _database;
    private readonly IMapper _mapper;

    public GetImageAssetsQueryHandler(IApplicationDbContext database, IMapper mapper) {
        _database = database;
        _mapper = mapper;
    }

    public async Task<ImageAssetDTO> Handle(GetImageAssetsQuery query, CancellationToken cancellationToken) {
        return _mapper.Map<ImageAssetDTO>(await _database.ImageAssetRepository.OneAsync(query.Id, cancellationToken));
    }
}
