using Microsoft.EntityFrameworkCore;
using Neon.Application;
using Neon.Domain;

namespace Neon.Infrastructure;

internal class ImageAssetRepository : Repository<ImageAsset>, IImageAssetRepository {

    public ImageAssetRepository( DbSet<ImageAsset> entities ) : base( entities ) { }

}