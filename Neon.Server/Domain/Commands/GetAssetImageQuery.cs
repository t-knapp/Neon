using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using MediatR;
using MongoDB.Driver;
using Neon.Server.Models;

namespace Neon.Server.Commands {
    public class GetAssetImageQuery : IRequestHandler<GetAssetImageQuery.Input, Stream> {
        public class Input : IRequest<Stream> {
            public string Id { get; set; }

            public Input(string id)
                => Id = id;
        }

        private readonly IMongoCollection<ImageAsset> _assetCollection;

        public GetAssetImageQuery(IMongoCollection<ImageAsset> assetCollection)
            => _assetCollection = assetCollection;

        public async Task<Stream> Handle(GetAssetImageQuery.Input input, CancellationToken cancellationToken) {
            var asset = _assetCollection
                .Find(a => a.ID == input.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if(asset is null)
                throw new ArgumentException($"Cannot find asset with id {input.Id}");

            var stream = new MemoryStream();
            await asset.Result.Data.DownloadAsync(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
    }
}