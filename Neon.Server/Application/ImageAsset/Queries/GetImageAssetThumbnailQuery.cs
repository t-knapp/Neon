using System.Net.Mime;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using MediatR;
using Neon.Domain;

namespace Neon.Application;

public record GetImageAssetThumbnailQuery(Guid id) : IRequest<ImageFile>;
public class GetImageAssetThumbnailHandler : IRequestHandler<GetImageAssetThumbnailQuery, ImageFile> {

    private readonly IApplicationDbContext _database;

    public GetImageAssetThumbnailHandler(IApplicationDbContext database)
        => _database = database;

    public async Task<ImageFile> Handle(GetImageAssetThumbnailQuery query, CancellationToken cancellationToken) {
        var asset = await _database.ImageAssetRepository.OneAsync(query.id);
        if (asset is null)
            throw new Exception($"Asset with id {query.id} not found");
        
        return new ImageFile(query.id.ToString(), "image/jpeg", asset.ThumbnailContent); // TODO: Content-Type from Entity
    }
}