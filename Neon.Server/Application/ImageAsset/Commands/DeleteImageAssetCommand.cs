// using System;
// using System.Threading;
// using System.Threading.Tasks;
// using MediatR;
// using Neon.Domain;
// 
// namespace Neon.Application
// {
//     public class DeleteImageAssetCommand : IRequestHandler<DeleteImageAssetCommand.Input, ImageAsset> {
//         
//         public class Input : IRequest<ImageAsset> {
//             public string Id { get; }
//             public Input(string id)
//                 => Id = id;
//         }
// 
//         private readonly IMongoCollection<ImageAsset> _assetCollection;
// 
//         public DeleteImageAssetCommand(IMongoCollection<ImageAsset> assetCollection)
//             => _assetCollection = assetCollection;
// 
//         public Task<ImageAsset> Handle(DeleteImageAssetCommand.Input input, CancellationToken cancellationToken) {
//             return _assetCollection.FindOneAndDeleteAsync(a => a.ID == input.Id);
//         }
//     }
// }