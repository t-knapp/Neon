using Microsoft.EntityFrameworkCore;
using Neon.Application;
using Neon.Domain;

namespace Neon.Infrastructure;

internal class AssetRepository : Repository<Asset>, IAssetRepository {

    public AssetRepository( DbSet<Asset> entities ) : base( entities ) { }

}