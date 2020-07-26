using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MediatR;
using MongoDB.Driver;
using Neon.Server.Models;
using Neon.Server.Configuration;

namespace Neon.Server.Commands
{
    public class UpdateImageAssetCommand : IRequestHandler<UpdateImageAssetCommand.Input, ImageAsset> {
        public class Input : IRequest<ImageAsset> {
            public string Id { get; }
            public string Name { get; }
            public int? DisplayTime { get; }
            public bool? IsActive { get; }
            public int? Order { get; }
            public DateTime? NotBefore { get; set; }
            public DateTime? NotAfter { get; set; }

            public Input(string id, string name, int? displayTime, bool? isActive, int? order, DateTime? notBefore, DateTime? notAfter) {
                if (id is null)
                    throw new ArgumentNullException(nameof(id));

                Id = id;
                Name = name;
                DisplayTime = displayTime;
                IsActive = isActive;
                Order = order;
                NotBefore = notBefore;
                NotAfter = notAfter;
                // TODO: Plausibility checks on times
            }
        }

        private readonly IMongoCollection<ImageAsset> _assetCollection;
        private readonly ImageOptions _imageOptions;

        public UpdateImageAssetCommand(IMongoCollection<ImageAsset> assetCollection, IOptions<ImageOptions> imageOptions) {
            _assetCollection = assetCollection;
            _imageOptions = imageOptions.Value;
        }

        public async Task<ImageAsset> Handle(Input request, CancellationToken cancellationToken) {
            var filterBuilder = Builders<ImageAsset>.Filter;
            var filter = filterBuilder.Eq(a => a.ID, request.Id);
            var updateBuilder = Builders<ImageAsset>.Update;
            var updates = new List<UpdateDefinition<ImageAsset>>();
            if (request.Name != null)
                updates.Add(updateBuilder.Set(a => a.Name, request.Name));
            if (request.DisplayTime.HasValue)
                updates.Add(updateBuilder.Set(a => a.DisplayTime, request.DisplayTime.Value));
            if (request.IsActive.HasValue)
                updates.Add(updateBuilder.Set(a => a.IsActive, request.IsActive.Value));
            if (request.Order.HasValue)
                updates.Add(updateBuilder.Set(a => a.Order, request.Order.Value));
            if (request.NotBefore.HasValue)
                updates.Add(updateBuilder.Set(a => a.NotBefore, request.NotBefore.Value));
            if (request.NotAfter.HasValue)
                updates.Add(updateBuilder.Set(a => a.NotAfter, request.NotAfter.Value));

            return await _assetCollection.FindOneAndUpdateAsync<ImageAsset>(filter, updateBuilder.Combine(updates), null, cancellationToken);
        }
    }
}
