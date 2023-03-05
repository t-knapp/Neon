using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Neon.Domain;

namespace Neon.Application;

public class AddHttpAssetCommand : IRequest<HtmlAsset> {

    public string Name { get; }
    public string HtmlContent { get; }
    public int DisplayTime { get; }
    public bool IsActive { get; }
    public int Order { get; }
    public DateTime? NotBefore { get; set; }
    public DateTime? NotAfter { get; set; }

    public AddHttpAssetCommand(string name, int displayTime, bool isActive, int order, string htmlContent, DateTime? notBefore, DateTime? notAfter) {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        DisplayTime = displayTime;
        IsActive = isActive;
        Order = order;
        HtmlContent = htmlContent ?? throw new ArgumentNullException(nameof(htmlContent));
        NotBefore = notBefore;
        NotAfter = notAfter;
        // TODO: Plausibility checks on times
    }
}

public class AddHtmlAssetCommandHandler : IRequestHandler<AddHttpAssetCommand, HtmlAsset> {

    private readonly IApplicationDbContext _database;

    public AddHtmlAssetCommandHandler(IApplicationDbContext database) {
        _database = database;
    }

    public async Task<HtmlAsset> Handle(AddHttpAssetCommand command, CancellationToken cancellationToken) {
        var asset = new HtmlAsset(command.Name, command.DisplayTime, command.IsActive, command.Order, command.HtmlContent, command.NotBefore, command.NotAfter);
        await _database.HtmlAssetRepository.AddAsync(asset, cancellationToken);
        await _database.SaveChangesAsync(cancellationToken);
        return asset;
    }
}
