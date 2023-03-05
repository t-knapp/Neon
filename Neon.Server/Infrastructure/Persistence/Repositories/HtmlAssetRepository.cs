using Microsoft.EntityFrameworkCore;
using Neon.Application;
using Neon.Domain;

namespace Neon.Infrastructure;

internal class HtmlAssetRepository : Repository<HtmlAsset>, IHtmlAssetRepository {

    public HtmlAssetRepository( DbSet<HtmlAsset> entities ) : base( entities ) { }

}