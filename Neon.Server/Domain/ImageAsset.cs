using System;

namespace Neon.Domain;

public class ImageAsset : Entity {
    public string Name { get; set; }
    public int DisplayTime { get; set; }
    public bool IsActive { get; set; }
    public int Order { get; set; }
    public EAssetType Type { get; set; } = EAssetType.Image;
    public string ContentType { get; set; }
    public byte[] Content { get; set; }

    public byte[] ThumbnailContent { get; set; }
    public DateTime? NotBefore { get; set; }
    public DateTime? NotAfter { get; set; }

    public ImageAsset(string name, int displayTime, bool isActive, int order, string contentType, byte[] content, byte[] thumbnailContent, DateTime? notBefore, DateTime? notAfter) {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        DisplayTime = displayTime;
        IsActive = isActive;
        Order = order;
        ContentType = contentType ?? throw new ArgumentException(nameof(contentType));
        Content = content;
        ThumbnailContent = thumbnailContent;
        NotBefore = notBefore;
        NotAfter = notAfter;
    }
}