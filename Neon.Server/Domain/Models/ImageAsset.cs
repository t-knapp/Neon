using System;
using MongoDB.Entities;

namespace Neon.Server.Models {
    public class ImageAsset : FileEntity {
        public string Name { get; set; }
        public int DisplayTime { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public EAssetType Type { get; } = EAssetType.Image;
        public string ContentType { get; set; }
        public DateTime? NotBefore { get; set; }
        public DateTime? NotAfter { get; set; }

        public ImageAsset(string name, int displayTime, bool isActive, int order, string contentType, DateTime? notBefore, DateTime? notAfter) {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            DisplayTime = displayTime;
            IsActive = isActive;
            Order = order;
            ContentType = contentType ?? throw new ArgumentException(nameof(contentType));
            NotBefore = notBefore;
            NotAfter = notAfter;
        }
    }
}