// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading;
// using System.Threading.Tasks;
// using AutoMapper;
// using MediatR;
// using Neon.Domain;
// 
// namespace Neon.Application {
//     public class DeleteAssetCommand : IRequestHandler<DeleteAssetCommand.Input, Asset> {
// 
//         public class Input : IRequest<Asset> {
//             public string Id { get; }
// 
//             public Input(string id) {
//                 Id = id;
//             }
//         }
// 
//         private readonly IMapper _mapper;
// 
//         public DeleteAssetCommand(
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
//         public async Task<Asset> Handle(DeleteAssetCommand.Input input, CancellationToken cancellationToken) {
//             return await GetAssetType(input.Id) switch 
//             {
//                 EAssetType.Image => _mapper.Map<Asset>(await _imageAssetCollection.FindOneAndDeleteAsync(a => a.ID == input.Id)),
//                 EAssetType.TextHtml => _mapper.Map<Asset>(await _htmlAssetCollection.FindOneAndDeleteAsync(a => a.ID == input.Id)),
//                 _ => throw new NotSupportedException()
//             };
//         }
//     }
// }