namespace Neon.Application;

public interface IApplicationDbContext
{
    IImageAssetRepository ImageAssetRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}