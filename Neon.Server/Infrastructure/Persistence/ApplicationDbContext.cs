using Neon.Application;

namespace Neon.Infrastructure;

public class ApplicationDbContext : IApplicationDbContext {

    public IImageAssetRepository ImageAssetRepository { get; }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) {
        return Task.FromCanceled(cancellationToken);
    }

}