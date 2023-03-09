using MediatR;
using AutoMapper;

namespace Neon.Application;

public class UpdateHtmlAssetCommand : IRequest<HtmlAssetDTO> {

    public Guid Id { get; init; }
    public string Name { get; init; }
    public int DisplayTime { get; init; }
    public bool IsActive { get; init; }
    public int Order  { get; init; }
    public DateTime? NotBefore { get; init; }
    public DateTime? NotAfter { get; init; }
    public string Content { get; init; }

    public UpdateHtmlAssetCommand(Guid id, string name, int displayTime, bool isActive, int order, string content, DateTime? notBefore, DateTime? notAfter) {
        Id = id;
        Name = name ?? throw new ArgumentException(nameof(name));
        DisplayTime = (displayTime > 0) ? displayTime : throw new ArgumentException(nameof(displayTime));
        IsActive = isActive;
        Content = content ?? throw new ArgumentException(nameof(content));
        NotBefore = notBefore;
        NotAfter = notAfter;
    }

}
public class UpdateHtmlAssetCommandHandler : IRequestHandler<UpdateHtmlAssetCommand, HtmlAssetDTO> {

    private readonly IApplicationDbContext _database;
    private readonly IMapper _mapper;

    public UpdateHtmlAssetCommandHandler(IApplicationDbContext database, IMapper mapper) {
        _database = database;
        _mapper = mapper;
    }

    public async Task<HtmlAssetDTO> Handle(UpdateHtmlAssetCommand command, CancellationToken cancellationToken) {
        var asset = await _database.HtmlAssetRepository.OneAsync(command.Id, cancellationToken);
        if (asset is null)
            throw new KeyNotFoundException();

        asset.Name = command.Name;
        asset.DisplayTime = command.DisplayTime;
        asset.IsActive = command.IsActive;
        asset.Order = command.Order;
        asset.NotBefore = command.NotBefore;
        asset.NotAfter = command.NotAfter;
        asset.HtmlContent = command.Content;
        
        await _database.SaveChangesAsync(cancellationToken);

        return _mapper.Map<HtmlAssetDTO>(asset);
    }
}
