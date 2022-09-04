// using System;
// using System.Collections.Generic;
// using System.Threading;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.JsonPatch;
// using MediatR;
// using Neon.Domain;
// 
// namespace Neon.Application
// {
//     public class UpdateHtmlAssetCommand : IRequestHandler<UpdateHtmlAssetCommand.Input, HtmlAsset> {
//         public class Input : IRequest<HtmlAsset> {
//             public string Id { get; }
//             public JsonPatchDocument<HtmlAsset> Patch { get; }
// 
//             public Input(string id, JsonPatchDocument<HtmlAsset> patch) {
//                 Id = !string.IsNullOrEmpty(id) ? id : throw new ArgumentNullException(nameof(id));
//                 Patch = patch ?? throw new ArgumentNullException(nameof(patch));
//             }
//         }
// 
//         private readonly IMongoCollection<HtmlAsset> _assetCollection;
// 
//         public UpdateHtmlAssetCommand(IMongoCollection<HtmlAsset> assetCollection) {
//             _assetCollection = assetCollection;
//         }
// 
//         public async Task<HtmlAsset> Handle(Input request, CancellationToken cancellationToken) {
//             var existing = await _assetCollection.Find(_ => _.ID == request.Id).FirstAsync();
// 
//             request.Patch.ApplyTo(existing);
// 
//             var filterBuilder = Builders<HtmlAsset>.Filter;
//             var filter = filterBuilder.Eq(a => a.ID, request.Id);
//             var updateBuilder = Builders<HtmlAsset>.Update;
//             var updates = new List<UpdateDefinition<HtmlAsset>>();
//             updates.Add(updateBuilder.Set(a => a.Name, existing.Name));
//             updates.Add(updateBuilder.Set(a => a.DisplayTime, existing.DisplayTime));
//             updates.Add(updateBuilder.Set(a => a.Content, existing.Content));
//             updates.Add(updateBuilder.Set(a => a.IsActive, existing.IsActive));
//             updates.Add(updateBuilder.Set(a => a.Order, existing.Order));
//             updates.Add(updateBuilder.Set(a => a.NotBefore, existing.NotBefore));
//             updates.Add(updateBuilder.Set(a => a.NotAfter, existing.NotAfter));
// 
//             return await _assetCollection.FindOneAndUpdateAsync<HtmlAsset>(filter, updateBuilder.Combine(updates), null, cancellationToken);
//         }
//     }
// }
