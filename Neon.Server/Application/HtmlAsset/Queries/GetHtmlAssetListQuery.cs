using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;

namespace Neon.Application;

public record GetHtmlAssetListQuery() : IRequest<IEnumerable<HtmlAssetInfoDTO>>;
public class GetHtmlAssetListQueryHandler : IRequestHandler<GetHtmlAssetListQuery, IEnumerable<HtmlAssetInfoDTO>> {

    private readonly IApplicationDbContext _database;
    private readonly IMapper _mapper;

    public GetHtmlAssetListQueryHandler(IApplicationDbContext database, IMapper mapper) {
        _database = database;
        _mapper = mapper;
    }

    public async Task<IEnumerable<HtmlAssetInfoDTO>> Handle(GetHtmlAssetListQuery query, CancellationToken cancellationToken) {
        return _mapper.Map<IEnumerable<HtmlAssetInfoDTO>>(await _database.HtmlAssetRepository.AllAsync(cancellationToken));
    }
}