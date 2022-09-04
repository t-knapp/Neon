// using System;
// using System.IO;
// using System.Threading;
// using System.Threading.Tasks;
// using MediatR;
// using Neon.Domain;
// 
// namespace Neon.Application
// {
//     public class AddHtmlAssetCommand : IRequestHandler<AddHtmlAssetCommand.Input, HtmlAsset> {
//         public class Input : IRequest<HtmlAsset> {
// 
//             public string Name { get; }
//             public EAssetType Type { get; } = EAssetType.TextHtml;
//             public string Content { get; }
//             public int DisplayTime { get; }
//             public bool IsActive { get; }
//             public int Order { get; }
//             public DateTime? NotBefore { get; set; }
//             public DateTime? NotAfter { get; set; }
// 
//             public Input(string name, int displayTime, bool isActive, int order, string content, DateTime? notBefore, DateTime? notAfter) {
//                 Name = name ?? throw new ArgumentNullException(nameof(name));
//                 DisplayTime = displayTime;
//                 IsActive = isActive;
//                 Order = order;
//                 Content = content ?? throw new ArgumentNullException(nameof(content));
//                 NotBefore = notBefore;
//                 NotAfter = notAfter;
//                 // TODO: Plausibility checks on times
//             }
//         }
// 
//         private readonly IMongoCollection<HtmlAsset> _assetCollection;
// 
//         public AddHtmlAssetCommand(IMongoCollection<HtmlAsset> assetCollection) {
//             _assetCollection = assetCollection;
//         }
// 
//         public async Task<HtmlAsset> Handle(Input request, CancellationToken cancellationToken) {
//             var asset = new HtmlAsset(request.Name, request.DisplayTime, request.IsActive, request.Order, request.Content, request.NotBefore, request.NotAfter);
//             await _assetCollection.InsertOneAsync(asset, null, cancellationToken);
//             return asset;
//         }
//     }
// }
