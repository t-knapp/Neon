namespace Neon.Application;

public interface IApplicationDbContext
{
    IImageAssetRepository ImageAssetRepository { get; }

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}