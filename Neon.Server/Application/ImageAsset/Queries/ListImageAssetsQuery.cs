using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Neon.Domain;

namespace Neon.Application {
    public class ListImageAssetsQuery : IRequestHandler<ListImageAssetsQuery.Input, IEnumerable<ImageAsset>> {
        public class Input : IRequest<IEnumerable<ImageAsset>> {
            public Input() { } // TODO: Queryable ...
        }

        private readonly IApplicationDbContext _database;

        public ListImageAssetsQuery(IApplicationDbContext database)
            => _database = database;

        public Task<IEnumerable<ImageAsset>> Handle(ListImageAssetsQuery.Input input, CancellationToken cancellationToken) {
            return _database.ImageAssetRepository.AllAsync(cancellationToken);
        }
    }
}