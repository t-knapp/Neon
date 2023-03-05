using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Neon.Domain;

namespace Neon.Application;

public record DeleteAssetCommand( Guid id ) : IRequest<Asset>;

public class DeleteAssetCommandHandler : IRequestHandler<DeleteAssetCommand, Asset> {

    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _database;

    public DeleteAssetCommandHandler(IMapper mapper, IApplicationDbContext database) {
        _mapper = mapper;
        _database = database;
    }

    public async Task<Asset> Handle(DeleteAssetCommand command, CancellationToken cancellationToken) {
        var asset = await _database.AssetRepository.OneAsync( command.id );
        if (asset is null)
            throw new KeyNotFoundException();

        await _database.AssetRepository.RemoveAsync( asset );
        await _database.SaveChangesAsync();
        return asset;
    }
}