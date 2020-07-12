using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using MediatR;
using MongoDB.Driver;
using Neon.Server.Models;

namespace Neon.Server.Commands {
    public class AssetImageQuery : IRequestHandler<AssetImageQuery.Input, Tuple<ImageAsset, Stream>> {
        public class Input : IRequest<Tuple<ImageAsset, Stream>> {
            public string Id { get; set; }
            public bool ShouldIncludeData { get; set; }

            public Input(string id, bool shouldIncludeData = false)
                => (Id, ShouldIncludeData) = (id, shouldIncludeData);
        }

        private readonly IMongoCollection<ImageAsset> _assetCollection;

        public AssetImageQuery(IMongoCollection<ImageAsset> assetCollection)
            => _assetCollection = assetCollection;

        public async Task<Tuple<ImageAsset, Stream>> Handle(AssetImageQuery.Input input, CancellationToken cancellationToken) {
            var asset = await _assetCollection
                .Find(a => a.ID == input.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if(asset is null)
                throw new ArgumentException($"Cannot find asset with id {input.Id}");

            Stream stream = null;
            if(input.ShouldIncludeData) {
                stream = new MemoryStream();
                await asset.Data.DownloadAsync(stream);
                stream.Seek(0, SeekOrigin.Begin);
            }

            return new Tuple<ImageAsset, Stream>(asset, stream);
        }
    }
}