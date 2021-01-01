using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MongoDB.Driver;
using Neon.Server.Models;

namespace Neon.Server.Commands {
    public class AssetsListQuery : IRequestHandler<AssetsListQuery.Input, IEnumerable<Asset>> {
        public class Input : IRequest<IEnumerable<Asset>> {
            public Input() {}
        }

        private readonly IMapper _mapper;
        private readonly IMongoCollection<ImageAsset> _imageAssetCollection;
        private readonly IMongoCollection<HtmlAsset> _htmlAssetCollection;

        public AssetsListQuery(
            IMapper mapper,
            IMongoCollection<ImageAsset> imageCollection,
            IMongoCollection<HtmlAsset> htmlCollection
        ) {
            _mapper = mapper;
            _imageAssetCollection = imageCollection;
            _htmlAssetCollection = htmlCollection;
        }

        public Task<IEnumerable<Asset>> Handle(AssetsListQuery.Input input, CancellationToken cancellationToken) {
            var images = _mapper.Map<IEnumerable<ImageAsset>, IEnumerable<Asset>>(_imageAssetCollection.AsQueryable().AsEnumerable());
            var htmls = _mapper.Map<IEnumerable<HtmlAsset>, IEnumerable<Asset>>(_htmlAssetCollection.AsQueryable().AsEnumerable());
            return Task.FromResult(Enumerable.Concat(images, htmls));
        }
    }
}