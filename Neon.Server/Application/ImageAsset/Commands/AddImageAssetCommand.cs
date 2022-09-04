// using System;
// using System.IO;
// using System.Threading;
// using System.Threading.Tasks;
// using Microsoft.Extensions.Options;
// using MediatR;
// using SixLabors.ImageSharp;
// using SixLabors.ImageSharp.Formats;
// using SixLabors.ImageSharp.Processing;
// using Neon.Domain;
// 
// namespace Neon.Application
// {
//     public class AddImageAssetCommand : IRequestHandler<AddImageAssetCommand.Input, ImageAsset> {
//         public class Input : IRequest<ImageAsset> {
// 
//             public string Name { get; }
//             public EAssetType Type { get; } = EAssetType.Image;
//             public Stream Content { get; }
//             public string ContentType { get; }
//             public int DisplayTime { get; }
//             public bool IsActive { get; }
//             public int Order { get; }
//             public DateTime? NotBefore { get; set; }
//             public DateTime? NotAfter { get; set; }
// 
//             public Input(string name, int displayTime, bool isActive, int order, Stream content, string contentType, DateTime? notBefore, DateTime? notAfter) {
//                 Name = name ?? throw new ArgumentNullException(nameof(name));
//                 DisplayTime = displayTime;
//                 IsActive = isActive;
//                 Order = order;
//                 Content = content ?? throw new ArgumentNullException(nameof(content));
//                 ContentType = contentType;
//                 NotBefore = notBefore;
//                 NotAfter = notAfter;
//                 // TODO: Plausibility checks on times
//             }
//         }
// 
//         private readonly IMongoCollection<ImageAsset> _assetCollection;
//         private readonly ImageOptions _imageOptions;
// 
//         public AddImageAssetCommand(IMongoCollection<ImageAsset> assetCollection, IOptions<ImageOptions> imageOptions) {
//             _assetCollection = assetCollection;
//             _imageOptions = imageOptions.Value;
//         }
// 
//         public async Task<ImageAsset> Handle(Input request, CancellationToken cancellationToken) {
//             var asset = new ImageAsset(request.Name, request.DisplayTime, request.IsActive, request.Order, request.ContentType, request.NotBefore, request.NotAfter);
//             await _assetCollection.InsertOneAsync(asset, null, cancellationToken);
//             await asset.Data.UploadAsync(Resize(request.Content));
//             request.Content.Close();
//             return asset;
//         }
// 
//         private Stream Resize(Stream raw) {
//             try {
//                 using (Image image = Image.Load(raw, out IImageFormat mime))
//                 {
//                     int destWidth = _imageOptions.MaxWidth;
//                     int destHeight = _imageOptions.MaxHeight;
//                     double xScaleFactor = image.Width / (double)_imageOptions.MaxWidth;
//                     double yScaleFactor = image.Height / (double)_imageOptions.MaxHeight;
//                     if (xScaleFactor >= yScaleFactor)
//                         destHeight = 0;
//                     else
//                         destWidth = 0;
// 
//                     image.Mutate(x => x.Resize(destWidth, destHeight));
// 
//                     var output = new MemoryStream();
//                     image.Save(output, mime);
//                     return output;
//                 }
//             } catch (Exception) {
//                 return raw;
//             }
//         }
//     }
// }
