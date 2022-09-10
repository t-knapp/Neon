using MediatR;
using Neon.Domain;

namespace Neon.Application
{
    public class AddImageAssetCommand : IRequestHandler<AddImageAssetCommand.Input, ImageAsset> {
        public class Input : IRequest<ImageAsset> {

            public string Name { get; }
            public EAssetType Type { get; } = EAssetType.Image;
            public Stream Content { get; }
            public string ContentType { get; }
            public int DisplayTime { get; }
            public bool IsActive { get; }
            public int Order { get; }
            public DateTime? NotBefore { get; set; }
            public DateTime? NotAfter { get; set; }

            public Input(string name, int displayTime, bool isActive, int order, Stream content, string contentType, DateTime? notBefore, DateTime? notAfter) {
                Name = name ?? throw new ArgumentNullException(nameof(name));
                DisplayTime = displayTime;
                IsActive = isActive;
                Order = order;
                Content = content ?? throw new ArgumentNullException(nameof(content));
                ContentType = contentType;
                NotBefore = notBefore;
                NotAfter = notAfter;
                // TODO: Plausibility checks on times
            }
        }

        private readonly IApplicationDbContext _database;
        private readonly IImageResizeService _imageResizeService;
        // private readonly ImageOptions _imageOptions;

        public AddImageAssetCommand( IApplicationDbContext database, IImageResizeService imageResizeService ) {
            _database = database;
            _imageResizeService = imageResizeService;
        }

        public async Task<ImageAsset> Handle(Input request, CancellationToken cancellationToken) {
            var resized = _imageResizeService.Resize(request.Content, new System.Drawing.Size(1920, 1080)); // TODO: Use Options

            // TODO: Save byte[] to ImageAsset(Entity)
            var asset = new ImageAsset(request.Name, request.DisplayTime, request.IsActive, request.Order, request.ContentType, request.NotBefore, request.NotAfter);           
            await _database.ImageAssetRepository.AddAsync(asset, cancellationToken);
            await _database.SaveChangesAsync(cancellationToken);
            return asset;
        }
    }
}
