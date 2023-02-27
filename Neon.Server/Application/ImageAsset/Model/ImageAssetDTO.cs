using Neon.Domain;

namespace Neon.Application;

public class ImageAssetDTO : IMapFrom<ImageAsset> {
    
    public Guid Id {get; set;}
    public string Name { get; set; }
    public int DisplayTime { get; set; }
    public bool IsActive { get; set; }
    public int Order { get; set; }
    public EAssetType Type { get; set; }
    public DateTime? NotBefore { get; set; }
    public DateTime? NotAfter { get; set; }
}