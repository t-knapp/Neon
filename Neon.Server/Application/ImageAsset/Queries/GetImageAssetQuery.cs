using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

namespace Neon.Application;

public record GetImageAssetQuery(Guid Id) : IRequest<ImageAssetDTO>;
public class GetImageAssetQueryHandler : IRequestHandler<GetImageAssetQuery, ImageAssetDTO> {

    private readonly IApplicationDbContext _database;
    private readonly IMapper _mapper;

    public GetImageAssetQueryHandler(IApplicationDbContext database, IMapper mapper) {
        _database = database;
        _mapper = mapper;
    }

    public async Task<ImageAssetDTO> Handle(GetImageAssetQuery query, CancellationToken cancellationToken) {
        return _mapper.Map<ImageAssetDTO>(await _database.ImageAssetRepository.OneAsync(query.Id, cancellationToken));
    }
}
