using System;
using MongoDB.Entities;

namespace Neon.Server.Models {
    public class ImageAsset : FileEntity {
        public string Name { get; set; }
        public AssetContext Context { get; set; }
        public int DisplayTime { get; set; }
        public EAssetType Type { get; } = EAssetType.Image;
        public string ContentType { get; set; }
        public DateTime? NotBefore { get; set; }
        public DateTime? NotAfter { get; set; }

        public ImageAsset(string name, AssetContext context, int displayTime, string contentType, DateTime? notBefore, DateTime? notAfter) {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Context = context ?? throw new ArgumentNullException(nameof(context));
            DisplayTime = displayTime;
            ContentType = contentType ?? throw new ArgumentException(nameof(contentType));
            NotBefore = notBefore;
            NotAfter = notAfter;
        }
    }
}