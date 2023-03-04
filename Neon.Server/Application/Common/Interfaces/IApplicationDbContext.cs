namespace Neon.Application;

public interface IApplicationDbContext
{
    IAssetRepository AssetRepository { get; }
    IImageAssetRepository ImageAssetRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}