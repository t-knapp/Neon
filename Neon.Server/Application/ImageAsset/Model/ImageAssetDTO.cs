using Neon.Domain;

namespace Neon.Application;

public class ImageAssetDTO : IMapFrom<ImageAsset> {
    
    public Guid Id {get; set;}
    public string Name {get; set;}
    public bool IsActive {get; set;}

    // TODO: More fields.
}