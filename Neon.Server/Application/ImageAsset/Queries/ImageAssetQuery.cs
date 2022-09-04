// using System;
// using System.Threading;
// using System.Threading.Tasks;
// using System.IO;
// using MediatR;
// using Neon.Domain;
// 
// namespace Neon.Application {
//     public class ImageAssetQuery : IRequestHandler<ImageAssetQuery.Input, Tuple<ImageAsset, Stream>> {
//         public class Input : IRequest<Tuple<ImageAsset, Stream>> {
//             public string Id { get; set; }
//             public bool ShouldIncludeData { get; set; }
// 
//             public Input(string id, bool shouldIncludeData = false)
//                 => (Id, ShouldIncludeData) = (id, shouldIncludeData);
//         }
// 
//         private readonly IApplicationDbContext _database;
// 
//         public ImageAssetQuery(IApplicationDbContext database)
//             => _database = database;
// 
//         public async Task<Tuple<ImageAsset, Stream>> Handle(ImageAssetQuery.Input input, CancellationToken cancellationToken) {
//             var asset = await _assetCollection
//                 .Find(a => a.ID == input.Id)
//                 .SingleOrDefaultAsync(cancellationToken);
// 
//             if(asset is null)
//                 throw new ArgumentException($"Cannot find asset with id {input.Id}");
// 
//             Stream stream = null;
//             if(input.ShouldIncludeData) {
//                 stream = new MemoryStream();
//                 await asset.Data.DownloadAsync(stream);
//                 stream.Seek(0, SeekOrigin.Begin);
//             }
// 
//             return new Tuple<ImageAsset, Stream>(asset, stream);
//         }
//     }
// }