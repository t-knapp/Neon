// using System;
// using System.Threading;
// using System.Threading.Tasks;
// using MediatR;
// using Neon.Domain;
// 
// namespace Neon.Application {
//     public class HtmlAssetQuery : IRequestHandler<HtmlAssetQuery.Input, HtmlAsset> {
//         public class Input : IRequest<HtmlAsset> {
//             public string Id { get; set; }
//             public Input(string id) => Id = id;
//         }
// 
//         private readonly IMongoCollection<HtmlAsset> _assetCollection;
// 
//         public HtmlAssetQuery(IMongoCollection<HtmlAsset> assetCollection)
//             => _assetCollection = assetCollection;
// 
//         public async Task<HtmlAsset> Handle(HtmlAssetQuery.Input input, CancellationToken cancellationToken) {
//             var asset = await _assetCollection
//                 .Find(a => a.ID == input.Id)
//                 .SingleOrDefaultAsync(cancellationToken);
// 
//             if(asset is null)
//                 throw new ArgumentException($"Cannot find asset with id {input.Id}");
// 
//             return asset;
//         }
//     }
// }