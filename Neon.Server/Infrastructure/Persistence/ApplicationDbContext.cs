using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Neon.Application;
using Neon.Domain;

namespace Neon.Infrastructure;

public abstract partial class ApplicationDbContext : DbContext, IApplicationDbContext {

    public DbSet<Asset> Assets { get; set; }
    public DbSet<ImageAsset> ImageAssets { get; set; }

    public IAssetRepository AssetRepository => new AssetRepository( Assets );
    public IImageAssetRepository ImageAssetRepository => new ImageAssetRepository( ImageAssets );

    public ApplicationDbContext( DbContextOptions options ) : base( options ) { }

    public override Task<int> SaveChangesAsync( CancellationToken cancellationToken = default ) {
        return base.SaveChangesAsync( cancellationToken );
    }

    protected override void OnModelCreating( ModelBuilder builder )
    {
        builder.Entity<Asset>()
            .HasIndex( x => new { x.Id } )
            .IsUnique();

        builder.Entity<Asset>()
            .HasDiscriminator( e => e.Type )
            .HasValue<ImageAsset>( EAssetType.Image );

        builder.Entity<Asset>()
            .Property( e => e.Type )
            .HasMaxLength( 200 )
            .HasColumnName( "Type" );


        builder.Entity<Asset>()
            .Property( e => e.Name )
            .HasColumnName( "Name" );

        builder.Entity<Asset>()
            .Property( e => e.DisplayTime )
            .HasColumnName( "DisplayTime" );
        
        builder.Entity<Asset>()
            .Property( e => e.IsActive )
            .HasColumnName( "IsActive" );
               
        builder.Entity<Asset>()
            .Property( e => e.NotAfter )
            .HasColumnName( "NotAfter" );
               
        builder.Entity<Asset>()
            .Property( e => e.NotBefore )
            .HasColumnName( "NotBefore" );
               
        builder.Entity<Asset>()
            .Property( e => e.Order )
            .HasColumnName( "Order" );
        

        builder.Entity<ImageAsset>()
            .Property( e => e.Name )
            .HasColumnName( "Name" );

        builder.Entity<ImageAsset>()
            .Property( e => e.DisplayTime )
            .HasColumnName( "DisplayTime" );
        
        builder.Entity<ImageAsset>()
            .Property( e => e.IsActive )
            .HasColumnName( "IsActive" );
               
        builder.Entity<ImageAsset>()
            .Property( e => e.NotAfter )
            .HasColumnName( "NotAfter" );
               
        builder.Entity<ImageAsset>()
            .Property( e => e.NotBefore )
            .HasColumnName( "NotBefore" );
               
        builder.Entity<ImageAsset>()
            .Property( e => e.Order )
            .HasColumnName( "Order" );

    }

}