using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.JsonPatch;
using MediatR;
using AutoMapper;
using MongoDB.Driver;
using Neon.Server.Models;
using Neon.Server.Configuration;
using Neon.Server.Controllers;

namespace Neon.Server.Commands
{
    public class UpdateImageAssetCommand : IRequestHandler<UpdateImageAssetCommand.Input, ImageAsset> {
        public class Input : IRequest<ImageAsset> {
            public string Id { get; }
            public JsonPatchDocument<ImageAsset> Patch { get; }

            public Input(string id, JsonPatchDocument<ImageAsset> patch) {
                Id = !string.IsNullOrEmpty(id) ? id : throw new ArgumentNullException(nameof(id));
                Patch = patch ?? throw new ArgumentNullException(nameof(patch));
            }
        }

        private readonly IMongoCollection<ImageAsset> _assetCollection;
        private readonly ImageOptions _imageOptions;

        public UpdateImageAssetCommand(IMongoCollection<ImageAsset> assetCollection, IOptions<ImageOptions> imageOptions) {
            _assetCollection = assetCollection;
            _imageOptions = imageOptions.Value;
        }

        public async Task<ImageAsset> Handle(Input request, CancellationToken cancellationToken) {
            var existing = await _assetCollection.Find(_ => _.ID == request.Id).FirstAsync();

            request.Patch.ApplyTo(existing);

            var filterBuilder = Builders<ImageAsset>.Filter;
            var filter = filterBuilder.Eq(a => a.ID, request.Id);
            var updateBuilder = Builders<ImageAsset>.Update;
            var updates = new List<UpdateDefinition<ImageAsset>>();
            updates.Add(updateBuilder.Set(a => a.Name, existing.Name));
            updates.Add(updateBuilder.Set(a => a.DisplayTime, existing.DisplayTime));
            updates.Add(updateBuilder.Set(a => a.IsActive, existing.IsActive));
            updates.Add(updateBuilder.Set(a => a.Order, existing.Order));
            updates.Add(updateBuilder.Set(a => a.NotBefore, existing.NotBefore));
            updates.Add(updateBuilder.Set(a => a.NotAfter, existing.NotAfter));

            return await _assetCollection.FindOneAndUpdateAsync<ImageAsset>(filter, updateBuilder.Combine(updates), null, cancellationToken);
        }
    }
}
