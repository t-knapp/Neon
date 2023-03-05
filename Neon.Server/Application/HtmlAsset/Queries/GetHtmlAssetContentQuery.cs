using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Neon.Application;

public record GetHtmlAssetContentQuery(Guid id) : IRequest<string>; // Or Type?
public class GetHtmlAssetContentQueryHandler : IRequestHandler<GetHtmlAssetContentQuery, string> {

    private readonly IApplicationDbContext _database;

    public GetHtmlAssetContentQueryHandler(IApplicationDbContext database) {
        _database = database;
    }

    public async Task<string> Handle(GetHtmlAssetContentQuery query, CancellationToken cancellationToken) {
        var asset = await _database.HtmlAssetRepository.OneAsync(query.id);
        if (asset is null)
            throw new Exception($"Asset with id {query.id} not found");
        
        return asset.HtmlContent;
    }
}