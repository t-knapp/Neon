using System.Net.Mime;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using MediatR;
using Neon.Domain;

namespace Neon.Application;

public record GetImageAssetContentQuery(Guid id) : IRequest<ImageFile>;
public class GetImageAssetContentQueryHandler : IRequestHandler<GetImageAssetContentQuery, ImageFile> {

    private readonly IApplicationDbContext _database;

    public GetImageAssetContentQueryHandler(IApplicationDbContext database)
        => _database = database;

    public async Task<ImageFile> Handle(GetImageAssetContentQuery query, CancellationToken cancellationToken) {
        var asset = await _database.ImageAssetRepository.OneAsync(query.id);
        if (asset is null)
            throw new Exception($"Asset with id {query.id} not found");
        
        return new ImageFile(query.id.ToString(), "image/jpeg", asset.ImageContent); // TODO: Content-Type from Entity
    }
}