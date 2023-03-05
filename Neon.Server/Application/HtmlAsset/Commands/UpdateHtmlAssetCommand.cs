using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;

namespace Neon.Application;

public record UpdateHtmlAssetCommand(Guid Id, string Name, int DisplayTime, bool IsActive, int Order, DateTime? NotBefore, DateTime? NotAfter, string Content) : IRequest<HtmlAssetDTO>;    
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
