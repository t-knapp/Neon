using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using Neon.Server.Models;

namespace Neon.Server.Commands {
    public class ListHtmlAssetsQuery : IRequestHandler<ListHtmlAssetsQuery.Input, IEnumerable<HtmlAsset>> {
        public class Input : IRequest<IEnumerable<HtmlAsset>> {
            public Input() {}
        }

        private readonly IMongoCollection<HtmlAsset> _assetCollection;

        public ListHtmlAssetsQuery(IMongoCollection<HtmlAsset> assetCollection)
            => _assetCollection = assetCollection;

        public Task<IEnumerable<HtmlAsset>> Handle(ListHtmlAssetsQuery.Input input, CancellationToken cancellationToken) {
            return Task.FromResult(_assetCollection.AsQueryable().AsEnumerable());
        }
    }
}