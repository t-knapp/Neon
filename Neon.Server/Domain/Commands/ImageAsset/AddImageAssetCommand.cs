using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MediatR;
using MongoDB.Driver;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using Neon.Server.Models;
using Neon.Server.Configuration;

namespace Neon.Server.Commands
{
    public class AddImageAssetCommand : IRequestHandler<AddImageAssetCommand.Input, ImageAsset> {
        public class Input : IRequest<ImageAsset> {

            public string Name { get; }
            public EAssetType Type { get; } = EAssetType.Image;
            public string ContextName { get; }
            public Stream Content { get; }
            public string ContentType { get; }
            public int DisplayTime { get; }

            public Input(string name, string contextName, int displayTime, Stream content, string contentType) {
                Name = name ?? throw new ArgumentNullException(nameof(name));
                ContextName = contextName ?? throw new ArgumentNullException(nameof(contextName));
                DisplayTime = displayTime;
                Content = content ?? throw new ArgumentNullException(nameof(content));
                ContentType = contentType;
            }
        }

        private readonly IMongoCollection<ImageAsset> _assetCollection;
        private readonly ImageOptions _imageOptions;

        public AddImageAssetCommand(IMongoCollection<ImageAsset> assetCollection, IOptions<ImageOptions> imageOptions) {
            _assetCollection = assetCollection;
            _imageOptions = imageOptions.Value;
        }

        public async Task<ImageAsset> Handle(Input request, CancellationToken cancellationToken) {
            var asset = new ImageAsset(request.Name, new AssetContext(request.ContextName), request.DisplayTime, request.ContentType);
            await _assetCollection.InsertOneAsync(asset, null, cancellationToken);
            await asset.Data.UploadAsync(Resize(request.Content));
            request.Content.Close();
            return asset;
        }

        private Stream Resize(Stream raw) {
            try {
                using (Image image = Image.Load(raw, out IImageFormat mime))
                {
                    int destWidth = _imageOptions.MaxWidth;
                    int destHeight = _imageOptions.MaxHeight;
                    double xScaleFactor = image.Width / (double)_imageOptions.MaxWidth;
                    double yScaleFactor = image.Height / (double)_imageOptions.MaxHeight;
                    if (xScaleFactor >= yScaleFactor)
                        destHeight = 0;
                    else
                        destWidth = 0;

                    image.Mutate(x => x.Resize(destWidth, destHeight));

                    var output = new MemoryStream();
                    image.Save(output, mime);
                    return output;
                }
            } catch (Exception) {
                return raw;
            }
        }
    }
}
