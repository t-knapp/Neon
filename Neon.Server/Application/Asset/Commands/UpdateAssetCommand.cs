// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.JsonPatch;
// using AutoMapper;
// using MediatR;
// using Neon.Domain;
// 
// namespace Neon.Application {
//     public class UpdateAssetCommand : IRequestHandler<UpdateAssetCommand.Input, Asset> {
// 
//         public class Input : IRequest<Asset> {
//             public string Id { get; }
//             public JsonPatchDocument<Asset> Patch { get; }
// 
//             public Input(string id, JsonPatchDocument<Asset> patch) {
//                 Id = id;
//                 Patch = patch;
//             }
//         }
// 
//         private readonly IMapper _mapper;
//         private readonly IMongoCollection<ImageAsset> _imageAssetCollection;
//         private readonly IMongoCollection<HtmlAsset> _htmlAssetCollection;
// 
//         public UpdateAssetCommand(
//             IMapper mapper,
//             IMongoCollection<ImageAsset> imageCollection,
//             IMongoCollection<HtmlAsset> htmlCollection
//         ) {
//             _mapper = mapper;
//             _imageAssetCollection = imageCollection;
//             _htmlAssetCollection = htmlCollection;
//         }
// 
//         // TODO: Move to common domain-service
//         private async Task<EAssetType> GetAssetType(string assetId) {
//             ImageAsset imageAsset = await _imageAssetCollection.Find(a => a.ID == assetId).FirstOrDefaultAsync();
//             if(imageAsset != null)
//                 return imageAsset.Type;
// 
//             HtmlAsset htmlAsset = await _htmlAssetCollection.Find(a => a.ID == assetId).FirstOrDefaultAsync();
//             if(htmlAsset != null)
//                 return htmlAsset.Type;
// 
//             throw new NotSupportedException();
//         }
// 
//         // TODO: Move to image asset domain-service
//         private async Task<ImageAsset> UpdateImageAsset(UpdateAssetCommand.Input request, CancellationToken cancellationToken) {
//             var existing = await _imageAssetCollection.Find(_ => _.ID == request.Id).FirstAsync();
// 
//             _mapper.Map<JsonPatchDocument<ImageAsset>>(request.Patch).ApplyTo(existing);
// 
//             var filterBuilder = Builders<ImageAsset>.Filter;
//             var filter = filterBuilder.Eq(a => a.ID, request.Id);
//             var updateBuilder = Builders<ImageAsset>.Update;
//             var updates = new List<UpdateDefinition<ImageAsset>>();
//             updates.Add(updateBuilder.Set(a => a.Name, existing.Name));
//             updates.Add(updateBuilder.Set(a => a.DisplayTime, existing.DisplayTime));
//             updates.Add(updateBuilder.Set(a => a.IsActive, existing.IsActive));
//             updates.Add(updateBuilder.Set(a => a.Order, existing.Order));
//             updates.Add(updateBuilder.Set(a => a.NotBefore, existing.NotBefore));
//             updates.Add(updateBuilder.Set(a => a.NotAfter, existing.NotAfter));
// 
//             return await _imageAssetCollection.FindOneAndUpdateAsync<ImageAsset>(filter, updateBuilder.Combine(updates), null, cancellationToken);
//         }
// 
//         // TODO: Move to image asset domain-service
//         private async Task<HtmlAsset> UpdateHtmlAsset(UpdateAssetCommand.Input request, CancellationToken cancellationToken) {
//             var existing = await _htmlAssetCollection.Find(_ => _.ID == request.Id).FirstAsync();
// 
//             _mapper.Map<JsonPatchDocument<HtmlAsset>>(request.Patch).ApplyTo(existing);
// 
//             var filterBuilder = Builders<HtmlAsset>.Filter;
//             var filter = filterBuilder.Eq(a => a.ID, request.Id);
//             var updateBuilder = Builders<HtmlAsset>.Update;
//             var updates = new List<UpdateDefinition<HtmlAsset>>();
//             updates.Add(updateBuilder.Set(a => a.Name, existing.Name));
//             updates.Add(updateBuilder.Set(a => a.DisplayTime, existing.DisplayTime));
//             updates.Add(updateBuilder.Set(a => a.IsActive, existing.IsActive));
//             updates.Add(updateBuilder.Set(a => a.Order, existing.Order));
//             updates.Add(updateBuilder.Set(a => a.NotBefore, existing.NotBefore));
//             updates.Add(updateBuilder.Set(a => a.NotAfter, existing.NotAfter));
// 
//             return await _htmlAssetCollection.FindOneAndUpdateAsync<HtmlAsset>(filter, updateBuilder.Combine(updates), null, cancellationToken);
//         }
// 
//         public async Task<Asset> Handle(UpdateAssetCommand.Input input, CancellationToken cancellationToken) {
//             return await GetAssetType(input.Id) switch
//             {
//                 EAssetType.Image => _mapper.Map<Asset>(await UpdateImageAsset(input, cancellationToken)),
//                 EAssetType.TextHtml => _mapper.Map<Asset>(await UpdateHtmlAsset(input, cancellationToken)),
//                 _ => throw new NotSupportedException()
//             };
//         }
//     }
// }