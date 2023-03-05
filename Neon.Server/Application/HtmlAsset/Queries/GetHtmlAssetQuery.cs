using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

namespace Neon.Application;

public record GetHtmlAssetQuery(Guid Id) : IRequest<HtmlAssetDTO>;
public class GetHtmlAssetQueryHandler : IRequestHandler<GetHtmlAssetQuery, HtmlAssetDTO> {

    private readonly IApplicationDbContext _database;
    private readonly IMapper _mapper;

    public GetHtmlAssetQueryHandler(IApplicationDbContext database, IMapper mapper) {
        _database = database;
        _mapper = mapper;
    }

    public async Task<HtmlAssetDTO> Handle(GetHtmlAssetQuery query, CancellationToken cancellationToken) {
        var asset = await _database.HtmlAssetRepository.OneAsync(query.Id, cancellationToken);
        if (asset is null)
            throw new KeyNotFoundException();

        return _mapper.Map<HtmlAssetDTO>(asset);
    }
}