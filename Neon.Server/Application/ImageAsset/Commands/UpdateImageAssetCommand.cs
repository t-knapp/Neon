using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.JsonPatch;
using MediatR;
using AutoMapper;

namespace Neon.Application;

public record UpdateImageAssetCommand(Guid Id, string Name, int DisplayTime, bool IsActive, int Order, DateTime? NotBefore, DateTime? NotAfter) : IRequest<ImageAssetDTO>;

public class UpdateImageAssetCommandHandler : IRequestHandler<UpdateImageAssetCommand, ImageAssetDTO> {
    
    private readonly IApplicationDbContext _database;
    private readonly IMapper _mapper;

    public UpdateImageAssetCommandHandler(IApplicationDbContext database, IMapper mapper) {
        _database = database;
        _mapper = mapper;
    }

    public async Task<ImageAssetDTO> Handle(UpdateImageAssetCommand command, CancellationToken cancellationToken) {
        var asset = await _database.ImageAssetRepository.OneAsync(command.Id, cancellationToken);
        if (asset is null)
            throw new KeyNotFoundException();

        asset.Name = command.Name;
        asset.Order = command.Order;
        asset.DisplayTime = command.DisplayTime;
        asset.IsActive = command.IsActive;
        asset.NotBefore = command.NotBefore;
        asset.NotAfter = command.NotAfter;
        await _database.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ImageAssetDTO>(asset);
    }
}
