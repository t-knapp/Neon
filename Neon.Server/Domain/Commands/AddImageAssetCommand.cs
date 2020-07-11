using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using MongoDB.Entities;
using Neon.Server.Models;

namespace Neon.Server.Commands
{
    public class AddImageAssetCommand : IRequestHandler<AddImageAssetCommand.Input, ImageAsset> {
        public class Input : IRequest<ImageAsset> {

            public string Name { get; }
            public EAssetType Type { get; } = EAssetType.Image;
            public string ContextName { get; }
            public Stream Content { get;} 
            public TimeSpan DisplayTime { get; }

            public Input(string name, string contextName, TimeSpan displayTime, Stream content) {
                Name = name ?? throw new ArgumentNullException(nameof(name));
                ContextName = contextName ?? throw new ArgumentNullException(nameof(contextName));
                DisplayTime = displayTime;
                Content = content ?? throw new ArgumentNullException(nameof(content));
            }
        }

        private readonly IMongoCollection<ImageAsset> _assetCollection;

        public AddImageAssetCommand(IMongoCollection<ImageAsset> assetCollection)
            => _assetCollection = assetCollection;

        public async Task<ImageAsset> Handle(Input request, CancellationToken cancellationToken) {
            var asset = new ImageAsset(request.Name, new AssetContext(request.ContextName), request.DisplayTime);
            await _assetCollection.InsertOneAsync(asset, null, cancellationToken);
            await asset.Data.UploadAsync(request.Content);
            /*
            var imageId = Guid.NewGuid().ToString();
            var path = Path.Join("data", "images", imageId);
            using (FileStream fs = File.Create(path))
            {
                await request.Content.CopyToAsync(fs);
                await fs.FlushAsync();
                fs.Close();
            }
            */
            return asset;
        }
    }
}
