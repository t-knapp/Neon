using System;

namespace Neon.Domain;

public record ImageFile(string Name, string MimeType, byte[] Data);

public class ImageAsset : Asset {

    public string ContentType { get; set; }
    public byte[] ImageContent { get; set; }
    public byte[] ThumbnailContent { get; set; }

    public ImageAsset(string name, int displayTime, bool isActive, int order, string contentType, byte[] imageContent, byte[] thumbnailContent, DateTime? notBefore, DateTime? notAfter) {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        DisplayTime = displayTime;
        IsActive = isActive;
        Order = order;
        NotBefore = notBefore;
        NotAfter = notAfter;
        
        Type = EAssetType.Image;
        ContentType = contentType ?? throw new ArgumentException(nameof(contentType));
        ImageContent = imageContent;
        ThumbnailContent = thumbnailContent;
    }
}