using Microsoft.EntityFrameworkCore;
using Neon.Application;
using Neon.Domain;

namespace Neon.Infrastructure;

public abstract partial class ApplicationDbContext : DbContext, IApplicationDbContext {

    public DbSet<ImageAsset> ImageAssets { get; set; }
    public IImageAssetRepository ImageAssetRepository => new ImageAssetRepository( ImageAssets );

    public ApplicationDbContext( DbContextOptions options ) : base( options ) { }

    public override Task<int> SaveChangesAsync( CancellationToken cancellationToken = default ) {
        return base.SaveChangesAsync( cancellationToken );
    }

    protected override void OnModelCreating( ModelBuilder builder )
    {
        builder.Entity<ImageAsset>().HasIndex( x => new { x.Id } ).IsUnique();
    }

}