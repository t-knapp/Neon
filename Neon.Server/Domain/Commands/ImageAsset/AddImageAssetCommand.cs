using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using Neon.Server.Models;

namespace Neon.Server.Commands
{
    public class AddImageAssetCommand : IRequestHandler<AddImageAssetCommand.Input, ImageAsset> {
        public class Input : IRequest<ImageAsset> {

            public string Name { get; }
            public EAssetType Type { get; } = EAssetType.Image;
            public string ContextName { get; }
            public Stream Content { get; }
            public string ContentType { get; }
            public int DisplayTime { get; }

            public Input(string name, string contextName, int displayTime, Stream content, string contentType) {
                Name = name ?? throw new ArgumentNullException(nameof(name));
                ContextName = contextName ?? throw new ArgumentNullException(nameof(contextName));
                DisplayTime = displayTime;
                Content = content ?? throw new ArgumentNullException(nameof(content));
                ContentType = contentType;
            }
        }

        private readonly IMongoCollection<ImageAsset> _assetCollection;

        public AddImageAssetCommand(IMongoCollection<ImageAsset> assetCollection)
            => _assetCollection = assetCollection;

        public async Task<ImageAsset> Handle(Input request, CancellationToken cancellationToken) {
            var asset = new ImageAsset(request.Name, new AssetContext(request.ContextName), request.DisplayTime, request.ContentType);
            await _assetCollection.InsertOneAsync(asset, null, cancellationToken);
            await asset.Data.UploadAsync(request.Content);
            return asset;
        }
    }
}
