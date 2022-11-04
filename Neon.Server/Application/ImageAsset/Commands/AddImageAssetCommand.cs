using MediatR;
using AutoMapper;
using Neon.Domain;

namespace Neon.Application;

public class AddImageAssetCommand : IRequest<ImageAssetDTO> {

    public string Name { get; }
    public EAssetType Type { get; } = EAssetType.Image;
    public Stream Content { get; }
    public string ContentType { get; }
    public int DisplayTime { get; }
    public bool IsActive { get; }
    public int Order { get; }
    public DateTime? NotBefore { get; set; }
    public DateTime? NotAfter { get; set; }

    public AddImageAssetCommand(string name, int displayTime, bool isActive, int order, Stream content, string contentType, DateTime? notBefore, DateTime? notAfter) {
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
public class AddImageAssetCommandHandler : IRequestHandler<AddImageAssetCommand, ImageAssetDTO> {
    
    private readonly IApplicationDbContext _database;
    private readonly IMapper _mapper;
    private readonly IImageResizeService _imageResizeService;
    // private readonly ImageOptions _imageOptions;

    public AddImageAssetCommandHandler(IApplicationDbContext database, IMapper mapper, IImageResizeService imageResizeService) {
        _database = database;
        _mapper = mapper;
        _imageResizeService = imageResizeService;
    }

    public async Task<ImageAssetDTO> Handle(AddImageAssetCommand command, CancellationToken cancellationToken) {
        var resized = resize(command.Content, new System.Drawing.Size(1920, 1080));
        command.Content.Seek(0, SeekOrigin.Begin);
        var thumbnail = resize(command.Content, new System.Drawing.Size(160, 90));

        var asset = new ImageAsset(command.Name, command.DisplayTime, command.IsActive, command.Order, command.ContentType, resized, thumbnail, command.NotBefore, command.NotAfter);
        await _database.ImageAssetRepository.AddAsync(asset, cancellationToken);
        await _database.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ImageAssetDTO>(asset);
    }

    private byte[] resize(Stream input, System.Drawing.Size size) {
        var resizedStream = _imageResizeService.Resize(input, size);
        resizedStream.Seek(0, SeekOrigin.Begin);
        var ms = new MemoryStream();
        resizedStream.CopyTo(ms);
        return ms.ToArray();
    }
}
