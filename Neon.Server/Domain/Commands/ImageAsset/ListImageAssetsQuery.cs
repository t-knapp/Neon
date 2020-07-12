using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using Neon.Server.Models;

namespace Neon.Server.Commands {
    public class ListImageAssetsQuery : IRequestHandler<ListImageAssetsQuery.Input, IEnumerable<ImageAsset>> {
        public class Input : IRequest<IEnumerable<ImageAsset>> {
            public Input() { } // TODO: Queryable ...
        }

        private readonly IMongoCollection<ImageAsset> _assetCollection;

        public ListImageAssetsQuery(IMongoCollection<ImageAsset> assetCollection)
            => _assetCollection = assetCollection;

        public Task<IEnumerable<ImageAsset>> Handle(ListImageAssetsQuery.Input input, CancellationToken cancellationToken) {
            return Task.FromResult(_assetCollection.AsQueryable().AsEnumerable());
        }
    }
}